using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Vmartinez.RequestInspector.Entities;

namespace Vmartinez.RequestInspector.Contexts
{
    internal class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;        

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("RequestInspector"));
            }
        }

        public DbSet<HttpContextEntity> HttpContext { get; set; }
    }
}
