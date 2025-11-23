namespace NutriBreak.DTOs;

public record MealDto(decimal Id, decimal UserId, string Title, int Calories, string TimeOfDay);
public record CreateMealRequest(decimal Id, decimal UserId, string Title, int Calories, string TimeOfDay);
public record UpdateMealRequest(string Title, int Calories, string TimeOfDay);
