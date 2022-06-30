using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace DataAccessLayer.Repositories
{
    internal class MongoRepository<T> : IRepository<T> where T : BaseModel
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository()
        {
            var connectionString = "mongodb://localhost:27017/ProductsDb";

            var connection = new MongoUrlBuilder(connectionString);

            var client = new MongoClient(connectionString);

            var database = client.GetDatabase(connection.DatabaseName);

            _collection = database.GetCollection<T>($"{typeof(T).Name}s");
        }

        public IEnumerable<T> GetAll()
        {
            var result = _collection.Find(new BsonDocument()).ToList();

            return result;
        }

        public T Get(int id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var result = _collection.Find(filter).First();

            return result;
        }

        public void Create(T item)
        {
            _collection.InsertOne(item);
        }

        public void Update(T item)
        {
            _collection.ReplaceOne(
                new BsonDocument("_id", item.Id),
                item,
                new ReplaceOptions() { IsUpsert = false });
        }

        public void Delete(int id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            _collection.FindOneAndDelete(filter);
        }
    }
}
