using Colonel.Shopping.Core;
using Colonel.Shopping.Models;
using Colonel.Shopping.Repositories;
using Colonel.Shopping.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Colonel.Shopping
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
            services.Configure<ProjectBaseUrlSettings>(
             Configuration.GetSection(nameof(ProjectBaseUrlSettings)));

            services.AddSingleton<IProjectBaseUrlSettings>(x =>
           x.GetRequiredService<IOptions<ProjectBaseUrlSettings>>().Value);

            services.Configure<BasketDatabaseSettings>(
                Configuration.GetSection(nameof(BasketDatabaseSettings)));

            services.AddSingleton<IBasketDatabaseSettings>(x =>
                x.GetRequiredService<IOptions<BasketDatabaseSettings>>().Value);

            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IStockService, StockService>();
            services.AddSingleton<IPriceService, PriceService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IBasketRepository, BasketRepository>();
            services.AddSingleton<IBasketService, BasketService>();
            services.AddSingleton<IEventPublisher, EventPublisher>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Colonel.Shopping", Version = "v1" });

            });

            services.AddLogging();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket Service");
            });

            app.UseMvc();

        }
    }
}
