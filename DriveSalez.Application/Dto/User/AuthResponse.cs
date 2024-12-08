namespace DriveSalez.Application.Dto.User;

public record AuthResponse(
    Guid Id, 
    string Token, 
    DateTimeOffset JwtExpiration, 
    string RefreshToken, 
    DateTimeOffset RefreshTokenExpiration);