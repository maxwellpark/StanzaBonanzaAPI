using StanzaBonanza.Models.Models;

namespace StanzaBonanza.DataAccess.Repositories.Interfaces
{
    public interface IPoem_AuthorRepository
    {
        Task<IEnumerable<Poem_Author>> GetAllAsync();
    }
}
