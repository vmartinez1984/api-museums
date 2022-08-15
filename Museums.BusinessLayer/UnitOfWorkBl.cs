using Museums.Core.Interfaces;

namespace Museums.BusinessLayer
{
    public class UnitOfWorkBl : IUnitOfWorkBl
    {

        public UnitOfWorkBl(
            IMuseum museum
            , ICrontabBl crontabBl
            , ILogBl logBl
            , IScrapyBl scrapyBl
        )
        {
            this.Museum = museum;
            this.Crontab = crontabBl;
            this.Log = logBl;
            this.Scrapy = scrapyBl;
        }
        public IMuseum Museum { get; }

        public ICrontabBl Crontab { get; }

        public ILogBl Log { get; }

        public IScrapyBl Scrapy { get; }        
    }
}