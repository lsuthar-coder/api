using System.Reflection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Trial.Server.Models;

namespace Trial.Server.Services
{
    public class GenericMongoDb<T> where T : class, IBaseEntity
    {
        private readonly IMongoCollection<T> collection;

        public GenericMongoDb(IOptions<DataBaseSettings> databaseSettings)
        {

            var settings = databaseSettings.Value; // obj in appsetings.josn
            var collectionAttribute = typeof(T).GetCustomAttribute<MongoCollectionAttribute>()
                ?? throw new InvalidOperationException($"{typeof(T).Name} must define a Mongo collection.");
            var collectionName = typeof(DataBaseSettings)
                .GetProperty(collectionAttribute.SettingsPropertyName)?
                .GetValue(settings) as string;
            if (string.IsNullOrWhiteSpace(collectionName))
            {
                throw new InvalidOperationException(
                    $"The Mongo collection setting '{collectionAttribute.SettingsPropertyName}' is missing.");
            }

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            collection = database.GetCollection<T>(collectionName);
        }

        public Task<List<T>> GetAsync() =>
            collection.Find(_ => true).ToListAsync();

        public async Task<T?> GetAsync(string id) =>
            await collection.Find(entity => entity.Id == id).FirstOrDefaultAsync();

        public Task CreateAsync(T entity) =>
            collection.InsertOneAsync(entity);
    }
}