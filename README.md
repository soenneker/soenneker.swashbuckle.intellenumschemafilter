[![](https://img.shields.io/nuget/v/soenneker.swashbuckle.intellenumschemafilter.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.swashbuckle.intellenumschemafilter/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.swashbuckle.intellenumschemafilter/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.swashbuckle.intellenumschemafilter/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.swashbuckle.intellenumschemafilter.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.swashbuckle.intellenumschemafilter/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.swashbuckle.intellenumschemafilter/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.swashbuckle.intellenumschemafilter/actions/workflows/codeql.yml)

# Soenneker.Swashbuckle.IntellenumSchemaFilter

Generates string-enum OpenAPI schemas for Intellenum and `Soenneker.Gen.EnumValues` value types.

## Installation

```bash
dotnet add package Soenneker.Swashbuckle.IntellenumSchemaFilter
```

## Registration

```csharp
using Soenneker.Swashbuckle.IntellenumSchemaFilter;

builder.Services.AddSwaggerGen(options =>
{
    options.SchemaFilter<IntellenumSchemaFilter>();
});
```

## Example value type

```csharp
using Soenneker.Gen.EnumValues;

[EnumValue<string>]
public sealed partial class Orientation
{
    public static readonly Orientation Horizontal = new("horizontal");
    public static readonly Orientation Vertical = new("vertical");
}
```

With the filter registered, a property of type `Orientation` is represented as a string enum:

```yaml
type: string
enum:
  - horizontal
  - vertical
```

The values come from `ToString()` on static fields whose type exactly matches the generated enum-value type. The filter replaces the object-shaped schema properties with the discovered string values. It does not change runtime JSON serialization; configure the generator's serializer support separately when needed.
