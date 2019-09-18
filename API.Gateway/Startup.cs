using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace API.Gateway
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var authenticationProviderKey = "gatewayapi";
            services.AddAuthentication().AddIdentityServerAuthentication(authenticationProviderKey, options=> {
                options.Authority = "http://localhost:5000";
                options.ApiName = "gateway_api";
                options.SupportedTokens = SupportedTokens.Both;
                options.ApiSecret = "secret";
                options.RequireHttpsMetadata = false;
            });
            services.AddOcelot();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseOcelot().Wait();
        }
    }
}
