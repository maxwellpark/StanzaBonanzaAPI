using Microsoft.EntityFrameworkCore;
using StanzaBonanza.DataAccess.DbContexts;
using StanzaBonanza.DataAccess.Repositories.Interfaces;
using StanzaBonanza.Models.Models;

namespace StanzaBonanza.DataAccess.Repositories
{
    public class Poem_AuthorRepository : IPoem_AuthorRepository
    {
        private readonly ApplicationDbContext _db;

        public Poem_AuthorRepository(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<IEnumerable<Poem_Author>> GetAllAsync()
        {
            return await _db.Poems_Authors.ToListAsync();
        }
    }
}
