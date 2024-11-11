namespace DriveSalez.Shared.Dto.Dto.User;

public record AuthResponse(
    Guid Id, 
    string Token, 
    DateTimeOffset JwtExpiration, 
    string RefreshToken, 
    DateTimeOffset RefreshTokenExpiration);