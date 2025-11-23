using Microsoft.AspNetCore.Mvc;
using NutriBreak.Controllers.v1;
using NutriBreak.DTOs;
using NutriBreak.Persistence;

namespace NutriBreak.TestsNew;

public class BreakRecordsControllerTests : TestBase
{
    [Fact]
    public async Task CreateBreak_ShouldReturnCreated()
    {
        var ctx = CreateDbContext();
        var user = new NutriBreak.Domain.User { Name = "Joao", Email = "joao@example.com", WorkMode = "remoto" };
        ctx.Users.Add(user);
        await ctx.SaveChangesAsync();
        var http = CreateVersionedHttpContext();
        var controller = new BreakRecordsController(ctx, CreateLinkGenerator()) { ControllerContext = new ControllerContext { HttpContext = http } };
        var request = new CreateBreakRecordRequest(user.Id, 10, "pausa curta", "feliz", 7, 30);
        var result = await controller.CreateBreak(request);
        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.NotNull(created.RouteValues["id"]);
    }
}
