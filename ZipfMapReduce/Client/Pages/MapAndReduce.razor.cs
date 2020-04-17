using ZipfMapReduce.Shared.Models;
using ZipfMapReduce.Shared.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace ZipfMapReduce.Client.Pages
{
    public class MapAndReduceModel : ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; }
        protected List<Word> wordList = new List<Word>();
        protected List<Word> newWord = new List<Word>();

        protected override async Task OnInitializedAsync()
        {
            await GetWordList();
        }

        protected async Task GetWordList()
        {
            wordList = await Http.GetJsonAsync<List<Word>>("api/Word");
        }

        protected async Task Run()
        {
            await GetWords();
        }

        protected async Task GetWords()
        {
            var pairWords = await ReadLines(@"C:\Users\mylif\Downloads\cantrbry\test.txt");
            foreach(var elem in pairWords)
            {
                newWord.Add(new Word() { Value = elem.Key, Count = elem.Value});
            }
            await SaveWords();
        }

        protected static async Task<Dictionary<string, int>> ReadLines(string path)
        {
            int LineBlockSize = 30000;
            int LineCount = 0;
            List<string> Lines = new List<string>();
            var res = new Dictionary<string, int>();
            using (var fileStream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    string line;
                    while (!streamReader.EndOfStream)
                    {
                        var r = new Dictionary<string, int>();
                        LineCount++;
                        line = streamReader.ReadLine();
                        Lines.Add(line.Trim().ToLower());
                        if (Lines.Count == LineBlockSize)
                        {
                            MapReduceILGPU mp = new MapReduceILGPU();
                            r = await mp.Execute(1, Lines);
                        }
                        if (streamReader.EndOfStream && LineCount % LineBlockSize != 0)
                        {
                            MapReduceILGPU mp = new MapReduceILGPU();
                            r = await mp.Execute(1, Lines);
                        }

                        foreach(var elem in r)
                        {
                            if (res.ContainsKey(elem.Key))
                                res[elem.Key] += elem.Value;
                            else
                            {
                                res.Add(elem.Key, elem.Value);
                            }
                        }
                    }
                }
            }
            return res;
        }

        protected async Task SaveWords()
        {
            Parallel.ForEach(newWord, async elem =>
            {
                {
                    if (elem.Id != null)
                    {
                        await Http.SendJsonAsync(HttpMethod.Put, "api/Employee/Edit", elem);
                    }
                    else
                    {
                        await Http.SendJsonAsync(HttpMethod.Post, "/api/Employee/Create", elem);
                    }
                }
            });
            await GetWordList();
        }
    }
}
