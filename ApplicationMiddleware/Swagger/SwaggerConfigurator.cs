using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace ApplicationMiddleware.Swagger
{
    public static class SwaggerConfigurator
    {
        public static SwaggerApplicationSettings ApplicationSettings { get; private set; }
        public static SwaggerSecuritySettings SecuritySettings { get; private set; }

        public static IServiceCollection ConfigureSwaggerServices(this IServiceCollection services, SwaggerSecuritySettings securitySettings = null)
        {
            SecuritySettings = securitySettings;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    ApplicationSettings.ApplicationVersion,
                    new OpenApiInfo
                    {
                        Title = ApplicationSettings.ApplicationTitle,
                        Version = ApplicationSettings.ApplicationVersion,
                        Contact = ApplicationSettings.OpenApiContact
                    });

                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                if (SecuritySettings != null)
                {
                    c.AddSecurityDefinition(SecuritySettings.SecuritySchemeName, SecuritySettings.OpenApiSecurityScheme);
                    c.AddSecurityRequirement(SecuritySettings.OpenApiSecurityRequirement);
                }
            })
            .AddSwaggerGenNewtonsoftSupport();

            return services;
        }

        public static void ConfigureApplicationWithSwagger(this IApplicationBuilder app, SwaggerApplicationSettings settings)
        {
            ApplicationSettings = settings; 

            // Enable middleware to serve generated SwaggerConfigurator as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{ApplicationSettings.ApplicationVersion}/swagger.json", ApplicationSettings.ApplicationTitle);
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
