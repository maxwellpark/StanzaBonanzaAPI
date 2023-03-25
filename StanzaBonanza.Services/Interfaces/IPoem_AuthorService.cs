using StanzaBonanza.Dtos.PoemDto;
using StanzaBonanza.Models.Models;
using StanzaBonanza.Models.ResultSets;

namespace StanzaBonanza.Services.Interfaces
{
    public interface IPoem_AuthorService
    {
        Task<Poem> AddPoemAsync(PoemDto poemDto);
        Task<Poems_AuthorsJoinResultSet> GetPoems_AuthorsJoinResultSet();
    }
}
