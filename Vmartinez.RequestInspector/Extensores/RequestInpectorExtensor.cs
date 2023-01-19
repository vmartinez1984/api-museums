using Microsoft.Extensions.DependencyInjection;
using Vmartinez.RequestInspector.Contexts;
using Vmartinez.RequestInspector.Interfaces;
using Vmartinez.RequestInspector.Repositories;

namespace Vmartinez.RequestInspector.Extensores
{
    public static class RequestInpectorExtensor
    {
        public static void AddRequestInpector(this IServiceCollection services)
        {
            services.AddTransient<AppDbContext>();
            services.AddTransient<IRequestRepository, RequestRepository>();            
        }
    }
}
