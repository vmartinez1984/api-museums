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

        //public AppDbContext()
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=.;Database=RequestInspector;User Id=sa;Password=123456; TrustServerCertificate=True");
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("RequestInspector"));
            }
        }

        public DbSet<HttpContextEntity> HttpContext { get; set; }
    }
}
