namespace DriveSalez.Shared.Dto.Dto.User;

public record ConfirmEmailRequest(Guid UserId, string Token);