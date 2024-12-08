namespace DriveSalez.Domain.Aggregates.UserAggregate;

public record WorkHour
{
    public DayOfWeek DayOfWeek { get; }
    public TimeSpan? OpenTime { get; }
    public TimeSpan? CloseTime { get; }
    public bool IsClosed { get; }

    public WorkHour(DayOfWeek dayOfWeek, TimeSpan? openTime, TimeSpan? closeTime, bool isClosed)
    {
        if (!isClosed && (openTime == null || closeTime == null))
            throw new ArgumentException("OpenTime and CloseTime must be provided if the business is open.");
        
        if (!isClosed && openTime >= closeTime)
            throw new ArgumentException("OpenTime must be earlier than CloseTime.");
        
        DayOfWeek = dayOfWeek;
        OpenTime = openTime;
        CloseTime = closeTime;
        IsClosed = isClosed;
    }
}