using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Asp.Versioning;
using NutriBreak.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;

namespace NutriBreak.TestsNew;

public abstract class TestBase
{
    protected NutriBreakDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<NutriBreakDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new NutriBreakDbContext(options);
    }

    protected DefaultHttpContext CreateVersionedHttpContext(string version = "1.0")
    {
        var http = new DefaultHttpContext();
        var parts = version.Split('.');
        var major = int.Parse(parts[0]);
        var minor = parts.Length > 1 ? int.Parse(parts[1]) : 0;
        http.Features.Set<IApiVersioningFeature>(new ApiVersioningFeature(http) { RequestedApiVersion = new ApiVersion(major, minor) });
        return http;
    }

    protected LinkGenerator CreateLinkGenerator()
    {
        var services = new ServiceCollection();
        services.AddRouting();
        return services.BuildServiceProvider().GetRequiredService<LinkGenerator>();
    }
}
