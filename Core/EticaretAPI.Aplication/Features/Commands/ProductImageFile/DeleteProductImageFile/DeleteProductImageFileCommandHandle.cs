using EticaretAPI.Aplication.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EticaretAPI.Aplication.Features.Commands.ProductImageFile.DeleteProductImageFile
{
    public class DeleteProductImageFileCommandHandle : IRequestHandler<DeleteProductImageFileCommandRequest, DeleteProductImageFileCommandResponse>
    {
        IProductReadRepository _productReadRepository;
        IProductWriteRepository _productWriteRepository;

        public DeleteProductImageFileCommandHandle(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<DeleteProductImageFileCommandResponse> Handle(DeleteProductImageFileCommandRequest request, CancellationToken cancellationToken)
        {
           Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
           Domain.Entities.ProductImageFile? productImageFile = product?.ProductImages.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));
            if(productImageFile!=null)
             product?.ProductImages.Remove(productImageFile);
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
