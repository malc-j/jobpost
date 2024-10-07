using JobPost.Models;
using Moq;
using Moq.EntityFrameworkCore;
using NuGet.Protocol;
using NUnit.Framework.Legacy;
using WebApi.Controllers;
using WebApi.Entities.Context;
using WebApi.Entities.Repositories;

namespace JobPost.NUnit.Test
{
    public class EmployerRepositoryTests
    {
        private EmployerRepository _repository { get; set; }
        private Mock<AppDbContext> _mock { get; set; } = new Mock<AppDbContext>();

        [SetUp]
        public void Setup()
        {
            _mock.Setup(p => p.Employers).ReturnsDbSet(GetSampleEmployers());
            this._repository = new EmployerRepository(_mock.Object);
        }

        [Test]
        public async Task GetAll_ValidCall()
        {
            //Arrange
            IEnumerable<Employer> expected = GetSampleEmployers();

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
            var expected = GetSampleEmployers().FirstOrDefault();

            //Act
            var actual = await _repository.GetById(new Guid("d57d2761-191b-4e91-844c-bcc7133ffb11"));

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(expected, Is.EqualTo(actual).Using(new EmployerEqualityComparer()));
        }

        [Test]
        public async Task Insert_ValidCall()
        {
            //Arrange
            var mock = new Mock<AppDbContext>();
            var repository = new EmployerRepository(mock.Object);

            //Act
            Employer employer = GetSampleEmployer();
            var hej = await _repository.Insert(employer);

            //Assert
            _mock.Verify(x => x.SaveChangesAsync(default(CancellationToken)), Times.Once);

        }

        [Test]
        public async Task UpdateEmployer_ValidCall() 
        {
            //Arrange
            var mock = new Mock<AppDbContext>();
            var repository = new EmployerRepository(mock.Object);

            //Act
            Employer employer = GetSampleEmployer();
            employer.Firstname = "NewFirstName";
            employer.Lastname = "NewLastName";
            employer.Phone = "00039283";
            employer.Email = "test@test,com";
            employer.CompanyName = "testCompany";
            await repository.Update(employer);

            //Assert
            mock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task DeleteEmployer_ValidCall()
        {
            //Arrange
            Employer employer = GetSampleEmployer();
            var mock = new Mock<AppDbContext>();
            var repository = new EmployerRepository(mock.Object);

            //Act
            await repository.Delete(employer);

            //Assert
            mock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }



        // Helper methods ---->
        private Employer GetSampleEmployer()
        {
            return new Employer
            {
                Firstname = "Luke",
                Lastname = "Shaw",
                Email = "lswah@test.com",
                CompanyName = "Testing Inc",
                Phone = "8725948572",
                CreatedAt = DateTime.Today,
            };
        }

        private List<Employer> GetSampleEmployers()
        {
            var output = new List<Employer> {

                new Employer
                {
                    Id = new Guid("d57d2761-191b-4e91-844c-bcc7133ffb11"),
                    Firstname = "Marcus",
                    Lastname = "Rashford",
                    CompanyName = "Burger King",
                    Email = "Marcusrashford@test.com",
                    Phone = "12334466",
                    CreatedAt = DateTime.Today,
                },
                new Employer
                {
                    Id = new Guid("0d324dd1-c9c3-4acb-bffa-eb7539386e04"),
                    Firstname = "Bruno",
                    Lastname = "Fernandes",
                    CompanyName = "Subway Inc",
                    Email = "brunobrunobruno@test.com",
                    Phone = "12334466",
                    CreatedAt = DateTime.Today,
                }
            };

            return output;
        }
       
    }

}
