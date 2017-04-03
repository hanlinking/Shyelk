using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Shyelk.Infrastructure.Core.Data.EntityFramework;
using Shyelk.Infrastructure.Core.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Shyelk.UserCenter.Entity;
using Shyelk.Infrastructure.Core.Caching.Redis;

namespace Shyelk.UserCenter
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var result = ReflectionTools.GetSubTypes(typeof(IRepository<,>));
            //var rlt = typeof(BaseEntity<Guid>).IsAssignableFrom(typeof(User));
            // Add framework services.
            services.AddRedisCache(Configuration.GetSection("redisConnection"));
            //services.Configure<TokenProviderOptions>(Configuration.GetSection("TokenProviderOptions"));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions()
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            //var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("luhanlin1@#$%^&#%"));
            // app.UseOAuthTokenProvider();
            // var tokenValidationParameters = new TokenValidationParameters
            // {
            //     // The signing key must match!
            //     ValidateIssuerSigningKey = true,
            //     IssuerSigningKey = signingKey,

            //     // Validate the JWT Issuer (iss) claim
            //     ValidateIssuer = true,
            //     ValidIssuer = "uc.igidia.com",

            //     // Validate the JWT Audience (aud) claim
            //     ValidateAudience = true,
            //     ValidAudience = "Audience",

            //     // Validate the token expiry
            //     ValidateLifetime = true,

            //     // If you want to allow a certain amount of clock drift, set that here:
            //     ClockSkew = TimeSpan.Zero
            // };

            // app.UseCookieAuthentication(new CookieAuthenticationOptions
            // {
            //     AccessDeniedPath = "/values/AccessDenied",
            //     LoginPath = "/values/Login",
            //     AutomaticAuthenticate = true,
            //     AutomaticChallenge = true,
            //     AuthenticationScheme = "Cookie",
            //     CookieName = "access_token",
            //     TicketDataFormat = new ShyelkDataFormat(
            //         SecurityAlgorithms.HmacSha256,
            //         tokenValidationParameters)
            // });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
