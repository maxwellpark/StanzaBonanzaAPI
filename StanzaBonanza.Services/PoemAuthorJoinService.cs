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

            if (join == null)
                throw new NullReferenceException("Join returned null when joining Authors on Poems");

            return join;
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

            var resultSet = new Poems_AuthorsJoinResultSet();

            foreach (var record in (await _poem_authorRepository.GetAllAsync().ConfigureAwait(false)))
            {
                if (!resultSet.JoinResults.TryGetValue(record.PoemId, out var result))
                {
                    result = new Poems_AuthorsJoinResult
                    {
                        Poem = new Poem(record.Poem.PoemId, record.Poem.Title, record.Poem.Body, record.Poem.CreatedDate),
                        Authors = new HashSet<Author>()
                    };
                    resultSet.JoinResults.Add(record.PoemId, result);
                }
                result.Authors.Add(authorDict[record.AuthorId]);
            }

            return resultSet;
        }
    }
}
