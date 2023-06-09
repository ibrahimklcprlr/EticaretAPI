﻿using EticaretAPI.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EticaretAPI.Aplication.Repositories;
using EticaretAPI.Persistence.Repositories;
using EticaretAPI.Persistence.Repositories.Invoice;
using EticaretAPI.Persistence.Repositories.File;
using EticaretAPI.Persistence.Repositories.ProductImage;
using EticaretAPI.Domain.Entities.Identity;
using EticaretAPI.Aplication.Abstraction.Services;
using EticaretAPI.Persistence.Services;
using EticaretAPI.Aplication.Repositories.Basket;
using EticaretAPI.Persistence.Repositories.Basket;
using EticaretAPI.Aplication.Repositories.BasketItem;
using EticaretAPI.Persistence.Repositories.BasketItem;

namespace EticaretAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this  IServiceCollection services) 
        {
            services.AddDbContext<EticaretAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<EticaretAPIDbContext>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();
            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();

        }

    }
}
