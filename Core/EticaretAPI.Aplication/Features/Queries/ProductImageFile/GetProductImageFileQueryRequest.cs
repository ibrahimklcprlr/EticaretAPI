using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.Features.Queries.ProductImageFile
{
    public class GetProductImageFileQueryRequest:IRequest<List<GetProductImageFileQueryResponse>>
    {
        public string id { get; set; }
    }
}
