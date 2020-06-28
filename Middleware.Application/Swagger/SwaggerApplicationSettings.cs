using Microsoft.OpenApi.Models;

namespace Middleware.Application.Swagger
{
    public class SwaggerApplicationSettings
    {
        public string ApplicationTitle {get; set;}
        public string ApplicationVersion { get; set; }
        public OpenApiContact OpenApiContact { get; set; }
    }
}
