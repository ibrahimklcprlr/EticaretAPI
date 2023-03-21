using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.Features.Queries.ProductImageFile
{
    public class GetProductImageFileQueryResponse
    {
        public string Path { get; set; }
        public  string FileName { get; set; }
        public Guid Id { get; set; }
    }
}
