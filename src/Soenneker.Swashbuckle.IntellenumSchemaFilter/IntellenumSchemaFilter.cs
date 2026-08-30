using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using Microsoft.OpenApi;
using Soenneker.Extensions.Enumerable;
using Soenneker.Reflection.Cache;
using Soenneker.Reflection.Cache.Fields;
using Soenneker.Reflection.Cache.Types;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Soenneker.Swashbuckle.IntellenumSchemaFilter;

/// <summary>
/// A Swashbuckle Schema filter for Intellenum and Soenneker.Gen.EnumValues
/// </summary>
public sealed class IntellenumSchemaFilter : ISchemaFilter
{
    private readonly ReflectionCache _reflectionCache;

    public IntellenumSchemaFilter()
    {
        _reflectionCache = new ReflectionCache();
    }
    
    /// <summary>
    /// Applies intellenum Schema Filter for the Intellenum Schema Filter.
    /// </summary>
    /// <param name="schema">Schema to read or generate.</param>
    /// <param name="context">HTTP context containing the Authorization header.</param>
    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema is not OpenApiSchema mutator)
            return;

        Type? type = context.Type;

        CachedType cachedType = _reflectionCache.GetCachedType(type);

        if (!cachedType.IsEnumValue)
            return;

        CachedField[]? fields = cachedType.GetCachedFields();

        if (fields.IsNullOrEmpty())
            return;

        var openApiValues = new List<JsonNode>();

        foreach (CachedField field in fields)
        {
            if (!field.FieldInfo.IsStatic || field.FieldInfo.FieldType != cachedType.Type)
                continue;

            var enumValue = field.FieldInfo.GetValue(null)?.ToString();

            if (enumValue == null)
                continue;

            openApiValues.Add(JsonValue.Create(enumValue));
        }

        // See https://swagger.io/docs/specification/data-models/enums/
        mutator.Type = JsonSchemaType.String;
        mutator.Enum = openApiValues;
        mutator.Properties = null;
    }
}
