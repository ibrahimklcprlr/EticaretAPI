using EticaretAPI.Aplication.Repositories;
using EticaretAPI.Aplication.RequestParametres;
using EticaretAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;

        public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(request.Page * request.Size).Take(request.Size)
                .Include(p=>p.ProductImages).
                Select(p => new
            {
                p.Id,
                p.CreatedDate,
                p.UpdatedDate,
                p.Price,
                p.Name,
                p.Stock,
                p.ProductImages
            }).ToList();


            return new()
            {
                totalCount = totalCount,
                products = products
            };

        }
    }
}
