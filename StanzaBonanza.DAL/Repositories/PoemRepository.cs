using Microsoft.EntityFrameworkCore;
using StanzaBonanza.DataAccess.DbContexts;
using StanzaBonanza.Models;

namespace StanzaBonanza.DataAccess.Repositories
{
    public class PoemRepository : IPoemRepository
    {
        private readonly ApplicationDbContext _db;

        public PoemRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Poem>> GetAllAsync()
        {
            return await _db.Poems.ToListAsync();
        }
    }
}
