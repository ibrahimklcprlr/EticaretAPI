using EticaretAPI.Aplication.Repositories;
using EticaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Repositories
{
    public class InvoiceFileReadRepository : ReadRepository<EticaretAPI.Domain.Entities.InvoiceFile>, IInvoiceFileReadRepository
    {
        public InvoiceFileReadRepository(EticaretAPIDbContext context) : base(context)
        {
        }
    }
}
