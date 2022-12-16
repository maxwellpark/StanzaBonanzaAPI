using Microsoft.EntityFrameworkCore;
using StanzaBonanza.DataAccess.DbContexts;
using StanzaBonanza.Models.Models;

namespace StanzaBonanza.DataAccess.Repositories
{
    public class PoemRepository : IPoemRepository
    {
        private readonly ApplicationDbContext _db;

        public PoemRepository(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<Poem> GetByIdAsync(int id)
        {
            return await _db.Poems.FindAsync(id);
        }

        public async Task<IEnumerable<Poem>> GetAllAsync()
        {
            return await _db.Poems.ToListAsync();
        }
    }
}
