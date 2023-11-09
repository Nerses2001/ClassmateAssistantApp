using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ClassmateAssistantApp.Filters
{
    public class SwaggerAddEnumDescriptionsFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach(var p in swaggerDoc.Components.Schemas.Where(x=>x.Value?.Enum?.Count > 0)) 
            {
                IList<IOpenApiAny> propertyEnums = p.Value.Enum;
             
                if(propertyEnums != null && propertyEnums.Count > 0) 
                {
                    p.Value.Description += DescribeEnum(propertyEnums, p.Key);
                }

            }
            foreach (var pathItem in swaggerDoc.Paths.Values)
            {
                DescribeEnumParameters(pathItem.Operations, swaggerDoc);
            }
        }

        private string? DescribeEnum(IList<IOpenApiAny> enums, string proprtyTypeName)
        {
            List<string> enumDescriptions = new();
            Type enumType = GetEnumTypeByName(proprtyTypeName);
            if (enumType == null)
                return null;

            foreach (OpenApiInteger enumOption in enums.Cast<OpenApiInteger>())
            {
                int enumInt = enumOption.Value;


                var enumName = Enum.GetName(enumType, enumInt);
                enumDescriptions.Add(string.Format("{0} = {1}", enumInt, enumName));
            }

            return string.Join(", ", enumDescriptions.ToArray());
        }

        private void DescribeEnumParameters(IDictionary<OperationType, OpenApiOperation> operations, OpenApiDocument swaggerDoc)
        {
            if (operations != null)
            {
                foreach (var oper in operations)
                {
                    foreach (var param in oper.Value.Parameters)
                    {
                        var paramEnum = swaggerDoc.Components.Schemas.
                            FirstOrDefault(x => x.Key == param.Name);
                        
                        if (paramEnum.Value != null)
                        {
                            param.Description += DescribeEnum(paramEnum.Value.Enum, paramEnum.Key);
                        }
                    }
                }
            }
        }

        private Type GetEnumTypeByName(string enumTypeName) => AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .FirstOrDefault(x => x.Name == enumTypeName)!;
    }
}
