using StanzaBonanza.DataAccess.UnitOfWork;
using StanzaBonanza.Models.Models;
using StanzaBonanza.Models.Results;
using StanzaBonanza.Models.ResultSets;
using StanzaBonanza.Services.Interfaces;

namespace StanzaBonanza.Services
{
    public class PoemAuthorJoinService : IPoemAuthorJoinService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PoemAuthorJoinService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IEnumerable<AuthorPoemJoinResult>> GetPoems_AuthorsJoinAsync()
        {
            var authorsRepo = _unitOfWork.GetRepository<Author>();
            var poemsRepo = _unitOfWork.GetRepository<Poem>();

            var authors = await authorsRepo.GetAllAsync().ConfigureAwait(false);
            var poems = await poemsRepo.GetAllAsync().ConfigureAwait(false);

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
            var authorsRepo = _unitOfWork.GetRepository<Author>();
            var authors = await authorsRepo.GetAllAsync().ConfigureAwait(false);
            var authorDict = authors.ToDictionary(a => a.AuthorId);

            var poemsRepo = _unitOfWork.GetRepository<Poem>();
            var poems = await poemsRepo.GetAllAsync().ConfigureAwait(false);

            var poem_authorsRepo = _unitOfWork.GetRepository<Poem_Author>();

            var resultSet = new Poems_AuthorsJoinResultSet
            {
                // Group junction entities by poem ID and create objects with each poem and its actors on 
                JoinResults = (await poem_authorsRepo.GetAllAsync().ConfigureAwait(false))
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
