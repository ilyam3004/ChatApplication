using Microsoft.EntityFrameworkCore;
using Data.Entities;

namespace Data.Repositories;

public class MessageRepository(ApplicationDbContext context) 
    : Repository<Message>(context), IMessageRepository
{
    public Task<Message?> GetMessageWithUser(Guid messageId)
        => DbContext.Messages.Include(m => m.User)
            .FirstOrDefaultAsync(m =>
                m.MessageId == messageId);

    public async Task<List<Message>> GetChatMessages(Guid chatId)
        => await DbContext.Messages.Include(m => m.User)
            .Where(m => m.ChatId == chatId)
            .ToListAsync();
}