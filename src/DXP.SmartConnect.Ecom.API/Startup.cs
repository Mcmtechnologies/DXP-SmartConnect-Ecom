using DXP.SmartConnect.Ecom.Core.Interfaces;
using DXP.SmartConnect.Ecom.Core.Services;
using DXP.SmartConnect.Ecom.Core.Settings;
using DXP.SmartConnect.Ecom.Infrastructure.Extensions;
using DXP.SmartConnect.Ecom.SharedKernel.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace DXP.SmartConnect.Ecom.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ReadmeSettings>(Configuration.GetSection("ReadmeSettings"));

            services.AddControllers();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICheckoutService, CheckoutService>();

            // Default Infrastructure Service DI
            services.AddInfrastructureServiceConfig(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SmartConnect Ecom API",
                    Version = "v1",
                    Description = "A SmartConnect Ecom Mi9V8 ASP.NET Core Web API",
                    TermsOfService = new Uri(Configuration.GetSection("ApplicationSettings")["TermsOfService"]),
                    Contact = new OpenApiContact
                    {
                        Name = "Relationshop",
                        Email = string.Empty
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use Relationshop licence"
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Remove this extension if it affects performance in a production environment.
            app.UseApiRequestResponseLogging();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseHttpClientException();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "SmartConnect Ecom API");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
