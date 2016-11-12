using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Orleans;
using Orleans.Concurrency;

//Todo Get the configuration to a centran location
//Todo Get a serilizer instead of the static document

namespace Grains.IGrains
{
    public interface IMongoWriter : IGrainWithIntegerKey
    {
        Task SavePageData(string uri, string title, string text, string source);
    }

    [StatelessWorker]
    public class MongoWriter : Grain, IMongoWriter
    {
        IMongoClient _client;
        IMongoDatabase _database;

        public override async Task OnActivateAsync()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("test");

            if (await CollectionExistsAsync("test1") != true)
            {
                _database.CreateCollection("test1");
            }
            
            await base.OnActivateAsync();
        }
      
        private async Task<bool> CollectionExistsAsync(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);

            //filter by collection name
            var collections = await _database.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter });
           
            //check for existence
            return (await collections.ToListAsync()).Any();
        }

        public async Task SavePageData(string uri, string title, string text, string source)
        {
            var document = new BsonDocument
            {
                {"uri", uri},
                {"title", title},
                {"text", text},
                {"source", source}
            };

            await _database.GetCollection<BsonDocument>("test1").InsertOneAsync(document);
        }
    }
}
