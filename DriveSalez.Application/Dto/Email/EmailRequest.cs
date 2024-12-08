namespace DriveSalez.Application.Dto.Email;

public record EmailRequest(
    string ToAddress,
    string Subject,
    string? Body = "",
    string? AttachmentPath = "",
    bool IsHtml = false);