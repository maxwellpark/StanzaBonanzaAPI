using StanzaBonanza.Models.Models;

namespace StanzaBonanza.DataAccess.Repositories.Interfaces
{
    public interface IAuthorPoemJoinService
    {
        Task<IEnumerable<AuthorPoemJoin>> GetAuthorPoemsJoinAsync();
    }
}
