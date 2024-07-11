using Microsoft.EntityFrameworkCore;
using Data.Entities;

namespace Data.Repositories;

public class ChatRepository : IChatRepository
{ 
    private readonly ApplicationDbContext _context;
    
    public ChatRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Chat>> SearchChats(string searchQuery)
        => await _context.Chats.Where(c =>
            c.ChatName.Contains(searchQuery)).ToListAsync();
}