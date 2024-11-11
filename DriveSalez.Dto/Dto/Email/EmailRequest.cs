namespace DriveSalez.Shared.Dto.Dto.Email;

public record EmailRequest(
    string ToAddress,
    string Subject,
    string? Body = "",
    string? AttachmentPath = "",
    bool IsHtml = false);