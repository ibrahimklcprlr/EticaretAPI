using EticaretAPI.Aplication.Repositories;
using EticaretAPI.Domain.Entities.Common;
using EticaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T: BaseEntity
    {
        private readonly EticaretAPIDbContext _context;

        public WriteRepository(EticaretAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table =>_context.Set<T>();

        public async Task<bool> AddAsync(T model)
       {
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return entityEntry.State ==EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> datas)
        {
                await Table.AddRangeAsync(datas);
            return true;
        }

        public bool Remove(T datas)
        {
            EntityEntry<T> entityEntry = Table.Remove(datas);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(string id)
        { 
            T model = await Table.FirstOrDefaultAsync(c => c.Id == Guid.Parse(id));
            return Remove(model);
        }
        public bool RemoveRange(T datas)
        {
            Table.RemoveRange(datas);
            return true;
        }

        public async Task<int> SaveAsync()
        =>await _context.SaveChangesAsync();

        public bool Update(T model)
        {
            EntityEntry entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }
    }
}
