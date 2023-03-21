using EticaretAPI.Aplication.Abstraction.Storage;
using EticaretAPI.Aplication.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.Features.Commands.ProductImageFile.UploadProductImageFile
{
    public class UploadProductImageFileCommandHandle : IRequestHandler<UploadProductImageFileCommandRequest, UploadProductImageFileCommandResponse>
    {
       readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly IProductReadRepository _productReadRepository;
        readonly IStorageService _storageService;

        public UploadProductImageFileCommandHandle(IProductImageFileWriteRepository productImageFileWriteRepository, IProductReadRepository productReadRepository, IStorageService storageService)
        {
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _productReadRepository = productReadRepository;
            _storageService = storageService;
        }

        public async Task<UploadProductImageFileCommandResponse> Handle(UploadProductImageFileCommandRequest request, CancellationToken cancellationToken)
        {
            List<(string filename, string pathorContainerName)> result = await _storageService.UploadAsync("photo-images", request.FormData);

            Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id, true);
            await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new Domain.Entities.ProductImageFile
            {
                FileName = r.filename,
                Path = r.pathorContainerName,
                Storage = _storageService.StorageName,
                Product = new List<Domain.Entities.Product>() { product }
            }).ToList()); ;
            await _productImageFileWriteRepository.SaveAsync();
            return new();
        }
    }
}
