
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using EticaretAPI.Aplication.Abstraction.Token;

namespace EticaretAPI.Aplication
{
    public static class ServicesRegistration
    { 
        public static void AddAplicationServices(this IServiceCollection services) {
        services.AddMediatR(typeof(ServicesRegistration));
        
        }

    }
}
