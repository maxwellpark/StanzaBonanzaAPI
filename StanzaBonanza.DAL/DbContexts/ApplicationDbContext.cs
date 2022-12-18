using Microsoft.EntityFrameworkCore;
using StanzaBonanza.Models.Models;

namespace StanzaBonanza.DataAccess.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Poem> Poems { get; set; }
        public DbSet<Author> Authors { get; set; }
        //public DbSet<AuthorPoemJunction> AuthorPoemJunctions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// Define junction
            //modelBuilder.Entity<AuthorPoemJunction>()
            //    .HasKey(junc => new { junc.AuthorId, junc.PoemId }); // Two PKs

            //modelBuilder.Entity<AuthorPoemJunction>()
            //    .HasOne(junc => junc.Author)
            //    .WithMany(auth => auth.)

            // Seed Authors
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = 1,
                    Name = "Amy",
                    RegisteredDate = new DateTime(2022, 1, 1)
                }
            );
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = 2,
                    Name = "Bella",
                    RegisteredDate = new DateTime(2022, 1, 2)
                }
            );
            // Seed Poems
            modelBuilder.Entity<Poem>().HasData(
                new Poem
                {
                    Id = 1,
                    AuthorCreatorId = 1,
                    Title = "Foo",
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                }
            );
            modelBuilder.Entity<Poem>().HasData(
                new Poem
                {
                    Id = 2,
                    AuthorCreatorId = 2,
                    Title = "Bar",
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean ac laoreet mauris. Aliquam erat volutpat.",
                }
            );
            modelBuilder.Entity<Poem>().HasData(
                new Poem
                {
                    Id = 3,
                    AuthorCreatorId = 2,
                    Title = "Baz",
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean ac laoreet mauris. Aliquam erat volutpat. Duis id metus enim. Aenean scelerisque eros nibh",
                }
            );
        }
    }
}
