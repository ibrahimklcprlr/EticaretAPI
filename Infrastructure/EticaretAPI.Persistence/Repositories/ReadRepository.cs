using EticaretAPI.Aplication.Repositories;
using EticaretAPI.Domain.Entities.Common;
using EticaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly EticaretAPIDbContext _context;

        public ReadRepository(EticaretAPIDbContext context)
        {
            this._context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking=true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            query = query.AsNoTracking();
            return query;
        }
        public IQueryable<T> GetWhere(bool tracking, Expression<Func<T, bool>> method)
        {
            var query = Table.Where(method);
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        { var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }
        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        //=> await Table.FirstOrDefaultAsync(s => s.Id == Guid.Parse(id));
        {
            var query = Table.AsQueryable();
            if(!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(s => s.Id == Guid.Parse(id));
        }


    }
}
