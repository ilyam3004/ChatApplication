namespace Data.Entities;

public class Message
{
    public Guid MessageId { get; set; }
    public Guid ChatId { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; }
    public DateTime Date { get; set; }

    public Chat Chat { get; set; }
    public User User { get; set; }
}