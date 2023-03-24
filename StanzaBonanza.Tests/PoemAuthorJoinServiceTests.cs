using Moq;
using StanzaBonanza.DataAccess.UnitOfWork;
using StanzaBonanza.Models.Models;
using StanzaBonanza.Services;
using StanzaBonanza.Services.Interfaces;

namespace StanzaBonanza.Tests
{
    [TestFixture]
    public class PoemAuthorJoinServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private IPoemAuthorJoinService _service;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _service = new PoemAuthorJoinService(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task GetPoems_AuthorsJoinResultSet_ReturnsExpectedResult()
        {
            // Arrange
            var authors = new List<Author>
            {
                new Author { AuthorId = 1, Name = "John Doe" },
                new Author { AuthorId = 2, Name = "Jane Doe" }
            };
            var poems = new List<Poem>
            {
                new Poem { PoemId = 1, Title = "Poem 1", Body = "Lorem ipsum", AuthorCreatorId = 1 },
                new Poem { PoemId = 2, Title = "Poem 2", Body = "Dolor sit amet", AuthorCreatorId = 2 },
                new Poem { PoemId = 3, Title = "Poem 3", Body = "Consectetur adipiscing elit", AuthorCreatorId = 1 }
            };
            var poem_authors = new List<Poem_Author>
            {
                new Poem_Author { PoemId = 1, AuthorId = 1, Poem = poems[0], Author = authors[0] },
                new Poem_Author { PoemId = 2, AuthorId = 2, Poem = poems[1], Author = authors[1] },
                new Poem_Author { PoemId = 2, AuthorId = 1, Poem = poems[1], Author = authors[0] },
                new Poem_Author { PoemId = 3, AuthorId = 1, Poem = poems[2], Author = authors[0] }
            };
            _unitOfWorkMock.Setup(uow => uow.GetRepository<Author>().GetAllAsync()).ReturnsAsync(authors);
            _unitOfWorkMock.Setup(uow => uow.GetRepository<Poem>().GetAllAsync()).ReturnsAsync(poems);
            _unitOfWorkMock.Setup(uow => uow.GetRepository<Poem_Author>().GetAllAsync()).ReturnsAsync(poem_authors);

            var service = new PoemAuthorJoinService(_unitOfWorkMock.Object);

            // Act
            var result = await service.GetPoems_AuthorsJoinResultSet();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.JoinResults, Is.Not.Null);
            Assert.That(result.JoinResults, Has.Length.EqualTo(3));

            var joinResult1 = result.JoinResults[0];
            Assert.Multiple(() =>
            {
                Assert.That(joinResult1.Poem.PoemId, Is.EqualTo(1));
                Assert.That(joinResult1.Authors.Count, Is.EqualTo(1));
                Assert.That(joinResult1.Authors, Has.Member(authors[0]));
            });

            var joinResult2 = result.JoinResults[1];
            Assert.Multiple(() =>
            {
                Assert.That(joinResult2.Poem.PoemId, Is.EqualTo(2));
                Assert.That(joinResult2.Authors.Count(), Is.EqualTo(2));
                Assert.That(joinResult2.Authors, Has.Member(authors[0]));
                Assert.That(joinResult2.Authors, Has.Member(authors[1]));
            });

            var joinResult3 = result.JoinResults[2];
            Assert.Multiple(() =>
            {
                Assert.That(joinResult3.Poem.PoemId, Is.EqualTo(3));
                Assert.That(joinResult3.Authors.Count(), Is.EqualTo(1));
            });
        }
    }
}