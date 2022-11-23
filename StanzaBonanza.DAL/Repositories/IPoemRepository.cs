using StanzaBonanza.Models;

namespace StanzaBonanza.DataAccess.Repositories
{
    public interface IPoemRepository
    {
        Task<IEnumerable<Poem>> GetAllAsync();
    }
}
