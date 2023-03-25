using Microsoft.EntityFrameworkCore;
using StanzaBonanza.DataAccess.DbContexts;
using StanzaBonanza.Models.Models;

namespace StanzaBonanza.DataAccess.Repositories
{
    public class PoemRepository : Repository<Poem>
    {
        private readonly ApplicationDbContext _db;

        public PoemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public override async Task<Poem> GetByIdAsync(int id)
        {
            return await _db.Poems.FindAsync(id);
        }

        public override async Task<IEnumerable<Poem>> GetAllAsync()
        {
            return await _db.Poems.ToListAsync();
        }

        public override async Task AddAsync(Poem entity)
        {
            await DbSet.AddAsync(entity);
        }
    }
}
