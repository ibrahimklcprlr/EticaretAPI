using EticaretAPI.Aplication.Features.Queries.Product.GetAllProduct;
using EticaretAPI.Aplication.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P = EticaretAPI.Domain.Entities;

namespace EticaretAPI.Aplication.Features.Queries.Product.GetProduct
{
    public class GetByIdProductQueryHandle : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
       readonly IProductReadRepository _productReadRepository;

        public GetByIdProductQueryHandle(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            P.Product responseproduct= await _productReadRepository.GetByIdAsync(request.id, true);
            return new() { Price=responseproduct.Price,Stock=responseproduct.Stock,Name=responseproduct.Name};
            
        }

      
    }
}
