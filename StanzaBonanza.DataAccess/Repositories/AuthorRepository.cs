using Microsoft.EntityFrameworkCore;
using StanzaBonanza.DataAccess.DbContexts;
using StanzaBonanza.Models.Models;

namespace StanzaBonanza.DataAccess.Repositories
{
    public class AuthorRepository : Repository<Author>
    {
        private readonly ApplicationDbContext _db;

        public AuthorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public override async Task<Author> GetByIdAsync(int id)
        {
            return await _db.Authors.FindAsync(id);
        }

        public override async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _db.Authors.ToListAsync();
        }

        public override async Task AddAsync(Author entity)
        {
            await DbSet.AddAsync(entity);
        }
    }
}
