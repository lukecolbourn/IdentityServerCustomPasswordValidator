using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace LoginWeb
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryApiResources(new List<ApiResource> { new ApiResource("api1", "My API") })
                .AddInMemoryClients(new List<Client>
                {
                    new Client
                    {
                        ClientId = "client",

                        // resource owner uses MgsPasswordValidator
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                        // secret for authentication
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },

                        // scopes that client has access to
                        AllowedScopes = { "api1" }
                    }
                })
            ;

            services.AddTransient<IResourceOwnerPasswordValidator, CustomPasswordValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDeveloperExceptionPage();

            app.UseIdentityServer();

            

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
