using EticaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.Repositories
{
    public interface IRepository<T> where T:BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
