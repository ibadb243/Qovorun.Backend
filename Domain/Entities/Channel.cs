using Domain.Entities.Abstract;

namespace Domain.Entities;

public class Channel : Conference
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid? LinkedGroupId { get; set; } 
    public DateTimeOffset? UpdatedAt { get; set; }
}