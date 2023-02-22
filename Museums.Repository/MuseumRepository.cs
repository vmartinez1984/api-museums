using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Museums.Core.Entities;
using Museums.Core.Interfaces;

namespace Museums.Repository
{
    public class MuseumRepository : IMuseumRepository
    {
        private readonly IMongoCollection<MuseumEntity> _collection;

        public MuseumRepository(IOptions<DbSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
           databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<MuseumEntity>(
                databaseSettings.Value.MuseumCollection);
        }

        public async Task<string> AddAsync(MuseumEntity entity)
        {
            await _collection.InsertOneAsync(entity);

            return entity.Id;
        }

        public async Task<List<MuseumEntity>> GetAsync()
        {
            List<MuseumEntity> entities;

            int pageSize = 10;
            int page = 1;
            var filter = Builders<MuseumEntity>.Filter.Where(x => x.MuseoNombre.Contains("Historia"));

            //var data = await _collection.Find(_=> true)
            entities = await _collection.Find(filter)
                .Sort("{MuseoId:1}")
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
            var count = entities.Count();
            //entities = await _collection.Find(_ => true).ToListAsync();

            return entities;
        }

        public async Task<List<MuseumEntity>> GetAsync(Pager pager)
        {
            try
            {
                List<MuseumEntity> entities;
                FilterDefinition<MuseumEntity> filter;

                if (string.IsNullOrEmpty(pager.Search))
                    filter = Builders<MuseumEntity>.Filter.Where(_ => true);
                else
                {
                    pager.Search = pager.Search.ToLower();
                    filter = Builders<MuseumEntity>.Filter.Where(
                        x => x.MuseoNombre.ToLower().Contains(pager.Search)
                        || x.MuseoTematicaN1.ToLower().Contains(pager.Search)
                        || x.NomMun.ToLower().Contains(pager.Search)
                        || x.HoariosYCostos.ToLower().Contains(pager.Search)                        
                    );
                }


                entities = await _collection.Find(filter)
                    .Sort("{MuseoId:1}")
                    .Skip((pager.PageCurrent - 1) * pager.RecordsPerPage)
                    .Limit(pager.RecordsPerPage)
                    .ToListAsync();
                pager.TotalRecordsFiltered = (int)await _collection.Find(filter).Sort("{MuseoId:1}").CountAsync();
                pager.TotalRecords = (int)await _collection.CountDocumentsAsync(new BsonDocument());

                return entities;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<MuseumEntity> GetAsync(int id)
        {
            MuseumEntity entity;

            entity = await _collection.Find(x => x.MuseoId == id).FirstOrDefaultAsync();

            return entity;
        }

        public async Task<MuseumEntity> GetAsync(string id)
        {
            MuseumEntity entity;

            entity = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

            return entity;
        }

        public void Update(MuseumEntity entity)
        {
            _collection.ReplaceOne(x => x.Id == entity.Id, entity);
        }

        public async Task UpdateAsync(MuseumEntity entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }
    }
}