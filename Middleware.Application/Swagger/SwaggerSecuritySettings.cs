using Microsoft.OpenApi.Models;

namespace Middleware.Application.Swagger
{
    public class SwaggerSecuritySettings
    {
        public string SecuritySchemeName { get; set; }
        public OpenApiSecurityScheme OpenApiSecurityScheme { get; set; }
        public OpenApiSecurityRequirement OpenApiSecurityRequirement { get; set; }
    }
}
