using Domain.Entities.Abstract;

namespace Domain.Entities;

public class Group : Conference
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}