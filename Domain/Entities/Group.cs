using Domain.Entities.Abstract;

namespace Domain.Entities;

public class Group : Conference
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Group() { }

    public Group(string name, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = null;
        DeletedAt = null;
    }
}