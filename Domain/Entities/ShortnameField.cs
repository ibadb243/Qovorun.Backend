using Domain.Enums;

namespace Domain.Entities;

public class ShortnameField
{
    public Guid Id { get; set; }
    public ShortnameOwner Owner { get; set; }
    public Guid? OwnerId { get; set; }
    public string Shortname { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ChangedOwnerAt { get; set; }
}