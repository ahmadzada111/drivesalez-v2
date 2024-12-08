namespace DriveSalez.Application.Dto.User;

public record WorkHourRequest(
    DayOfWeek DayOfWeek, 
    TimeSpan? OpenTime, 
    TimeSpan? CloseTime, 
    bool IsClosed);