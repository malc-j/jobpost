using AutoFixture;
using JobPost.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Services.Repositories;

namespace JobPost.NUnit.Test.ControllerTests
{
    internal class EmployersControllerTests
    {
        private Mock<IEmployerRepository> _repository;
        private EmployersController _controller { get; set; }
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
            _repository = new Mock<IEmployerRepository>();
            _fixture = new Fixture();
            _controller = new EmployersController(_repository.Object);
        }


        [Test]
        public async Task GetEmployers_OkRequest()
        {
            //Arrange
            var expected = _fixture.CreateMany<Employer>(3);
            _repository.Setup(x => x.GetAll()).ReturnsAsync(expected);

            //Act
            var actionResult = await _controller.GetEmployers();
            var actual = actionResult.Value;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Count(), Is.EqualTo(expected.Count()));
            Assert.That(expected, Is.EqualTo(actual).Using(new EmployerEqualityComparer()));

        }

        [Test]
        public async Task GetEmployers_BadRequest()
        {
            //Arrange
            var expected = _fixture.CreateMany<Employer>(3);
            _repository.Setup(x => x.GetAll()).Throws(new Exception());

            //Act
            var actionResult = await _controller.GetEmployers();
            var actual = actionResult.Result as BadRequestResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedBadRequestCode));

        }

        [Test]
        public async Task GetEmployer_OkRequest()
        {
            //Arrange
            var expected = _fixture.Create<Employer>();
            _repository.Setup(x => x.GetById(expected.Id)).ReturnsAsync(expected);

            //Act
            var actionResult = await _controller.GetEmployer(expected.Id);
            var actual = actionResult.Value;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual, Is.EqualTo(expected));

        }

        [Test]
        public async Task GetEmployer_NotFound()
        {
            //Arrange
            var employer = _fixture.Create<Employer>();
            _repository.Setup(x => x.GetById(employer.Id));

            //Act
            var actionResult = await _controller.GetEmployer(employer.Id);
            var actual = actionResult.Result as NotFoundResult;

            //Assert
            Assert.That(actionResult.Value, Is.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedNotFoundCode));
        }

        [Test]
        public async Task GetEmployer_BadRequest()
        {
            //Arrange
            var employer = _fixture.Create<Employer>();
            _repository.Setup(x => x.GetById(employer.Id)).Throws(new Exception());

            //Act
            var actionResult = await _controller.GetEmployer(employer.Id);
            var actual = actionResult.Result as BadRequestResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedBadRequestCode));
        }

        [Test]
        public async Task UpdateEmployer_DifferentIds_ReturnsBadRequest()
        {
            //Arrange
            var employer = _fixture.Create<Employer>();

            //Act
            var actionResult = await _controller.PutEmployer(new Guid(), employer);
            var actual = actionResult.Result as BadRequestResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedBadRequestCode));
        }

        [Test]
        public async Task UpdateEmployer_NoAffectedRows_ReturnsBadRequest()
        {
            //Arrange
            var employer = _fixture.Create<Employer>();
            _repository.Setup(x => x.Update(employer)).ReturnsAsync(false);

            //Act
            var actionResult = await _controller.PutEmployer(employer.Id, employer);
            var actual = actionResult.Result as BadRequestResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedBadRequestCode));
        }

        [Test]
        public async Task UpdateEmployer_DbUpdateException_ReturnsNotFound()
        {
            //Arrange
            var employer = _fixture.Create<Employer>();
            _repository.Setup(x => x.Update(employer)).Throws(new DbUpdateConcurrencyException());

            //Act
            var actionResult = await _controller.PutEmployer(employer.Id, employer);
            var actual = actionResult.Result as NotFoundResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedNotFoundCode));
        }

        [Test]
        public async Task UpdateEmployer_DbUpdateException_BadRequest()
        {
            //Arrange
            var employer = _fixture.Create<Employer>();
            _repository.Setup(x => x.Update(employer)).Throws(new DbUpdateConcurrencyException());
            _repository.Setup(x => x.Exists(employer.Id)).Returns(true);

            //Act
            var actionResult = await _controller.PutEmployer(employer.Id, employer);
            var actual = actionResult.Result as BadRequestResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedBadRequestCode));
        }

        [Test]
        public async Task UpdateEmployer_OkRequest()
        {
            //Arrange
            var employer = _fixture.Create<Employer>();
            _repository.Setup(x => x.Update(employer)).ReturnsAsync(true);

            //Act
            var actionResult = await _controller.PutEmployer(employer.Id, employer);
            var actual = actionResult.Result as OkResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task CreateEmployer_AlreadyExists_ReturnsConflictResponse()
        {
            //Arrange
            var employer = _fixture.Create<Employer>();
            _repository.Setup(x => x.Insert(employer)).Throws(new DbUpdateException());
            _repository.Setup(x => x.Exists(employer.Id)).Returns(true);

            //Act
            var actionResult = await _controller.PostEmployer(employer);
            var actual = actionResult.Result as ConflictResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedConflictCode));
        }

        [Test]
        public async Task CreateEmployer_DbUpdateException_BadRequest()
        {
            //Arrange
            var employer = _fixture.Create<Employer>();
            _repository.Setup(x => x.Insert(employer)).Throws(new DbUpdateException());
            _repository.Setup(x => x.Exists(employer.Id)).Returns(false);

            //Act
            var actionResult = await _controller.PostEmployer(employer);
            var actual = actionResult.Result as BadRequestResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedBadRequestCode));
        }

        [Test]
        public async Task CreateEmployer_OkRequest()
        {
            //Arrange
            var employer = _fixture.Create<Employer>();
            _repository.Setup(x => x.Insert(employer)).ReturnsAsync(true);

            //Act
            var actionResult = await _controller.PostEmployer(employer);
            var actual = actionResult.Result as CreatedAtActionResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedCreatedCode));
            Assert.That(actual.Value, Is.EqualTo(employer).Using(new EmployerEqualityComparer()));
        }


        [Test]
        public async Task DeleteEmployer_NotFound()
        {
            //Arrange
            var employer = _fixture.Create<Employer>();
            _repository.Setup(x => x.GetById(employer.Id));

            //Act
            var actionResult = await _controller.DeleteEmployer(employer.Id);
            var actual = actionResult as NotFoundResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedNotFoundCode));
        }

        [Test]
        public async Task DeleteEmployer_BadRequest()
        {
            //Arrange
            var employer = _fixture.Create<Employer>();
            _repository.Setup(x => x.GetById(employer.Id)).ReturnsAsync(employer);
            _repository.Setup(x => x.Delete(employer)).Throws(new Exception());

            //Act
            var actionResult = await _controller.DeleteEmployer(employer.Id);
            var actual = actionResult as BadRequestResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedBadRequestCode));
        }

        [Test]
        public async Task DeleteEmployer_OkRequest()
        {
            //Arrange
            var employer = _fixture.Create<Employer>();
            _repository.Setup(x => x.GetById(employer.Id)).ReturnsAsync(employer);
            _repository.Setup(x => x.Delete(employer));

            //Act
            var actionResult = await _controller.DeleteEmployer(employer.Id);
            var actual = actionResult as OkResult;

            //Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(_expectedGoodRequestCode));
        }
    }
}
