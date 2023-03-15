
using EticaretAPI.Aplication.Abstraction.Storage;
using EticaretAPI.Infrastructure.Services;
using EticaretAPI.Infrastructure.Services.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Infrastructure
{
    public static class ServicesRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
        }
        public static void AddStorage<T>(this IServiceCollection services) where T:Storage ,IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}
