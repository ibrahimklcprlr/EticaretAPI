﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Domain.Entities
{
    public class ProductImageFile:File
    {
        public ICollection<Product> Product { get; set; }
    }
}
