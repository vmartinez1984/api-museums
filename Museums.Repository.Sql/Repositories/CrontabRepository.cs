using Museums.Core.Entities;
using Museums.Core.Interfaces;

namespace Museums.Repository.Sql.Repositories
{
    public class CrontabRepository : ICrontabRepository
    {
        public Task<string> AddAsycn(CrontabEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CrontabEntity>> GetAsync()
        {
            var lista = new List<CrontabEntity>();

            return lista;
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
