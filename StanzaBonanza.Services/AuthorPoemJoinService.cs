using StanzaBonanza.DataAccess.Repositories.Interfaces;
using StanzaBonanza.Models.Results;
using StanzaBonanza.Models.ResultSets;
using StanzaBonanza.Services.Interfaces;

namespace StanzaBonanza.Services
{
    // Todo: To be replaced with Unit Of Work 
    public class AuthorPoemJoinService : IAuthorPoemJoinService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IPoemRepository _poemRepository;
        private readonly IPoem_AuthorRepository _poem_authorRepository;

        public AuthorPoemJoinService(IAuthorRepository authorRepository, IPoemRepository poemRepository, IPoem_AuthorRepository poem_authorRepository)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _poemRepository = poemRepository ?? throw new ArgumentNullException(nameof(poemRepository));
            _poem_authorRepository = poem_authorRepository ?? throw new ArgumentNullException(nameof(poem_authorRepository));
        }

        public async Task<IEnumerable<AuthorPoemJoinResult>> GetAuthorPoemsJoinAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            var poems = await _poemRepository.GetAllAsync();

            var join = authors?.Join(poems, author => author?.AuthorId, poem => poem?.AuthorCreatorId, (author, poem) => new AuthorPoemJoinResult
            {
                Author = author,
                Poem = poem
            })?.ToList();

            if (join == null)
                throw new NullReferenceException("Join returned null when joining Authors on Poems");

            return join;
        }

        public async Task<Poems_AuthorsJoinResultSet> GetPoems_AuthorsJoinResultSet()
        {
            var authors = await _authorRepository.GetAllAsync();
            var poems = await _poemRepository.GetAllAsync();
            var poems_authors = await _poem_authorRepository.GetAllAsync();

            // Unoptimised 
            var resultSet = new Poems_AuthorsJoinResultSet();

            /*
             * select poems.*, Authors.* from poems 
            inner join Poems_Authors on Poems.PoemId = Poems_Authors.PoemId 
            inner join authors on Authors.AuthorId = Poems_Authors.AuthorId;
             * 
             */

            var query = from poem in poems join 

            return default;
        }
    }
}
