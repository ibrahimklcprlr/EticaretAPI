using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.Features.Commands.ProductImageFile.ChangeShowCaseImageFile
{
    public class ChangeShowCaseImageFileCommandRequest:IRequest<ChangeShowCaseImageFileCommandResponse>
    {
        public string ImageId { get; set; }
        public string ProductId { get; set; }
    }
}
