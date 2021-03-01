using DXP.SmartConnect.Ecom.Core.DTOs;
using DXP.SmartConnect.Ecom.Core.Entities;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace DXP.SmartConnect.Ecom.API.Configurations
{
    public static class MapsterConfiguration
    {
        public static void UseMapster(this IServiceCollection services)
        {
            var config = new TypeAdapterConfig();
            services.AddSingleton(config);

            RegisterModelMapping(config);

            services.AddScoped<IMapper, ServiceMapper>();
        }
        public static void RegisterModelMapping(TypeAdapterConfig config)
        {
            config.NewConfig<ProductSearchDto, ProductSearch>();
        }
    }
}
