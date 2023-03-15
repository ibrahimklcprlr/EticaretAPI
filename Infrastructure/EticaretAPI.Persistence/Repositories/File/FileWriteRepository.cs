using EticaretAPI.Aplication.Repositories;
using EticaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Repositories.File
{
    public class FileWriteRepository : WriteRepository<EticaretAPI.Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(EticaretAPIDbContext context) : base(context)
        {
        }
    }
}
