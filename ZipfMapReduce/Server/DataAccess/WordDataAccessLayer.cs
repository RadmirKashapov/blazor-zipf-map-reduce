using ZipfMapReduce.Server.Interface;
using ZipfMapReduce.Shared.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;

namespace ZipfMapReduce.Server.DataAccess
{
    public class WordDataAccessLayer : IWord
    {
        private WordDBContext db;

        public WordDataAccessLayer(WordDBContext _db)
        {
            db = _db;
        }

        //To Get all words details       
        public List<Word> GetAllWords()
        {
            try
            {
                return db.WordRecord.Find(_ => true).ToList();
            }
            catch
            {
                throw;
            }
        }

        //To Add new word record       
        public void AddWord(Word word)
        {
            try
            {
                db.WordRecord.InsertOne(word);
            }
            catch
            {
                throw;
            }
        }


        //Get the details of a particular word      
        public Word GetWordData(string id)
        {
            try
            {
                FilterDefinition<Word> filterWordData = Builders<Word>.Filter.Eq("Id", id);

                return db.WordRecord.Find(filterWordData).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particluar word      
        public void UpdateWord(Word word)
        {
            try
            {
                db.WordRecord.ReplaceOne(filter: g => g.Id == word.Id, replacement: word);
            }
            catch
            {
                throw;
            }
        }

        //To Delete the record of a particular word      
        public void DeleteWord(string id)
        {
            try
            {
                FilterDefinition<Word> wordData = Builders<Word>.Filter.Eq("Id", id);
                db.WordRecord.DeleteOne(wordData);
            }
            catch
            {
                throw;
            }
        }
    }
}
