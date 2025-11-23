namespace NutriBreak.DTOs;

public record BreakRecordDto(decimal Id, decimal UserId, DateTime StartedAt, int DurationMinutes, string Type, string Mood, int EnergyLevel, int ScreenTimeMinutes);
public record CreateBreakRecordRequest(decimal Id, decimal UserId, int DurationMinutes, string Type, string Mood, int EnergyLevel, int ScreenTimeMinutes);
public record UpdateBreakRecordRequest(int DurationMinutes, string Type, string Mood, int EnergyLevel, int ScreenTimeMinutes);
