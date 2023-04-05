using EticaretAPI.Aplication.Repositories.Basket;
using EticaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Repositories.Basket
{
    internal class BasketReadRepository : ReadRepository<Domain.Entities.Basket>, IBasketReadRepository
    {
        public BasketReadRepository(EticaretAPIDbContext context) : base(context)
        {
        }
    }
}
