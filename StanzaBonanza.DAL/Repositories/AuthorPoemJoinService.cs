using StanzaBonanza.DataAccess.DbContexts;
using StanzaBonanza.DataAccess.Repositories.Interfaces;
using StanzaBonanza.Models.Models;

namespace StanzaBonanza.DataAccess.Repositories
{
    // Todo: To be replaced with Unit Of Work 
    public class AuthorPoemJoinService : IAuthorPoemJoinService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IPoemRepository _poemRepository;

        public AuthorPoemJoinService(ApplicationDbContext db, IAuthorRepository authorRepository, IPoemRepository poemRepository)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _poemRepository = poemRepository ?? throw new ArgumentNullException(nameof(poemRepository));
        }

        public async Task<IEnumerable<AuthorPoemJoin>> GetAuthorPoemsJoinAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            var poems = await _poemRepository.GetAllAsync();

            var join = authors.Join(poems, author => author.Id, poem => poem.AuthorCreatorId, (author, poem) => new AuthorPoemJoin
            {
                Author = author,
                Poem = poem
            })?.ToList();

            return join;
        }
    }
}
