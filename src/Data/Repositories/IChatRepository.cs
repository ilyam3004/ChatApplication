using Data.Entities;

namespace Data.Repositories;

public interface IChatRepository
{
    Task<List<Chat>> SearchChats(string searchQuery);
}