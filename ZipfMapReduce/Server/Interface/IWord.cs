using ZipfMapReduce.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZipfMapReduce.Server.Interface
{
    public interface IWord
    {
        public List<Word> GetAllWords();
        public void AddWord(Word word);
        public Word GetWordData(string id);
        public void UpdateWord(Word word);
        public void DeleteWord(string id);

    }
}
