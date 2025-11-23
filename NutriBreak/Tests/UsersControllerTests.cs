using Microsoft.EntityFrameworkCore;
using NutriBreak.Controllers.v1;
using NutriBreak.DTOs;
using NutriBreak.Persistence;
using Xunit;
using Microsoft.AspNetCore.Http;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;

namespace NutriBreak.Tests;

public class UsersControllerTests
{
    private NutriBreakDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<NutriBreakDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new NutriBreakDbContext(options);
    }

    private LinkGenerator CreateLinkGenerator()
    {
        var services = new ServiceCollection();
        services.AddRouting();
        return services.BuildServiceProvider().GetRequiredService<LinkGenerator>();
    }

    [Fact]
    public async Task CreateUser_ShouldReturnCreated()
    {
        var ctx = CreateDbContext();
        var httpContext = new DefaultHttpContext();
        httpContext.Features.Set<IApiVersioningFeature>(new ApiVersioningFeature(httpContext) { RequestedApiVersion = new ApiVersion(1,0) });
        var controller = new UsersController(ctx, CreateLinkGenerator()) { ControllerContext = new ControllerContext { HttpContext = httpContext } };
        var request = new CreateUserRequest("John Doe", "john@example.com", "remoto");
        var result = await controller.CreateUser(request);
        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.NotNull(created.RouteValues["id"]);
    }
}
