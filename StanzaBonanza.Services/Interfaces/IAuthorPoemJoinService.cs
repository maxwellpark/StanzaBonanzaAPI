using StanzaBonanza.Models.Results;
using StanzaBonanza.Models.ResultSets;

namespace StanzaBonanza.Services.Interfaces
{
    public interface IAuthorPoemJoinService
    {
        Task<IEnumerable<AuthorPoemJoinResult>> GetAuthorPoemsJoinAsync();
        Task<Poems_AuthorsJoinResultSet> GetPoems_AuthorsJoinResultSet();
    }
}
