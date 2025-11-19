namespace NutriBreak.Domain;

public class Meal
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Calories { get; set; }
    public string TimeOfDay { get; set; } = string.Empty; // breakfast, lunch, etc
}
