using GoodHamburger.Application.Interfaces;
using GoodHamburger.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPedidoAppService, PedidoAppService>();
            services.AddScoped<ICardapioService, CardapioService>();
            services.AddScoped<IPromocaoService, PromocaoService>();

            return services;
        }
    }
}
