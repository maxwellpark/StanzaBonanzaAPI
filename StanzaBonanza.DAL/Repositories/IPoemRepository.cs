using StanzaBonanza.Models;

namespace StanzaBonanza.DataAccess.Repositories
{
    public interface IPoemRepository
    {
        Task<Poem> GetByIdAsync(int id);
        Task<IEnumerable<Poem>> GetAllAsync();
    }
}
