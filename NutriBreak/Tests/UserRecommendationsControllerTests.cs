using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriBreak.Controllers.v2;
using NutriBreak.Domain;
using NutriBreak.Persistence;
using Xunit;

namespace NutriBreak.Tests;

public class UserRecommendationsControllerTests
{
    private NutriBreakDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<NutriBreakDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new NutriBreakDbContext(options);
    }

    [Fact]
    public async Task GetRecommendations_ShouldReturnExpectedSuggestion()
    {
        // Arrange
        var ctx = CreateDbContext();
        var user = new User { Name = "Alice", Email = "alice@example.com", WorkMode = "remoto" };
        ctx.Users.Add(user);
        ctx.Meals.Add(new Meal { UserId = user.Id, Title = "Almoço", Calories = 600, TimeOfDay = "lunch" });
        ctx.BreakRecords.Add(new BreakRecord { UserId = user.Id, StartedAt = DateTime.UtcNow.AddMinutes(-90), DurationMinutes = 10, Type = "pausa curta", Mood = "neutro", EnergyLevel = 6, ScreenTimeMinutes = 200 });
        await ctx.SaveChangesAsync();

        var controller = new UserRecommendationsController(ctx);

        // Act
        var actionResult = await controller.GetRecommendations(user.Id);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(actionResult);
        var value = ok.Value!;
        var type = value.GetType();

        string suggestedBreakType = type.GetProperty("suggestedBreakType")!.GetValue(value)?.ToString()!;
        string suggestedMeal = type.GetProperty("suggestedMeal")!.GetValue(value)?.ToString()!;
        int nextBreakInMinutes = (int)type.GetProperty("nextBreakInMinutes")!.GetValue(value)!;

        Assert.Equal("pausa curta", suggestedBreakType); // because last break was 90 mins ago (< 120)
        Assert.Equal("snack saudável", suggestedMeal); // last meal was lunch
        Assert.Equal(0, nextBreakInMinutes); // >60 mins since last break => immediate
    }

    [Fact]
    public async Task GetRecommendations_UserNotFound_ShouldReturnNotFound()
    {
        var ctx = CreateDbContext();
        var controller = new UserRecommendationsController(ctx);
        var result = await controller.GetRecommendations(Guid.NewGuid());
        Assert.IsType<NotFoundResult>(result);
    }
}
