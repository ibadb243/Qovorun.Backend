using Domain.Enums;

namespace Domain.Entities;

public class ShortnameField
{
    public Guid Id { get; set; }
    public ShortnameOwner Owner { get; set; }
    public Guid? OwnerId { get; set; }
    public string Shortname { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ChangedOwnerAt { get; set; }

    public ShortnameField() { }

    public ShortnameField(ShortnameOwner owner, Guid? ownerId, string shortname)
    {
        Id = Guid.NewGuid();
        Owner = owner;
        OwnerId = ownerId;
        Shortname = shortname;
        CreatedAt = DateTimeOffset.Now;
        ChangedOwnerAt = null;
    }
}