namespace DriveSalez.Application.Dto.User;

public record ConfirmEmailRequest(Guid UserId, string Token);