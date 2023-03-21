using EticaretAPI.Aplication.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P=EticaretAPI.Domain.Entities;

namespace EticaretAPI.Aplication.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandle : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly  IProductWriteRepository _productWriteRepository;
        readonly IProductReadRepository _productReadRepository;

        public UpdateProductCommandHandle(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            P.Product product = await _productReadRepository.GetByIdAsync(request.Id,tracking:true);
            product.Name = request.Name;
            product.Price = request.Price;
            product.Stock = request.Stock;
            await _productWriteRepository.SaveAsync();
            return new();

        }
    }
}
