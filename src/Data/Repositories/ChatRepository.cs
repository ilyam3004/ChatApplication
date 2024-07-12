using Microsoft.EntityFrameworkCore;
using Data.Entities;

namespace Data.Repositories;

public class ChatRepository(ApplicationDbContext context) 
    : Repository<Chat>(context), IChatRepository
{ 
    private readonly ApplicationDbContext _context = context;

    public async Task<List<Chat>> SearchChats(string searchQuery)
        => await _context.Chats.Where(c =>
            c.ChatName.Contains(searchQuery)).ToListAsync();

    public async Task<Chat?> GetChatWithUsers(Guid chatId)
        => await _context.Chats.Include(c =>
            c.Users).FirstOrDefaultAsync(c =>
            c.ChatId == chatId);
}