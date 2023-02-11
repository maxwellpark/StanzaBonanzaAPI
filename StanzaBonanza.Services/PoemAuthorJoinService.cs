using StanzaBonanza.DataAccess.Repositories.Interfaces;
using StanzaBonanza.Models.Models;
using StanzaBonanza.Models.Results;
using StanzaBonanza.Models.ResultSets;
using StanzaBonanza.Services.Interfaces;

namespace StanzaBonanza.Services
{
    public class PoemAuthorJoinService : IPoemAuthorJoinService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IPoemRepository _poemRepository;
        private readonly IPoem_AuthorRepository _poem_authorRepository;

        public PoemAuthorJoinService(IAuthorRepository authorRepository, IPoemRepository poemRepository, IPoem_AuthorRepository poem_authorRepository)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _poemRepository = poemRepository ?? throw new ArgumentNullException(nameof(poemRepository));
            _poem_authorRepository = poem_authorRepository ?? throw new ArgumentNullException(nameof(poem_authorRepository));
        }

        public async Task<IEnumerable<AuthorPoemJoinResult>> GetPoems_AuthorsJoinAsync()
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

            // Get junction table records 
            var poems_authors = await _poem_authorRepository.GetAllAsync();
            var poems_authorsList = poems_authors.ToList();

            var resultSet = new Poems_AuthorsJoinResultSet();

            // Build result set 
            poems_authorsList.ForEach(record =>
            {
                var poemId = record.PoemId;

                if (!resultSet.JoinResults.Any(result => result.Poem.PoemId == poemId))
                {
                    // Get all authors for this poem and add to the result set
                    var poems_authorsForPoem = poems_authorsList.Where(record => record.PoemId == poemId).Select(match => match.Author);
                    resultSet.JoinResults.Add(new Poems_AuthorsJoinResult
                    {
                        Poem = new Poem(record.Poem.PoemId, record.Poem.Title, record.Poem.Body, record.Poem.CreatedDate),
                        Authors = poems_authorsForPoem.Select(pa => new Author(pa.AuthorId, pa.Name, pa.RegisteredDate))
                    });
                }
            });
            return resultSet;
        }
    }
}
