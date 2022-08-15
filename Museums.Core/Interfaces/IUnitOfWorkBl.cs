using Museums.Core.Dtos;
using Museums.Core.Entities;

namespace Museums.Core.Interfaces
{
    public interface IUnitOfWorkBl
    {
        public IMuseum Museum { get; }
        public ICrontabBl Crontab { get; }
        public ILogBl Log { get; }

        public IScrapyBl Scrapy {get;}
    }

    public interface IScrapyBl
    {
        string Process();
        string Process(string id);
    }

    public interface ILogBl
    {
        void Update(LogDto logDto);

        string Add(LogDto logDto);
        Task<LogDto> GetAsync(string id);
        Task UpdateAsync(LogDto item);
    }

    public interface ICrontabBl
    {
        Task<List<CrontabDto>> GetAsync();

        Task<string> AddAsync(CrontabDtoIn item);
    }

    public interface IMuseum
    {
        Task<MuseumDto> GetAsync(int museumId);

        Task<MuseumDto> GetAsync(string id);

        Task<List<MuseumDto>> GetAsync();

        Task UpdateAsync(MuseumDto museum);

        void Update(MuseumDto museum);
    }
}