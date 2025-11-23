using Microsoft.AspNetCore.Mvc;
using NutriBreak.Controllers.v1;
using NutriBreak.DTOs;
using NutriBreak.Persistence;

namespace NutriBreak.TestsNew;

public class UsersControllerTests : TestBase
{
    [Fact]
    public async Task CreateUser_ShouldReturnCreated()
    {
        var ctx = CreateDbContext();
        var http = CreateVersionedHttpContext();
        var controller = new UsersController(ctx, CreateLinkGenerator()) { ControllerContext = new ControllerContext { HttpContext = http } };
        var request = new CreateUserRequest(1.0m, "John Doe", "john@example.com", "remoto");
        var result = await controller.CreateUser(request);
        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(1.0m, created.RouteValues["id"]);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenMissing()
    {
        var ctx = CreateDbContext();
        var http = CreateVersionedHttpContext();
        var controller = new UsersController(ctx, CreateLinkGenerator()) { ControllerContext = new ControllerContext { HttpContext = http } };
        var response = await controller.GetById(999m);
        Assert.IsType<NotFoundResult>(response.Result);
    }
}
