using Microsoft.Extensions.DependencyInjection;
using Museums.Core.Interfaces;

namespace Museums.BusinessLayer
{
    public static class Extensor
    {
        public static void AddBl(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWorkBl, UnitOfWorkBl>();

            services.AddScoped<IMuseum, MuseumBl>();
            services.AddScoped<ICrontabBl, CrontabBl>();
            services.AddScoped<ILogBl, LogBl>();
            services.AddScoped<IScrapyBl, ScrapyBl>();
        }
    }
}
