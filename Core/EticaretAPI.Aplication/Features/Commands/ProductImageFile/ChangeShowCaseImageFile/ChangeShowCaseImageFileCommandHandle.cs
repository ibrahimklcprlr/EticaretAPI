using EticaretAPI.Aplication.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.Features.Commands.ProductImageFile.ChangeShowCaseImageFile
{
    public class ChangeShowCaseImageFileCommandHandle : IRequestHandler<ChangeShowCaseImageFileCommandRequest, ChangeShowCaseImageFileCommandResponse>
    {
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
public ChangeShowCaseImageFileCommandHandle(IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<ChangeShowCaseImageFileCommandResponse> Handle(ChangeShowCaseImageFileCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _productImageFileWriteRepository.Table.Include(p => p.Product).SelectMany(p => p.Product, (pif, p) => new { pif, p });
            var data= await query.FirstOrDefaultAsync(c=>c.p.Id==Guid.Parse(request.ProductId)&&c.pif.Showcase);
            if (data!=null)
             data.pif.Showcase = false;
            var image = await query.FirstOrDefaultAsync(c => c.pif.Id == Guid.Parse(request.ImageId));
            image.pif.Showcase = true;
            _productImageFileWriteRepository.SaveAsync();
            return new();

        }
    }
}
