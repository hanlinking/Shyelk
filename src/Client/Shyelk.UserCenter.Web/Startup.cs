using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shyelk.Infrastructure.Core.Security.Authentication;
using Shyelk.Infrastructure.Core.DependencyInjection;
using Shyelk.Infrastructure.Core.Data.EntityFramework;
using Shyelk.Infrastructure.Core.Caching.Redis;
using Shyelk.UserCenter.IService;
using System.Reflection;
using Microsoft.Extensions.FileProviders;
using AutoMapper;

namespace Shyelk.UserCenter.Web
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
            var path = AppContext.BaseDirectory;
            //SEDbContextManager.Initial("default",Configuration.GetConnectionString("LogDb"),DatabaseType.MySql,new string[]{path+"\\Shyelk.UserCenter.Entity"});
            SEDbContextManager.Initial("default", Configuration.GetConnectionString("LogDb"), DatabaseType.MySql
            , new string[] { "Shyelk.UserCenter.Entity" });
            services.AddRedisCache(Configuration.GetSection("redisConnection"));
            services.AddBaseService();
            services.AddAutoMapper();
            services.AddCors();
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseForwardedHeaders(new ForwardedHeadersOptions()
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseStaticFiles();
            // var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("luhanlin1@#$%^&#%"));

            // var options = new TokenProviderOptions
            // {
            //     Audience = "ExampleAudience",
            //     Issuer = "ExampleIssuer",
            //     SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
            // };
            // app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));
            // app.UseJwtBearerAuthentication(new JwtBearerOptions{

            // });
            app.UseCors(builder=>{
                builder.AllowAnyOrigin();
            });
            //app.UseOAuthTokenProvider();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}");
                routes.MapRoute(
                    name: "api",
                    template: "api/{controller}/{action}/{id?}"
                );
            });
        }
    }
}
