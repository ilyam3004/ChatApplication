namespace Data.Entities;

public class Chat
{
    public Guid ChatId { get; set; }
    public Guid ChatOwnerId { get; set; }
    public string ChatName { get; set; }

    public List<Message> Messages { get; set; }
}