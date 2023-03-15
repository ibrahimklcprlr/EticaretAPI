﻿using EticaretAPI.Aplication.Repositories;
using EticaretAPI.Domain.Entities;
using EticaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Repositories.Invoice
{
    public class InvoiceFileWriteRepository : WriteRepository<InvoiceFile>, IInvoiceFileWriteRepository
    {
        public InvoiceFileWriteRepository(EticaretAPIDbContext context) : base(context)
        {
        }
    }
}
