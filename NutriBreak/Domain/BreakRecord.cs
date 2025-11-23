namespace NutriBreak.Domain;

public class BreakRecord
{
    public decimal Id { get; set; } // definido pelo usuário
    public decimal UserId { get; set; }
    public User? User { get; set; }
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public int DurationMinutes { get; set; }
    public string Type { get; set; } = string.Empty; // pausa curta, alongamento, alimentacao
    public string Mood { get; set; } = string.Empty; // humor reportado
    public int EnergyLevel { get; set; } // 1-10
    public int ScreenTimeMinutes { get; set; }
}
