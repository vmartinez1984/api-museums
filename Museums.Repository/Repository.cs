using Museums.Core.Interfaces;

namespace Museums.Repository
{
    public class Repository : IRepository
    {
        public Repository(
            IMuseumRepository museumRepository, ILogRepository logRepository, 
            ICrontabRepository crontabrepository)
        {
            this.Museum = museumRepository;
            this.Log = logRepository;
            this.Crontab = crontabrepository;
        }
        
        public IMuseumRepository Museum { get; }

        public ILogRepository Log {get;}

        public ICrontabRepository Crontab {get;}
    }
}