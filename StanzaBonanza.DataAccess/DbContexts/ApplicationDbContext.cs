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
        public DbSet<Poem_Author> Poems_Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define junction table relationships 
            modelBuilder.Entity<Poem_Author>()
                .HasOne(pa => pa.Poem)
                .WithMany(pa => pa.Poem_Authors)
                .HasForeignKey(pa => pa.PoemId);

            modelBuilder.Entity<Poem_Author>()
                .HasOne(pa => pa.Author)
                .WithMany(pa => pa.Poem_Authors)
                .HasForeignKey(pa => pa.AuthorId);

            // Seed Authors
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    AuthorId = 1,
                    Name = "Amy",
                    RegisteredDate = new DateTime(2022, 1, 1)
                }
            );
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    AuthorId = 2,
                    Name = "Bella",
                    RegisteredDate = new DateTime(2022, 1, 2)
                }
            );
            // Seed Poems
            modelBuilder.Entity<Poem>().HasData(
                new Poem
                {
                    PoemId = 1,
                    AuthorCreatorId = 1,
                    Title = "Foo",
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                }
            );
            modelBuilder.Entity<Poem>().HasData(
                new Poem
                {
                    PoemId = 2,
                    AuthorCreatorId = 2,
                    Title = "Bar",
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean ac laoreet mauris. Aliquam erat volutpat.",
                }
            );
            modelBuilder.Entity<Poem>().HasData(
                new Poem
                {
                    PoemId = 3,
                    AuthorCreatorId = 2,
                    Title = "Baz",
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean ac laoreet mauris. Aliquam erat volutpat. Duis id metus enim. Aenean scelerisque eros nibh",
                }
            );

            // Seed Poem_Authors 
            modelBuilder.Entity<Poem_Author>().HasData(
                new Poem_Author
                {
                    Poem_AuthorId = 1,
                    PoemId = 1,
                    AuthorId = 1
                }
            );
            modelBuilder.Entity<Poem_Author>().HasData(
                new Poem_Author
                {
                    Poem_AuthorId = 2,
                    PoemId = 1,
                    AuthorId = 2
                }
            );
            modelBuilder.Entity<Poem_Author>().HasData(
                new Poem_Author
                {
                    Poem_AuthorId = 3,
                    PoemId = 2,
                    AuthorId = 2
                }
            );
            modelBuilder.Entity<Poem_Author>().HasData(
                new Poem_Author
                {
                    Poem_AuthorId = 4,
                    PoemId = 3,
                    AuthorId = 1
                }
            );
            modelBuilder.Entity<Poem_Author>().HasData(
                new Poem_Author
                {
                    Poem_AuthorId = 5,
                    PoemId = 3,
                    AuthorId = 2
                }
            );
        }
    }
}
