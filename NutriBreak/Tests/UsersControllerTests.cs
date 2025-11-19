using Microsoft.EntityFrameworkCore;
using NutriBreak.Controllers.v1;
using NutriBreak.DTOs;
using NutriBreak.Persistence;
using Xunit;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Asp.Versioning;

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

    [Fact]
    public async Task CreateUser_ShouldReturnCreated()
    {
        var ctx = CreateDbContext();
        var httpContext = new DefaultHttpContext();
        httpContext.Features.Set<IApiVersioningFeature>(new ApiVersioningFeature() { RequestedApiVersion = new ApiVersion(1,0) });
        var controller = new UsersController(ctx, new LinkGeneratorStub()) { ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext { HttpContext = httpContext } };
        var request = new CreateUserRequest("John Doe", "john@example.com", "remoto");
        var result = await controller.CreateUser(request);
        Assert.NotNull(result);
    }

    private class LinkGeneratorStub : LinkGenerator
    {
        public override string? GetPathByAddress<TAddress>(HttpContext httpContext, TAddress address, RouteValueDictionary? values, RouteValueDictionary? ambientValues, PathString? pathBase, FragmentString fragment, LinkOptions? options) => "/stub";
        public override string? GetPathByAction(HttpContext httpContext, string? action, string? controller, object? values, PathString? pathBase, FragmentString fragment, LinkOptions? options) => "/stub";
        public override string? GetUriByAddress<TAddress>(HttpContext httpContext, TAddress address, RouteValueDictionary? values, RouteValueDictionary? ambientValues, string? scheme, HostString? host, PathString? pathBase, FragmentString fragment, LinkOptions? options) => "http://localhost/stub";
        public override string? GetUriByAction(HttpContext httpContext, string? action, string? controller, object? values, string? scheme, HostString? host, PathString? pathBase, FragmentString fragment, LinkOptions? options) => "http://localhost/stub";
    }
}
