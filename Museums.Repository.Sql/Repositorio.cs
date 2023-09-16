using Museums.Core.Entities;
using Museums.Core.Interfaces;

namespace Museums.Repository.Sql
{
    public class Repositorio : IRepository
    {
        public Repositorio(
            IMuseumRepository museumRepository,
            ILogRepository logRepository,
            ICrontabRepository crontabRepository
        )
        {
            this.Museum = museumRepository;
            this.Crontab = crontabRepository;
        }

        public IMuseumRepository Museum { get; }

        public ILogRepository Log => throw new NotImplementedException();

        public ICrontabRepository Crontab { get; }
    }
}
