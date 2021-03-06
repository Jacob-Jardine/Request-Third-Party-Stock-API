using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RequestStockService.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace RequestStockService
{
    public class Startup
    {
        private IWebHostEnvironment _environment = null;
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers();
            if (_environment.IsDevelopment())
            {
                services.AddSingleton<IThirdPartyStockRepository, FakeThirdPartyStockRepository>();
                //services.AddHttpClient<IPurchaseRequestRepository, SendPurchaseRequestRepository>();
            }
            else if (_environment.IsStaging() || _environment.IsProduction())
            {
                services.AddHttpClient<IThirdPartyStockRepository, ThirdPartyStockRepository>();
            }
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
                options.Audience = Configuration["Auth0:Audience"];
            });
            services.AddAuthorization(o =>
            {
                o.AddPolicy("ReadThirdPartyStock", policy =>
                    policy.RequireClaim("permissions", "read:tps_stock"));
                o.AddPolicy("SendThirdPartyRequest", policy =>
                    policy.RequireClaim("permissions", "add:tps_request"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
