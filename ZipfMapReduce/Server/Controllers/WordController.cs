using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZipfMapReduce.Server.Interface;
using ZipfMapReduce.Shared.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZipfMapReduce.Server.Controllers
{
    [Route("api/[controller]")]
    public class WordController : Controller
    {
        private readonly IWord objword;

        public WordController(IWord _objword)
        {
            objword = _objword;
        }

        [HttpGet]
        public IEnumerable<Word> Get()
        {
            return objword.GetAllWords();
        }

        [HttpPost]
        public void Post([FromBody] Word word)
        {
            objword.AddWord(word);
        }

        [HttpGet("{id}")]
        public Word Get(string id)
        {
            return objword.GetWordData(id);
        }

        [HttpPut]
        public void Put([FromBody]Word word)
        {
            objword.UpdateWord(word);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            objword.DeleteWord(id);
        }
    }
}
