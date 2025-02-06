using Domain.Entities.Abstract;

namespace Domain.Entities;

public class Channel : Conference
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid? LinkedGroupId { get; set; } 
    public DateTimeOffset? UpdatedAt { get; set; }

    public Channel(string name, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        LinkedGroupId = null;
        CreatedAt = DateTimeOffset.Now;
        UpdatedAt = null;
        DeletedAt = null;
    }
}