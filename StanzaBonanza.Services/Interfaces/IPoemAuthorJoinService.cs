using StanzaBonanza.Models.Results;
using StanzaBonanza.Models.ResultSets;

namespace StanzaBonanza.Services.Interfaces
{
    public interface IPoemAuthorJoinService
    {
        Task<Poems_AuthorsJoinResultSet> GetPoems_AuthorsJoinResultSet();
    }
}
