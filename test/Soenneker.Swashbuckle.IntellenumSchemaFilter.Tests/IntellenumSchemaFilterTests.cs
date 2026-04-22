using Soenneker.Tests.HostedUnit;

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
}
