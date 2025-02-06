namespace Domain.Entities;

public class Message
{
    public Guid Id { get; set; }
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public Message() { }

    public Message(Group group, User user, string text) 
        : this(group.Id, user.Id, text) { }
    public Message(Guid groupId, User user, string text) 
        : this(groupId, user.Id, text) { }
    public Message(Group group, Guid userId, string text) 
        : this(group.Id, userId, text) { }
    public Message(Guid groupId, Guid userId, string text)
    {
        Id = Guid.NewGuid();
        GroupId = groupId;
        UserId = userId;
        Text = text;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = null;
        DeletedAt = null;
    }
}