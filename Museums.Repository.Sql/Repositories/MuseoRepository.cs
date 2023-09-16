using Microsoft.EntityFrameworkCore;
using Museums.Core.Entities;
using Museums.Core.Interfaces;
using Museums.Repository.Sql.Contexts;

namespace Museums.Repository.Sql.Repositories
{
    public class MuseoRepository : IMuseumRepository
    {
        private readonly AppDbContextSql _dbContext;

        public MuseoRepository(AppDbContextSql context)
        {
            this._dbContext = context;
        }

        public async Task<string> AddAsync(MuseumEntity entity)
        {
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity.Id.ToString();
        }

        public Task DeleteAsync(int museoId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MuseumEntity>> GetAsync()
        {
            var list = await _dbContext.Museo.ToListAsync();

            list.ForEach(entity =>
            {
                if (entity != null && !string.IsNullOrEmpty(entity.UrlImgs))
                    entity.ListUrlImg = entity.UrlImgs.Split("|").ToList();
            });

            return list;
        }

        public async Task<List<MuseumEntity>> GetAsync(Pager pager)
        {
            List<MuseumEntity> list;
            IQueryable<MuseumEntity> queryable;

            queryable = _dbContext.Museo;// .Include(x => x.Person);
            //queryable = queryable.Where(x => x.IsActive);
            pager.TotalRecords = queryable.Count();
            if (string.IsNullOrEmpty(pager.Search) == false)
            {
                pager.Search = pager.Search.ToLower();
                queryable = queryable.Where(
                   x =>
                   x.MuseoNombre.ToLower().Contains(pager.Search)
                   ||
                   x.MuseoTematicaN1.ToLower().Contains(pager.Search)
                     ||
                   x.MuseoColonia.ToLower().Contains(pager.Search)
                );
            }
            //if (string.IsNullOrEmpty(pager.SortColumn) == false && string.IsNullOrEmpty(pager.SortColumnDir) == false)
            //{
            //    queryable = queryable.OrderBy(pager.SortColumn + " " + pager.SortColumnDir);
            //}
            var sql = queryable.ToQueryString();
            list = await queryable
            //.OrderByDescending(x => x.Id)
            .Skip((pager.PageCurrent - 1) * pager.RecordsPerPage)
            .Take(pager.RecordsPerPage)
            .ToListAsync();
            pager.TotalRecordsFiltered = await queryable.CountAsync();

            list.ForEach(entity =>
            {
                if (entity != null && !string.IsNullOrEmpty(entity.UrlImgs))
                    entity.ListUrlImg = entity.UrlImgs.Split("|").ToList();
            });

            return list;
        }

        public async Task<MuseumEntity> GetAsync(int museumId)
        {
            var entity = await _dbContext.Museo.Where(x => x.MuseoId == museumId).FirstOrDefaultAsync();
            if (entity != null && !string.IsNullOrEmpty(entity.UrlImgs))
                entity.ListUrlImg = entity.UrlImgs.Split("|").ToList();
            return entity;
        }

        public async Task<MuseumEntity> GetAsync(string id)
        {
            var entity = await _dbContext.Museo.Where(x => x.Id.ToString() == id).FirstOrDefaultAsync();
            if (entity != null && !string.IsNullOrEmpty(entity.UrlImgs))
                entity.ListUrlImg = entity.UrlImgs.Split("|").ToList();

            return entity;
        }

        public void Update(MuseumEntity entity)
        {
            _dbContext.Museo.Update(entity);
            _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(MuseumEntity item)
        {
            _dbContext.Museo.Update(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}
