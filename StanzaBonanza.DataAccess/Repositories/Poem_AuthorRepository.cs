﻿using Microsoft.EntityFrameworkCore;
using StanzaBonanza.DataAccess.DbContexts;
using StanzaBonanza.Models.Models;

namespace StanzaBonanza.DataAccess.Repositories
{
    public class Poem_AuthorRepository : Repository<Poem_Author>
    {
        private readonly ApplicationDbContext _db;

        public Poem_AuthorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public override async Task<IEnumerable<Poem_Author>> GetAllAsync()
        {
            return await _db.Poems_Authors.ToListAsync();
        }

        public override async Task<Poem_Author> GetByIdAsync(int id)
        {
            return await _db.Poems_Authors.FirstOrDefaultAsync(pa => pa.Poem_AuthorId == id);
        }

        public override async Task AddAsync(Poem_Author entity)
        {
            await DbSet.AddAsync(entity);
        }
    }
}
