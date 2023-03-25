using StanzaBonanza.Models.ResultSets;

namespace StanzaBonanza.Services.Interfaces
{
    public interface IPoem_AuthorService
    {
        Task<Poems_AuthorsJoinResultSet> GetPoems_AuthorsJoinResultSet();
    }
}
