using Vmartinez.RequestInspector.Entities;

namespace Vmartinez.RequestInspector.Interfaces
{
    public interface IRequestRepository
    {
        Task<int> AddAsync(HttpContextEntity entity);
    }
}