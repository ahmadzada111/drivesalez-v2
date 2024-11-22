namespace DriveSalez.Shared.Dto.Dto.User;

public record WorkHourRequest(
    DayOfWeek DayOfWeek, 
    TimeSpan? OpenTime, 
    TimeSpan? CloseTime, 
    bool IsClosed);