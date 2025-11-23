using Microsoft.AspNetCore.Mvc;
using NutriBreak.Controllers.v1;
using NutriBreak.DTOs;
using NutriBreak.Persistence;

namespace NutriBreak.TestsNew;

public class MealsControllerTests : TestBase
{
    [Fact]
    public async Task CreateMeal_ShouldReturnCreated()
    {
        var ctx = CreateDbContext();
        var user = new NutriBreak.Domain.User { Name = "Jane", Email = "jane@example.com", WorkMode = "remoto" };
        ctx.Users.Add(user);
        await ctx.SaveChangesAsync();
        var http = CreateVersionedHttpContext();
        var controller = new MealsController(ctx, CreateLinkGenerator()) { ControllerContext = new ControllerContext { HttpContext = http } };
        var request = new CreateMealRequest(user.Id, "Almoço", 600, "lunch");
        var result = await controller.CreateMeal(request);
        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.NotNull(created.RouteValues["id"]);
    }
}
