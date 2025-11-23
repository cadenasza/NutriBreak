namespace NutriBreak.DTOs;

public record UserDto(decimal Id, string Name, string Email, string WorkMode);
public record CreateUserRequest(decimal Id, string Name, string Email, string WorkMode);
public record UpdateUserRequest(string Name, string WorkMode);
