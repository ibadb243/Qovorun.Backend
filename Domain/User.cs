namespace Domain;

public class User
{
    public Guid Id { get; set; }
    public string FirstnName { get; set; }
    public string LastName { get; set; }
    public string ShortName { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}