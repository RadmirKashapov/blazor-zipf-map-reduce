using ZipfMapReduce.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ZipfMapReduce.Client.Pages
{
    public class WordDataModel : ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; }
        protected List<Word> wordList = new List<Word>();

        protected override async Task OnInitializedAsync()
        {
            await GetWordList();
        }

        protected async Task GetWordList()
        {
            wordList = await Http.GetJsonAsync<List<Word>>("api/Word");
        }
    }
}
