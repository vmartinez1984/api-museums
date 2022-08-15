using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Museums.Core.Entities;
using Museums.Core.Interfaces;
using System.Linq;

namespace Museums.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly IMongoCollection<LogEntity> _collection;

        public LogRepository(IOptions<DbSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
           databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<LogEntity>(
                databaseSettings.Value.LogCollection);
        }

        public string Add(LogEntity entity)
        {
            _collection.InsertOne(entity);

            return entity.Id;
        }

        public async Task<string> AddAsync(LogEntity entity)
        {
            await _collection.InsertOneAsync(entity);

            return entity.Id;
        }

        public async Task<List<LogEntity>> GetAsync()
        {
            List<LogEntity> entities;

            int pageSize = 10;
            int page = 1;
            //var filter = Builders<LogEntity>.Filter.Where(x => x.MuseoNombre.Contains("Historia"));

            entities = await _collection.Find(_=> true)
            //entities = await _collection.Find(filter)
                .Sort("{MuseoId:1}")
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
            var count = entities.Count();
            //entities = await _collection.Find(_ => true).ToListAsync();

            return entities;
        }

        public async Task<LogEntity> GetAsync(string id)
        {
            LogEntity entity;

            entity = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

            return entity;
        }

        public void Update(LogEntity entity)
        {
            _collection.ReplaceOne(x => x.Id == entity.Id, entity);
        }

        public async Task UpdateAsync(LogEntity entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }
    }
}