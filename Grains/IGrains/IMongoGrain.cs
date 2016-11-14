using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Orleans;
using Orleans.Concurrency;

namespace Grains.IGrains
{
    public interface IMongoGrain : IGrainWithStringKey
    {
        Task SetCollection(string collectionName);
        Task Save(object data);
    }

    [StatelessWorker]
    public class MongoGrain : Grain, IMongoGrain
    {
        IMongoClient _client;
        IMongoDatabase _database;

        string _collectionName;

        public override async Task OnActivateAsync()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase(this.GetPrimaryKeyString());
                                  
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

        public async Task SetCollection(string collectionName)
        {
            _collectionName = collectionName;
            if (await CollectionExistsAsync(_collectionName) != true)
            {
                _database.CreateCollection(_collectionName);
            }
        }

        public async Task Save(object data)
        {
            var document = data.ToBsonDocument();
            
            await _database.GetCollection<BsonDocument>(_collectionName).InsertOneAsync(document);
        }
    }
}
