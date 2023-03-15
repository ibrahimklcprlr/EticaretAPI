using EticaretAPI.Aplication.Repositories;
using EticaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Repositories
{
    public class FileReadRepository : ReadRepository<EticaretAPI.Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(EticaretAPIDbContext context) : base(context)
        {
        }
    }
}
