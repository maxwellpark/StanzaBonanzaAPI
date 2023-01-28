using StanzaBonanza.Models.Models;
using StanzaBonanza.Models.Results;

namespace StanzaBonanza.Services.Interfaces
{
    public interface IAuthorPoemJoinService
    {
        Task<IEnumerable<AuthorPoemJoinResult>> GetAuthorPoemsJoinAsync();
    }
}
