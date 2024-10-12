using AutoFixture;
using JobPost.Models;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities.Context;
using WebApi.Services.Repositories;

namespace JobPost.NUnit.Test.Repositories
{
    public class PostRepositoryTests
    {
        private PostRepository _repository { get; set; }
        private Mock<AppDbContext> _mock { get; set; } = new Mock<AppDbContext>();
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _repository = new PostRepository(_mock.Object);

        }

        [Test]
        public async Task GetAll_ValidCall()
        {
            //Arrange
            IEnumerable<Post> expected = _fixture.CreateMany<Post>();
            _mock.Setup(p => p.Posts).ReturnsDbSet(expected);

            //Act
            IEnumerable<Post> actual = await _repository.GetAll();

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Count(), Is.EqualTo(expected.Count()));
            Assert.That(expected, Is.EqualTo(actual).Using(new PostEqualityComparer()));
        }

        [Test]
        public async Task GetById_ValidCall()
        {
            //Arrange
            var mock = new Mock<AppDbContext>();
            var repository = new PostRepository(mock.Object);
            var allExpectedPosts = _fixture.CreateMany<Post>();
            var expected = allExpectedPosts.First();
            mock.Setup(x => x.Posts).ReturnsDbSet(allExpectedPosts);

            //Act
            var actual = await repository.GetById(expected.Id);

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(expected, Is.EqualTo(actual).Using(new PostEqualityComparer()));
        }

        [Test]
        public async Task GetById_NotFound_ReturnsNull()
        {
            //Arrange
            var mock = new Mock<AppDbContext>();
            var repository = new PostRepository(mock.Object);
            var allExpectedPosts = _fixture.CreateMany<Post>();
            var expected = allExpectedPosts.First();
            mock.Setup(x => x.Posts).ReturnsDbSet(allExpectedPosts);

            //Act
            var actual = await repository.GetById(new Guid());

            //Assert
            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task Insert_ValidCall()
        {
            //Arrange
            var mock = new Mock<AppDbContext>();
            var repository = new PostRepository(mock.Object);
            Post post = _fixture.Create<Post>();

            //Act
            var hej = await repository.Insert(post);

            //Assert
            mock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task UpdatePost_ValidCall()
        {
            //Arrange
            var mock = new Mock<AppDbContext>();
            var repository = new PostRepository(mock.Object);
            Post post = _fixture.Create<Post>();
            Post newPostValues = _fixture.Create<Post>();

            //Act
            post.Title = newPostValues.Title;
            post.Description = newPostValues.Description;
            post.City = newPostValues.City;
            post.EndDate = newPostValues.EndDate;
            await repository.Update(post);

            //Assert
            mock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task DeletePost_ValidCall()
        {
            //Arrange
            Post post = _fixture.Create<Post>();
            var mock = new Mock<AppDbContext>();
            var repository = new PostRepository(mock.Object);

            //Act
            await repository.Delete(post);

            //Assert
            mock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public void Exists_ShouldReturnTrue()
        {
            //Arrange
            var mock = new Mock<AppDbContext>();
            var repository = new PostRepository(mock.Object);
            var allExpectedPosts = _fixture.CreateMany<Post>();
            var expected = allExpectedPosts.First();
            mock.Setup(x => x.Posts).ReturnsDbSet(allExpectedPosts);

            //Act
            var actual = repository.Exists(expected.Id);

            //Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void Exists_ShouldReturnFalse()
        {
            //Arrange
            var mock = new Mock<AppDbContext>();
            var repository = new PostRepository(mock.Object);
            var allExpectedPosts = _fixture.CreateMany<Post>();
            var expected = allExpectedPosts.First();
            mock.Setup(x => x.Posts).ReturnsDbSet(allExpectedPosts);

            //Act
            var actual = repository.Exists(new Guid());

            //Assert
            Assert.That(actual, Is.False);
        }
    }
}
