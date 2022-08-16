using Museums.Core.Entities;

namespace Museums.Core.Interfaces
{
    public interface IRepository
    {
        public IMuseumRepository Museum { get; }
        public ILogRepository Log { get; }
        public ICrontabRepository Crontab { get; }
    }

    public interface IMuseumRepository
    {
        Task UpdateAsync(MuseumEntity item);

        Task<List<MuseumEntity>> GetAsync();

        Task<List<MuseumEntity>> GetAsync(Pager pager);

        Task<string> AddAsync(MuseumEntity entity);

        Task<MuseumEntity> GetAsync(int museumId);
        
        Task<MuseumEntity> GetAsync(string id);
        void Update(MuseumEntity entity);
    }

    public interface ICrontabRepository
    {
        Task<string> AddAsycn(CrontabEntity entity);

        Task UpdateAsycn(CrontabEntity entity);

        Task<List<CrontabEntity>> GetAsync();

        Task<CrontabEntity> GetAsync(string id);
    }

    public interface ILogRepository
    {
        string Add(LogEntity entity);

        void Update(LogEntity entity);

        Task<List<LogEntity>> GetAsync();

        Task<LogEntity> GetAsync(string id);
        Task UpdateAsync(LogEntity entity);
    }
}