﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Colonel.Product.Models;
using Colonel.Product.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Colonel.Product
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
            services.Configure<ProductDatabaseSettings>(
                 Configuration.GetSection(nameof(ProductDatabaseSettings)));

            services.AddSingleton<IProductDatabaseSettings>(x =>
                x.GetRequiredService<IOptions<ProductDatabaseSettings>>().Value);

            services.AddSingleton<IProductRepository, ProductRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Colonel.Product", Version = "v1" });

            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IProductRepository _productRepository)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Service");
            });

            Task.Factory.StartNew(() => _productRepository.InitializeData());
            app.UseMvc();


        }
    }
}
