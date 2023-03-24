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
            var authors = await _authorRepository.GetAllAsync().ConfigureAwait(false);
            var poems = await _poemRepository.GetAllAsync().ConfigureAwait(false);

            var join = authors
                .Join(poems, author => author?.AuthorId, poem => poem?.AuthorCreatorId, (author, poem) => new AuthorPoemJoinResult
                {
                    Author = author,
                    Poem = poem
                })
                .ToList();

            return join == null ? throw new NullReferenceException("Join returned null when joining Authors on Poems") : (IEnumerable<AuthorPoemJoinResult>)join;
        }

        /// <summary>
        /// Gets all <see cref="Poem"/> records and all of their <see cref="Author"/>s for each result in the <see cref="Poems_AuthorsJoinResultSet"/>.
        /// Uses the Poems_Authors junction table.
        /// </summary>
        public async Task<Poems_AuthorsJoinResultSet> GetPoems_AuthorsJoinResultSet()
        {
            var authors = await _authorRepository.GetAllAsync().ConfigureAwait(false);
            var authorDict = authors.ToDictionary(a => a.AuthorId);

            var poems = await _poemRepository.GetAllAsync().ConfigureAwait(false);

            var resultSet = new Poems_AuthorsJoinResultSet
            {
                JoinResults = (await _poem_authorRepository.GetAllAsync().ConfigureAwait(false))
                    .GroupBy(pa => pa.PoemId)
                    .Select(group => new Poems_AuthorsJoinResult
                    {
                        Poem = new Poem(group.First().Poem.PoemId, group.First().Poem.Title, group.First().Poem.Body, group.First().Poem.CreatedDate),
                        Authors = new HashSet<Author>(group.Select(pa => authorDict[pa.AuthorId]))
                    })
                    .ToArray()
            };

            return resultSet;
        }
    }
}
