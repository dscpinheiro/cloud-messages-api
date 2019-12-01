using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Messages.Api.Filters
{
    class RemoveModelsFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context) => 
            swaggerDoc.Components.Schemas.Remove(nameof(Models.Message));
    }
}
