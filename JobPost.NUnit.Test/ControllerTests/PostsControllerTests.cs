using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Entities;
using WebApi.Services.Repositories;

namespace JobPost.NUnit.Test.ControllerTests
{
    public class PostsControllerTests
    {
        private Mock<IPostRepository> _repository;
        private PostsController _controller { get; set; }
        private Fixture _fixture { get; set; }
        // TODO: Maybe make global, or somehow make "reusable" ----->
        public int _expectedBadRequestCode { get; private set; } = 400;
        public int _expectedGoodRequestCode { get; private set; } = 200;
        public int _expectedNotFoundCode { get; private set; } = 404;
        public int _expectedConflictCode { get; private set; } = 409;
        public int _expectedCreatedCode { get; private set; } = 201;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IPostRepository>();
            _fixture = new Fixture();
            _controller = new PostsController(_repository.Object);
        }


        [Test]
        public async Task GetPosts_OkRequest()
        {
            //Arrange
            var expected = _fixture.CreateMany<Post>(3);
            _repository.Setup(x => x.GetAll()).ReturnsAsync(expected);

            //Act
            var actionResult = await _controller.GetPosts();
            var actual = actionResult.Value;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Count(), Is.EqualTo(expected.Count()));
            Assert.That(expected, Is.EqualTo(actual).Using(new PostEqualityComparer()));

        }

        [Test]
        public async Task GetPosts_BadRequest()
        {
            //Arrange
            var expected = _fixture.CreateMany<Post>(3);
            _repository.Setup(x => x.GetAll()).Throws(new Exception());

            //Act
            var actionResult = await _controller.GetPosts();
            var actual = actionResult.Result as BadRequestResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedBadRequestCode));

        }

        [Test]
        public async Task GetPost_OkRequest()
        {
            //Arrange
            var expected = _fixture.Create<Post>();
            _repository.Setup(x => x.GetById(expected.Id)).ReturnsAsync(expected);

            //Act
            var actionResult = await _controller.GetPost(expected.Id);
            var actual = actionResult.Value;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual, Is.EqualTo(expected));

        }

        [Test]
        public async Task GetPost_NotFound()
        {
            //Arrange
            var post = _fixture.Create<Post>();
            _repository.Setup(x => x.GetById(post.Id));

            //Act
            var actionResult = await _controller.GetPost(post.Id);
            var actual = actionResult.Result as NotFoundResult;

            //Assert
            Assert.That(actionResult.Value, Is.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedNotFoundCode));
        }

        [Test]
        public async Task GetPost_BadRequest()
        {
            //Arrange
            var post = _fixture.Create<Post>();
            _repository.Setup(x => x.GetById(post.Id)).Throws(new Exception());

            //Act
            var actionResult = await _controller.GetPost(post.Id);
            var actual = actionResult.Result as BadRequestResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedBadRequestCode));
        }

        //[Test]
        public async Task UpdatePost_DifferentIds_ReturnsBadRequest()
        {
            //Arrange
            var post = _fixture.Create<Post>();

            //Act
            var actionResult = await _controller.PutPost(new Guid(), post);
            var actual = actionResult.Result as BadRequestResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedBadRequestCode));
        }

        [Test]
        public async Task UpdatePost_NoAffectedRows_ReturnsBadRequest()
        {
            //Arrange
            var post = _fixture.Create<Post>();
            _repository.Setup(x => x.Update(post)).ReturnsAsync(false);

            //Act
            var actionResult = await _controller.PutPost(post.Id, post);
            var actual = actionResult.Result as BadRequestResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedBadRequestCode));
        }

        [Test]
        public async Task UpdatePost_DbUpdateException_ReturnsNotFound()
        {
            //Arrange
            var post = _fixture.Create<Post>();
            _repository.Setup(x => x.Update(post)).Throws(new DbUpdateConcurrencyException());

            //Act
            var actionResult = await _controller.PutPost(post.Id, post);
            var actual = actionResult.Result as NotFoundResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedNotFoundCode));
        }

        [Test]
        public async Task UpdatePost_DbUpdateException_BadRequest()
        {
            //Arrange
            var post = _fixture.Create<Post>();
            _repository.Setup(x => x.Update(post)).Throws(new DbUpdateConcurrencyException());
            _repository.Setup(x => x.Exists(post.Id)).Returns(true);

            //Act
            var actionResult = await _controller.PutPost(post.Id, post);
            var actual = actionResult.Result as BadRequestResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedBadRequestCode));
        }

        [Test]
        public async Task UpdatePost_OkRequest()
        {
            //Arrange
            var post = _fixture.Create<Post>();
            _repository.Setup(x => x.Update(post)).ReturnsAsync(true);

            //Act
            var actionResult = await _controller.PutPost(post.Id, post);
            var actual = actionResult.Result as OkResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task CreatePost_AlreadyExists_ReturnsConflictResponse()
        {
            //Arrange
            var post = _fixture.Create<Post>();
            _repository.Setup(x => x.Insert(post)).Throws(new DbUpdateException());
            _repository.Setup(x => x.Exists(post.Id)).Returns(true);

            //Act
            var actionResult = await _controller.PostPost(post);
            var actual = actionResult.Result as ConflictResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedConflictCode));
        }

        [Test]
        public async Task CreatePost_DbUpdateException_BadRequest()
        {
            //Arrange
            var post = _fixture.Create<Post>();
            _repository.Setup(x => x.Insert(post)).Throws(new DbUpdateException());
            _repository.Setup(x => x.Exists(post.Id)).Returns(false);

            //Act
            var actionResult = await _controller.PostPost(post);
            var actual = actionResult.Result as BadRequestResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedBadRequestCode));
        }

        [Test]
        public async Task CreatePost_OkRequest()
        {
            //Arrange
            var post = _fixture.Create<Post>();
            _repository.Setup(x => x.Insert(post)).ReturnsAsync(true);

            //Act
            var actionResult = await _controller.PostPost(post);
            var actual = actionResult.Result as CreatedAtActionResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedCreatedCode));
            Assert.That(actual.Value, Is.EqualTo(post).Using(new PostEqualityComparer()));
        }


        [Test]
        public async Task DeletePost_NotFound()
        {
            //Arrange
            var post = _fixture.Create<Post>();
            _repository.Setup(x => x.GetById(post.Id));

            //Act
            var actionResult = await _controller.DeletePost(post.Id);
            var actual = actionResult as NotFoundResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedNotFoundCode));
        }

        [Test]
        public async Task DeletePost_BadRequest()
        {
            //Arrange
            var post = _fixture.Create<Post>();
            _repository.Setup(x => x.GetById(post.Id)).ReturnsAsync(post);
            _repository.Setup(x => x.Delete(post)).Throws(new Exception());

            //Act
            var actionResult = await _controller.DeletePost(post.Id);
            var actual = actionResult as BadRequestResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedBadRequestCode));
        }

        [Test]
        public async Task DeletePost_OkRequest()
        {
            //Arrange
            var post = _fixture.Create<Post>();
            _repository.Setup(x => x.GetById(post.Id)).ReturnsAsync(post);
            _repository.Setup(x => x.Delete(post));

            //Act
            var actionResult = await _controller.DeletePost(post.Id);
            var actual = actionResult as OkResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedGoodRequestCode));
        }
    }
}
