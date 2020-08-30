using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Autofac;
using CRM.API.Configuration;
using Microsoft.OpenApi.Models;
using AutoMapper;
using CRM.Core;
using Microsoft.Extensions.Logging;
using System;

namespace CRM.API
{  
    public class Startup
    {
        private IConfiguration Configuration { get; set; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
            if (!env.IsProduction())
            {
                builder.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            }
            Configuration = builder.Build();            
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Models.TokenOptions.ISSUER,

                        ValidateAudience = true,
                        ValidAudience = Models.TokenOptions.AUDIENCE,

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = Models.TokenOptions.GetSymmetricSecurityKey()
                    };
                });
            services.AddMvcCore();
            services.AddControllers();
            ConfigureDependencies(services);
            services.Configure<DatabaseOptions>(Configuration);
            services.Configure<UrlOptions>(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "CRM.API", Version = "v1" });
                c.IncludeXmlComments(String.Format(@"{0}\Swagger.XML", AppDomain.CurrentDomain.BaseDirectory));
            }
        );
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        protected virtual void ConfigureDependencies(IServiceCollection services)
        { }
        
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
                       
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();            
            app.UseAuthorization();            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());           
        }
    }
}
