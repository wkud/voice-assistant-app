using VoiceAssistant.Domain.Enums;

namespace VoiceAssistant.Application.Dtos.Users;

public record UpdateUserDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string EmailAddress { get; set; }
    public required UserAccountStatus Status { get; set; }
}
