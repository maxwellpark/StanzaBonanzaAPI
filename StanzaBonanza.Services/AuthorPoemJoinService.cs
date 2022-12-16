using StanzaBonanza.DataAccess.Repositories;
using StanzaBonanza.Models.Models;

namespace StanzaBonanza.Services
{
    // Todo: To be replaced with Unit Of Work 
    public class AuthorPoemJoinService : IAuthorPoemJoinService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IPoemRepository _poemRepository;

        public AuthorPoemJoinService(IAuthorRepository authorRepository, IPoemRepository poemRepository)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _poemRepository = poemRepository ?? throw new ArgumentNullException(nameof(poemRepository));
        }

        public async Task<IEnumerable<AuthorPoemJoin>> GetAuthorPoemJoinAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            var poems = await _poemRepository.GetAllAsync();

            var join = authors?.Join(poems, author => author?.Id, poem => poem?.AuthorId, (author, poem) => new AuthorPoemJoin
            {
                Author = author,
                Poem = poem
            })?.ToList();

            if (join == null)
                throw new NullReferenceException("Join returned null when joining Authors on Poems");

            return join;
        }
    }
}
