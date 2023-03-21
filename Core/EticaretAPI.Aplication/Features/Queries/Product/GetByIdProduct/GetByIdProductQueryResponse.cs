using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.Features.Queries.Product.GetProduct
{
    public class GetByIdProductQueryResponse
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
    }
}
