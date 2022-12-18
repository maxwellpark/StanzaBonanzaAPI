using StanzaBonanza.Models.Models;

namespace StanzaBonanza.DataAccess.Repositories.Interfaces
{
    public interface IPoemRepository
    {
        Task<Poem> GetByIdAsync(int id);
        Task<IEnumerable<Poem>> GetAllAsync();
    }
}
