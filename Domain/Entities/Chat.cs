using Domain.Entities.Abstract;

namespace Domain.Entities;

public class Chat : Conference
{
    public User FirstMember { get; set; }
    public User SecondMember { get; set; }

    public Chat() { }

    public Chat(User first, User second)
    {
        Id = Guid.NewGuid();
        FirstMember = first;
        SecondMember = second;
        CreatedAt = DateTimeOffset.Now;
        DeletedAt = null;
    }
}