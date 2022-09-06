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

        /// <summary>
        /// Init the process for updates of musuems
        /// </summary>
        /// <param name="log"></param>
        void UpdateMuseums(LogDto log);

        /// <summary>
        /// Init the process for updates of musuems
        /// </summary>
        /// <param name="log"></param>
        Task UpdateMuseumsAsync(LogDto log);
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

        Task<MuseumPagerDto> GetAsync(PagerDto pager);

        Task UpdateAsync(MuseumDto museum);

        void Update(MuseumDto museum);
    }
}