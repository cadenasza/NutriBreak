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
        var request = new CreateUserRequest("John Doe", "john@example.com", "remoto");
        var result = await controller.CreateUser(request);
        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.NotNull(created.RouteValues["id"]);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenMissing()
    {
        var ctx = CreateDbContext();
        var http = CreateVersionedHttpContext();
        var controller = new UsersController(ctx, CreateLinkGenerator()) { ControllerContext = new ControllerContext { HttpContext = http } };
        var response = await controller.GetById(Guid.NewGuid());
        Assert.IsType<NotFoundResult>(response.Result);
    }
}
