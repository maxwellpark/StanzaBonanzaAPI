using StanzaBonanza.Models;

namespace StanzaBonanza.DataAccess.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(int id);
    }
}
