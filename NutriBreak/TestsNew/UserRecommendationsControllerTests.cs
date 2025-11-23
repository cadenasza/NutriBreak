using Microsoft.AspNetCore.Mvc;
using NutriBreak.Controllers.v2;
using NutriBreak.Persistence;

namespace NutriBreak.TestsNew;

public class UserRecommendationsControllerTests : TestBase
{
    [Fact]
    public async Task Recommendations_ShouldReturnNotFound_IfUserMissing()
    {
        var ctx = CreateDbContext();
        var http = CreateVersionedHttpContext("2.0");
        var controller = new UserRecommendationsController(ctx) { ControllerContext = new ControllerContext { HttpContext = http } };
        var result = await controller.GetRecommendations(Guid.NewGuid());
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Recommendations_ShouldReturnOk_ForExistingUser()
    {
        var ctx = CreateDbContext();
        var user = new NutriBreak.Domain.User { Name = "Ana", Email = "ana@example.com", WorkMode = "remoto" };
        ctx.Users.Add(user);
        await ctx.SaveChangesAsync();
        var http = CreateVersionedHttpContext("2.0");
        var controller = new UserRecommendationsController(ctx) { ControllerContext = new ControllerContext { HttpContext = http } };
        var result = await controller.GetRecommendations(user.Id);
        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Contains("userId", ok.Value!.ToString());
    }
}
