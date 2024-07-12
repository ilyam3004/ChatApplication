using Data.Entities;

namespace Data.Repositories;

public interface IMessageRepository : IRepository<Message>
{
    Task<Message?> GetMessageWithUser(Guid messageId);

    Task<List<Message>> GetChatMessages(Guid chatId);
}