using WebApi.Entities;
using WebApi.Entities.Repositories;

namespace JobPost.NUnit.Test
{
    public class Tests
    {
        private PostRepository _repository{ get; set; }

        [SetUp]
        public void Setup()
        {
            //MockDbContext _mockContext = new MockDbContext();
            //this._repository = new PostRepository(_mockContext);

        }

        [Test]
        public void Employer()
        {
            // Assign
            

            //Act

            //Assert
            Assert.Pass();

        }
    }
}