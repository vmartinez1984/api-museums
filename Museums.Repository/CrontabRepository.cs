using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Museums.Core.Entities;
using Museums.Core.Interfaces;

namespace Museums.Repository
{
    public class CrontabRepository : ICrontabRepository
    {
        private readonly IMongoCollection<CrontabEntity> _collection;

        public CrontabRepository(IOptions<DbSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
           databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<CrontabEntity>(
                databaseSettings.Value.CrontabCollection);
        }

        public async Task<string> AddAsycn(CrontabEntity entity)
        {
            await _collection.InsertOneAsync(entity);

            return entity.Id;
        }

        public async Task<List<CrontabEntity>> GetAsync()
        {
            List<CrontabEntity> entities;

            entities = await _collection.Find(_ => true).ToListAsync();

            return entities;
        }

        public Task<CrontabEntity> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsycn(CrontabEntity entity)
        {
            throw new NotImplementedException();
        }
    }

}