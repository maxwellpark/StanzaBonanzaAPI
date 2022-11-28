using Microsoft.EntityFrameworkCore;
using StanzaBonanza.DataAccess.DbContexts;
using StanzaBonanza.Models;

namespace StanzaBonanza.DataAccess.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _db;

        public AuthorRepository(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _db.Authors.FindAsync(id);
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _db.Authors.ToListAsync();
        }
    }
}
