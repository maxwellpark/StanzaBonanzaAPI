using StanzaBonanza.Models.Models;

namespace StanzaBonanza.DataAccess.Repositories
{
    public interface IAuthorPoemJoinService
    {
        Task<IEnumerable<AuthorPoemJoin>> GetAuthorPoemJoinAsync();
    }
}
