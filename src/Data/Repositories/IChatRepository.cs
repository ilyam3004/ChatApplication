using Data.Entities;

namespace Data.Repositories;

public interface IChatRepository : IRepository<Chat>
{
    Task<List<Chat>> SearchChats(string searchQuery);
}