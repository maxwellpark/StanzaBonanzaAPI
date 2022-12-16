using StanzaBonanza.Models.Models;

namespace StanzaBonanza.Services.Interfaces
{
    public interface IAuthorPoemJoinService
    {
        Task<IEnumerable<AuthorPoemJoin>> GetAuthorPoemJoin();
    }
}
