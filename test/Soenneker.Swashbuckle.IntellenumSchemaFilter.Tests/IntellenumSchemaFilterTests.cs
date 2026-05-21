using Microsoft.OpenApi;
using Soenneker.Tests.HostedUnit;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Soenneker.Swashbuckle.IntellenumSchemaFilter.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class IntellenumSchemaFilterTests : HostedUnitTest
{

    public IntellenumSchemaFilterTests(Host host) : base(host)
    {

    }

    [Test]
    public void Default()
    {

    }

    [Test]
    public void Apply_should_ignore_schema_references()
    {
        var filter = new IntellenumSchemaFilter();
        var schema = new OpenApiSchemaReference("RequestDataOptions", new OpenApiDocument(), "3.0");
        var context = new SchemaFilterContext(typeof(string), null!, new SchemaRepository(), null, null);

        filter.Apply(schema, context);
    }
}
