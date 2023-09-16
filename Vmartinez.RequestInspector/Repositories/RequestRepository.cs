using Vmartinez.RequestInspector.Contexts;
using Vmartinez.RequestInspector.Entities;
using Vmartinez.RequestInspector.Interfaces;

namespace Vmartinez.RequestInspector.Repositories
{
    internal class RequestRepository : IRequestRepository
    {
        private readonly AppDbContext _appDbContext;

        public RequestRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> AddAsync(HttpContextEntity entity)
        {
            try
            {
                _appDbContext.HttpContext.Add(entity);
                await _appDbContext.SaveChangesAsync();

                return entity.Id;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }
    }
}
