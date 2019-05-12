using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Data;
using Checkout.Data.Interfaces;
using Checkout.Models.Mappers;
using Checkout.Models.Mappers.Interfaces;
using Checkout.Services;
using Checkout.Services.Interfaces;
using Checkout.Wrappers;
using Checkout.Wrappers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Checkout
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IBasketService, BasketService>();
            services.AddSingleton<IItemMapper, ItemMapper>();
            services.AddSingleton<IBasketMapper, BasketMapper>();
            services.AddSingleton<IBasketStore, BasketStore>();
            services.AddSingleton<IItemStore, ItemStore>();
            services.AddSingleton<IGuidWrapper, GuidWrapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
