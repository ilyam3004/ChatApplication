namespace Data.Entities;

public class Chat
{
    public Guid ChatId { get; set; }
    public Guid ChatOwnerId { get; set; }
    public string ChatName { get; set; } = null!;

    public List<Message> Messages { get; set; } = null!;
    public List<User> Users { get; set; } = null!;
}