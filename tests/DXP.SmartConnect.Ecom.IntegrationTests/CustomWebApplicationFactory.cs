using DXP.SmartConnect.Ecom.API;
using DXP.SmartConnect.Ecom.Core.Interfaces;
using DXP.SmartConnect.Ecom.Infrastructure.Data.Database;
using DXP.SmartConnect.Ecom.Infrastructure.Data.WebApiClients;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace DXP.SmartConnect.Ecom.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var host = builder.Build();

            host.Start();
            return host;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var connectString = "Data Source=dev-sql1.gothink.local;Initial Catalog=SandboxProduct;persist security info=True;user id=dev-sqlaccess;password=Sysnify5355;";
            builder
                .UseSolutionRelativeContentRoot("src/DXP.SmartConnect.Ecom.API")
                .ConfigureServices(services =>
                {
                    services.AddHttpClient<IProductWebApiClient, ProductWebApiClient>(client =>
                    client.BaseAddress = new Uri("https://storefrontgateway.unt.stg.v8.commerce.mi9cloud.com/api/"));
                    services.AddDbContext<DBContext>(options => options.UseSqlServer(connectString).EnableSensitiveDataLogging());
                });
        }
    }
}