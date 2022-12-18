using StanzaBonanza.Models.Models;

namespace StanzaBonanza.DataAccess.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(int id);
    }
}
