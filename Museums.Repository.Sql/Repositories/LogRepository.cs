using Museums.Core.Entities;
using Museums.Core.Interfaces;

namespace Museums.Repository.Sql.Repositories
{
    public class LogRepository : ILogRepository
    {
        public string Add(LogEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<LogEntity>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<LogEntity> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(LogEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(LogEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
