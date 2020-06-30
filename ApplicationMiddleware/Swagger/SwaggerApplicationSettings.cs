using Microsoft.OpenApi.Models;

namespace ApplicationMiddleware.Swagger
{
    public class SwaggerApplicationSettings
    {
        public string ApplicationTitle {get; set;}
        public string ApplicationVersion { get; set; }
        public OpenApiContact OpenApiContact { get; set; }
    }
}
