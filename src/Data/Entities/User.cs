namespace Data.Entities;

public class User
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    
    public List<Message> Messages { get; set; }
}