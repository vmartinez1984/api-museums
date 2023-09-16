using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Museums.Core.Entities;

namespace Museums.Repository.Sql.Contexts
{
    public class AppDbContextSql : DbContext
    {
        private readonly string _connectionString;

        public AppDbContextSql()
        {
            _connectionString = "Data Source=192.168.1.86;Initial Catalog=Museos; Persist Security Info=True;User ID=sa;Password=Macross#2012; TrustServerCertificate=True;";
        }

        public AppDbContextSql(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServer");
        }

        public DbSet<MuseumEntity> Museo { get; set; }
        public DbSet<CrontabEntity> Crontab { get; set; }
        public DbSet<LogEntity> Log { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
    }
}
