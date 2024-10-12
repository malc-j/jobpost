using AutoFixture;
using JobPost.Models;
using Moq;
using Moq.EntityFrameworkCore;
using NuGet.Protocol;
using NUnit.Framework.Legacy;
using WebApi.Entities.Context;
using WebApi.Services.Repositories;


namespace JobPost.NUnit.Test.RepositoryTests
{
    public class EmployerRepositoryTests
    {
        private EmployerRepository _repository { get; set; }
        private Mock<AppDbContext> _mock { get; set; } = new Mock<AppDbContext>();
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
             _fixture = new Fixture();
            _repository = new EmployerRepository(_mock.Object);

        }

        [Test]
        public async Task GetAll_ValidCall()
        {
            //Arrange
            IEnumerable<Employer> expected = _fixture.CreateMany<Employer>();
            _mock.Setup(p => p.Employers).ReturnsDbSet(expected);

            //Act
            IEnumerable<Employer> actual = await _repository.GetAll();

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Count(), Is.EqualTo(expected.Count()));
            Assert.That(expected, Is.EqualTo(actual).Using(new EmployerEqualityComparer()));
        }

        [Test]
        public async Task GetById_ValidCall()
        {
            //Arrange
            var mock = new Mock<AppDbContext>();
            var repository = new EmployerRepository(mock.Object);
            var allExpectedEmployers = _fixture.CreateMany<Employer>();
            var expected = allExpectedEmployers.First();
            mock.Setup(x => x.Employers).ReturnsDbSet(allExpectedEmployers);

            //Act
            var actual = await repository.GetById(expected.Id);

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(expected, Is.EqualTo(actual).Using(new EmployerEqualityComparer()));
        }

        [Test]
        public async Task GetById_NotFound_ReturnsNull()
        {
            //Arrange
            var mock = new Mock<AppDbContext>();
            var repository = new EmployerRepository(mock.Object);
            var allExpectedEmployers = _fixture.CreateMany<Employer>();
            var expected = allExpectedEmployers.First();
            mock.Setup(x => x.Employers).ReturnsDbSet(allExpectedEmployers);

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
            var repository = new EmployerRepository(mock.Object);
            Employer employer = _fixture.Create<Employer>();

            //Act
            var hej = await repository.Insert(employer);

            //Assert
            mock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task UpdateEmployer_ValidCall()
        {
            //Arrange
            var mock = new Mock<AppDbContext>();
            var repository = new EmployerRepository(mock.Object);
            Employer employer = _fixture.Create<Employer>();
            Employer newEmployerValues = _fixture.Create<Employer>();

            //Act
            employer.Firstname = newEmployerValues.Firstname;
            employer.Lastname = newEmployerValues.Lastname;
            employer.Phone = newEmployerValues.Phone;
            employer.Email = newEmployerValues.Email;
            employer.CompanyName = newEmployerValues.CompanyName;
            await repository.Update(employer);

            //Assert
            mock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task DeleteEmployer_ValidCall()
        {
            //Arrange
            Employer employer = _fixture.Create<Employer>();
            var mock = new Mock<AppDbContext>();
            var repository = new EmployerRepository(mock.Object);

            //Act
            await repository.Delete(employer);

            //Assert
            mock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public void Exists_ShouldReturnTrue()
        {
            //Arrange
            var mock = new Mock<AppDbContext>();
            var repository = new EmployerRepository(mock.Object);
            var allExpectedEmployers = _fixture.CreateMany<Employer>();
            var expected = allExpectedEmployers.First();
            mock.Setup(x => x.Employers).ReturnsDbSet(allExpectedEmployers);

            //Act
            var actual =  repository.Exists(expected.Id);

            //Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void Exists_ShouldReturnFalse()
        {
            //Arrange
            var mock = new Mock<AppDbContext>();
            var repository = new EmployerRepository(mock.Object);
            var allExpectedEmployers = _fixture.CreateMany<Employer>();
            var expected = allExpectedEmployers.First();
            mock.Setup(x => x.Employers).ReturnsDbSet(allExpectedEmployers);

            //Act
            var actual = repository.Exists(new Guid());

            //Assert
            Assert.That(actual, Is.False);
        }

    }

}
