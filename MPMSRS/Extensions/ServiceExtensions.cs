using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using MPMSRS.Helpers.Utilities;
using MPMSRS.Models.Entities;
using MPMSRS.Services.Interfaces;
using MPMSRS.Services.Interfaces.Auth;
using MPMSRS.Services.Interfaces.ICaller;
using MPMSRS.Services.Logger;
using MPMSRS.Services.Repositories;
using MPMSRS.Services.Repositories.Auth;
using MPMSRS.Services.Repositories.Caller;
using MPMSRS.Services.Repositories.Command;

namespace MPMSRS.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["sqlconnection:connectionString"];
            services.AddDbContext<RepositoryContext>(o => o.UseSqlServer(connectionString), ServiceLifetime.Transient);
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
              options.AddPolicy("Development", builder =>
              {
                      // Allow multiple HTTP methods  
                      builder.WithMethods("GET", "POST", "PATCH", "DELETE", "OPTIONS")
                        .WithHeaders(
                          HeaderNames.Accept,
                          HeaderNames.SetCookie,
                          HeaderNames.ContentType,
                          HeaderNames.Authorization)
                        .AllowCredentials()
                        .SetIsOriginAllowed(origin =>
                        {
                            if (string.IsNullOrWhiteSpace(origin)) return false;
                            if (origin.ToLower().StartsWith("http://localhost")) return true;
                            return false;
                        });
                  })
              );
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureMPMServices(this IServiceCollection services)
        {
            services.AddScoped<TokenMPMServices>();
            services.AddSingleton<RefreshTokenGenerator>();
            services.AddSingleton<RefreshTokenValidator>();
            services.AddScoped<Authenticator>();
            services.AddScoped<FileServices>();
            services.AddSingleton<TokenGenerator>();
            services.AddScoped<IRefreshTokenRepository, DatabaseRefreshTokenRepository>();
            services.AddScoped<MyEmailSenderRepository>();
            services.AddScoped<DatabaseRefreshTokenRepository>();
            services.AddTransient<IMyEmailSender, MyEmailSenderRepository>();
        }

        public static void ConfigureMPMClientService(this IServiceCollection services, IConfiguration config)
        {
            var clientConfig = config["MPMSettings:Uri"];
            var clientVpsConfig = config["VPSSettings:Uri"];
            services.AddSingleton<IVPSClient>(new VPSClient(clientVpsConfig));
            services.AddSingleton<IMPMClient>(new MPMClient(clientConfig));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MPM SRS API",
                    Description = "A Collections for swagger MPM SRS api information",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Mohammad Taufik Andrian",
                        Email = "taufikandrian18@gmail.com",
                        Url = new Uri("https://example.com"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under OpenApiLicense",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });
        }
    }
}
