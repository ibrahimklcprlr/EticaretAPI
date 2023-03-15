using EticaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.Repositories
{
    public interface IReadRepository<T>:IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(string id, bool tracking);
        IQueryable<T> GetAll(bool tracking);
        IQueryable<T> GetWhere(bool tracking, Expression<Func<T,bool>>method);
        Task<T> GetSingleAsync( Expression<Func<T,bool>>method, bool tracking);
    }
}
