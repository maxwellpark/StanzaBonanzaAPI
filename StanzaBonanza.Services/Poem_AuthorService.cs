﻿using StanzaBonanza.DataAccess.UnitOfWork;
using StanzaBonanza.Dtos.PoemDto;
using StanzaBonanza.Models.Models;
using StanzaBonanza.Models.Results;
using StanzaBonanza.Models.ResultSets;
using StanzaBonanza.Services.Exceptions;
using StanzaBonanza.Services.Interfaces;

namespace StanzaBonanza.Services
{
    public class Poem_AuthorService : IPoem_AuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public Poem_AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Gets all <see cref="Poem"/> records and all of their <see cref="Author"/>s for each result in the <see cref="Poems_AuthorsJoinResultSet"/>.
        /// Uses the Poems_Authors junction table.
        /// </summary>
        public async Task<Poems_AuthorsJoinResultSet> GetPoems_AuthorsJoinResultSet()
        {
            var authorsRepo = _unitOfWork.GetRepository<Author>();
            var authors = await authorsRepo.GetAllAsync().ConfigureAwait(false);
            var authorDict = authors.ToDictionary(a => a.AuthorId);

            var poemsRepo = _unitOfWork.GetRepository<Poem>();
            var poems = await poemsRepo.GetAllAsync().ConfigureAwait(false);

            var poem_authorsRepo = _unitOfWork.GetRepository<Poem_Author>();

            var resultSet = new Poems_AuthorsJoinResultSet
            {
                // Group junction entities by poem ID and create objects with each poem and its actors on 
                JoinResults = (await poem_authorsRepo.GetAllAsync().ConfigureAwait(false))
                    .GroupBy(pa => pa.PoemId)
                    .Select(group => new Poems_AuthorsJoinResult
                    {
                        Poem = new Poem(group.First().Poem.PoemId, group.First().Poem.Title, group.First().Poem.Body, group.First().Poem.CreatedDate),
                        Authors = new HashSet<Author>(group.Select(pa => authorDict[pa.AuthorId]))
                    })
                    .ToArray()
            };

            return resultSet;
        }

        public async Task<Poem> AddPoemAsync(PoemDto poemDto)
        {
            var poem = new Poem(poemDto);
            poem = await _unitOfWork.AddPoemAsync(poem);

            if (poem == null)
                throw new PoemAndAuthorCreateFailedException("Failed to create poem.");

            return poem;
        }
    }
}
