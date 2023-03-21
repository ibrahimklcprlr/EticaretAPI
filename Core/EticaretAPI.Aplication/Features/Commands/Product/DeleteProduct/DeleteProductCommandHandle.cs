using EticaretAPI.Aplication.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.Features.Commands.Product.DeleteProduct
{
    public class DeleteProductCommandHandle : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
       readonly IProductWriteRepository _productWriteRepository;

        public DeleteProductCommandHandle(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.RemoveAsync(request.id);
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
