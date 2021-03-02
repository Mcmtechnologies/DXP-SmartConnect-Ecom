using DXP.SmartConnect.Ecom.Core.Interfaces;
using DXP.SmartConnect.Ecom.Infrastructure.Data.Database;
using DXP.SmartConnect.Ecom.Infrastructure.Data.WebApiClients;
using DXP.SmartConnect.Ecom.SharedKernel.Extensions;
using DXP.SmartConnect.Ecom.SharedKernel.Interfaces;
using DXP.SmartConnect.Ecom.SharedKernel.WebApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DXP.SmartConnect.Ecom.Infrastructure.Extensions
{
    public static class DefaultInfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServiceConfig(this IServiceCollection services, IConfiguration config)
        {
            string providerRequestUri = config.GetSection("ProviderSettings")?["RequestUri"];

            services.AddDbContext<DBContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.Configure<WebApiFaultHandleConfiguration>(config.GetSection("FaultHanderSettings"));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddSingleton<IWebApiPolicyFactory, WebApiPolicyFactory>();

            services
                .AddHttpClient<IProductWebApiClient, ProductWebApiClient>(client =>
                    client.BaseAddress = new Uri(providerRequestUri))
                .AddFaultHandlePolicies();
            services
               .AddHttpClient<ICartWebApiClient, CartWebApiClient>(client =>
                   client.BaseAddress = new Uri(providerRequestUri))
               .AddFaultHandlePolicies();
            services
               .AddHttpClient<ICheckoutWebApiClient, CheckoutWebApiClient>(client =>
                   client.BaseAddress = new Uri(providerRequestUri))
               .AddFaultHandlePolicies();
            services
            .AddHttpClient<IPurchaseWebApiClient, PurchaseWebApiClient>(client =>
                   client.BaseAddress = new Uri(providerRequestUri))
               .AddFaultHandlePolicies();

            return services;
        }
    }
}
