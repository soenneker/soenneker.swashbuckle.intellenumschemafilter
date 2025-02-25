using System;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Soenneker.Extensions.Enumerable;
using Soenneker.Reflection.Cache;
using Soenneker.Reflection.Cache.Fields;
using Soenneker.Reflection.Cache.Types;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Soenneker.Swashbuckle.IntellenumSchemaFilter;

/// <summary>
/// A Swashbuckle Schema filter for Intellenum
/// </summary>
public sealed class IntellenumSchemaFilter : ISchemaFilter
{
    private readonly ReflectionCache _reflectionCache;

    public IntellenumSchemaFilter()
    {
        _reflectionCache = new ReflectionCache();
    }

    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        Type? type = context.Type;

        CachedType cachedType = _reflectionCache.GetCachedType(type);

        if (!cachedType.IsIntellenum)
            return;

        CachedField[]? fields = cachedType.GetCachedFields();

        if (fields.IsNullOrEmpty())
            return;

        var openApiValues = new OpenApiArray();

        foreach (CachedField field in fields)
        {
            if (field.FieldInfo.FieldType.Name != cachedType.Type!.Name)
                continue;

            var enumValue = field.FieldInfo.GetValue(null)?.ToString();

            if (enumValue == null)
                continue;

            var openApiObj = new OpenApiString(enumValue);
            openApiValues.Add(openApiObj);
        }

        // See https://swagger.io/docs/specification/data-models/enums/
        schema.Type = "string";
        schema.Enum = openApiValues;
        schema.Properties = null;
    }
}