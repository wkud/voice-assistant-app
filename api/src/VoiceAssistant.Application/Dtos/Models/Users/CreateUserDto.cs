namespace VoiceAssistant.Application.Dtos.Users;

public record CreateUserDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string EmailAddress { get; set; }
}
