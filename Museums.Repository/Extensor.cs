using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Museums.Core.Interfaces;

namespace Museums.Repository
{
    public static class Extensor
    {
        public static void AddRepositoryNoSql(this IServiceCollection services, IConfiguration configuration)
        {            
            //services.Configure<DbSettings>(configuration.GetSection("DbSettings"));
            services.AddScoped<IMuseumRepository, MuseumRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ICrontabRepository, CrontabRepository>();
            services.AddScoped<IRepository, Repository>();
        }
    }
}
