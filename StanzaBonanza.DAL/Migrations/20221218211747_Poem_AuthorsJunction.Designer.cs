﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StanzaBonanza.DataAccess.DbContexts;

#nullable disable

namespace StanzaBonanza.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221218211747_Poem_AuthorsJunction")]
    partial class PoemAuthorsJunction
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StanzaBonanza.Models.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("RegisteredDate")
                        .HasColumnType("Date");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Amy",
                            RegisteredDate = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Name = "Bella",
                            RegisteredDate = new DateTime(2022, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("StanzaBonanza.Models.Models.Poem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorCreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("Date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Poems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorCreatorId = 1,
                            Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Foo"
                        },
                        new
                        {
                            Id = 2,
                            AuthorCreatorId = 2,
                            Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean ac laoreet mauris. Aliquam erat volutpat.",
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Bar"
                        },
                        new
                        {
                            Id = 3,
                            AuthorCreatorId = 2,
                            Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean ac laoreet mauris. Aliquam erat volutpat. Duis id metus enim. Aenean scelerisque eros nibh",
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Baz"
                        });
                });

            modelBuilder.Entity("StanzaBonanza.Models.Models.Poem_Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("PoemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PoemId");

                    b.ToTable("Poem_Authors");
                });

            modelBuilder.Entity("StanzaBonanza.Models.Models.Poem_Author", b =>
                {
                    b.HasOne("StanzaBonanza.Models.Models.Author", "Author")
                        .WithMany("Poem_Authors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StanzaBonanza.Models.Models.Poem", "Poem")
                        .WithMany("Poem_Authors")
                        .HasForeignKey("PoemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Poem");
                });

            modelBuilder.Entity("StanzaBonanza.Models.Models.Author", b =>
                {
                    b.Navigation("Poem_Authors");
                });

            modelBuilder.Entity("StanzaBonanza.Models.Models.Poem", b =>
                {
                    b.Navigation("Poem_Authors");
                });
#pragma warning restore 612, 618
        }
    }
}
