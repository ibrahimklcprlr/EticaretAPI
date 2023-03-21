using EticaretAPI.Aplication.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.Features.Queries.ProductImageFile
{
    public class GetProductImageFileQueryHandle : IRequestHandler<GetProductImageFileQueryRequest, List<GetProductImageFileQueryResponse>>
    {
        IProductReadRepository _productReadRepository;
        IConfiguration configuration;

        public GetProductImageFileQueryHandle(IProductReadRepository productReadRepository, IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            this.configuration = configuration;
        }

        public async Task<List<GetProductImageFileQueryResponse>> Handle(GetProductImageFileQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.id));

            return product.ProductImages.Select(p => new GetProductImageFileQueryResponse
            {
                Path = $"{configuration["BaseStorageUrl"]}/{p.Path}",
                FileName = p.FileName,
                Id = p.Id

            }).ToList();

        }
    }
}
