using Microsoft.Extensions.DependencyInjection;
using Museums.Core.Interfaces;
using Museums.Repository.Sql.Contexts;
using Museums.Repository.Sql.Repositories;

namespace Museums.Repository.Sql.Helpers
{
    public static class Extensor
    {
        public static void AddRepositorySql( this IServiceCollection services)
        {
            services.AddScoped<AppDbContextSql>();

            services.AddScoped<IRepository, Repositorio>();
            services.AddScoped<IMuseumRepository, MuseoRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ICrontabRepository, CrontabRepository>();
        }
    }
}
