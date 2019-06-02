using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Messages.Api.Filters
{
    class RemoveModelsFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context) =>
            swaggerDoc.Definitions.Remove(nameof(Models.Message));
    }
}