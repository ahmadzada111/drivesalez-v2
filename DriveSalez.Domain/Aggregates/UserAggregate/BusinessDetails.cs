namespace DriveSalez.Domain.Aggregates.UserAggregate;

public class BusinessDetails
{
    public Guid Id { get; set; }
    
    public string BusinessName { get; private set; }
    
    public string Address { get; private set; }
    
    public string? Description { get; private set; }
    
    public Image? ProfileImage { get; private set; }
    
    public Image? BackgroundImage { get; private set; }
    
    private readonly List<WorkHour> _workHours = [];
    public IReadOnlyCollection<WorkHour> WorkHours => _workHours.AsReadOnly();
    
    private readonly List<ContactPhoneNumber> _phoneNumbers = [];
    public IReadOnlyCollection<ContactPhoneNumber> PhoneNumbers => _phoneNumbers.AsReadOnly();

    public BusinessDetails(string businessName, string address, string? description)
    {
        BusinessName = businessName;
        Address = address;
        Description = description;
    }

    public void UpdateDetails(string businessName, string address, string? description)
    {
        BusinessName = businessName;
        Address = address;
        Description = description;
    }

    public void UpdateWorkHours(List<WorkHour> newWorkHours)
    {
        if (newWorkHours.Count != 7)
            throw new ArgumentException("You must provide work hours for all 7 days.");
        _workHours.Clear();
        _workHours.AddRange(newWorkHours);
    }

    public void UpdatePhoneNumbers(List<ContactPhoneNumber> newPhoneNumbers)
    {
        _phoneNumbers.Clear();
        _phoneNumbers.AddRange(newPhoneNumbers);
    }

    public void UpdateProfileImage(Image image)
    {
        ProfileImage = image;
    }

    public void UpdateBackgroundImage(Image image)
    {
        BackgroundImage = image;
    }
}