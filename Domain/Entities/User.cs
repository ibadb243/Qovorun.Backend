namespace Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Description { get; set; }
    public string PhoneNumber { get; set; }
    public string PasswordHash { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public User() { }

    public User(string firstname, string lastname, string description, string phoneNumber, string passwordHash)
    {
        Id = Guid.NewGuid();
        Firstname = firstname;
        Lastname = lastname;
        Description = description;
        PhoneNumber = phoneNumber;
        PasswordHash = passwordHash;
        CreatedAt = DateTimeOffset.Now;
        UpdatedAt = null;
        DeletedAt = null;
    }
}