namespace Data.Entities;

public class User
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string ConnectionId { get; set; }
    public Guid ChatId { get; set; }
    
    public Chat Chat { get; set; }
    public List<Message> Messages { get; set; }
}