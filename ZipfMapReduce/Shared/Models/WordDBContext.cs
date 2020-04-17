using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace ZipfMapReduce.Shared.Models
{
    public class WordDBContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        public WordDBContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _mongoDatabase = client.GetDatabase("WordDB");
        }
        public IMongoCollection<Word> WordRecord
        {
            get
            {
                return _mongoDatabase.GetCollection<Word>("WordRecord");
            }
        }
    }

}
