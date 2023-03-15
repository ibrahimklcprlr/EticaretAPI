using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.RequestParametres
{
    public record Pagenations
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
