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
    //public class PostRepositoryTests
    //{
    //    private PostRepository _repository { get; set; }
    //    private Mock<AppDbContext> _mock { get; set; } = new Mock<AppDbContext>();

    //    [SetUp]
    //    public void Setup()
    //    {

    //        _mock.Setup(p => p.Posts).ReturnsDbSet(GetSamplePosts());
    //        _repository = new PostRepository(_mock.Object);
    //    }

    //    [Test]
    //    public async Task GetAll_ValidCall()
    //    {
    //        //Arrange
    //        IEnumerable<Post> expected = GetSamplePosts();

    //        //Act
    //        IEnumerable<Post> actual = await _repository.GetAll();

    //        //Assert
    //        Assert.That(actual, Is.Not.Null);
    //        Assert.That(actual.Count(), Is.EqualTo(expected.Count()));
    //        Assert.That(expected, Is.EqualTo(actual).Using(new PostEqualityComparer()));
    //    }

    //    [Test]
    //    public async Task GetById_ValidCall()
    //    {
    //        //Arrange
    //        var expected = GetSamplePosts().FirstOrDefault();

    //        //Act
    //        var actual = await _repository.GetById(new Guid("f57e1313-ef91-43a3-8d13-0491fa61633b"));

    //        //Assert
    //        Assert.That(actual, Is.Not.Null);
    //        Assert.That(expected, Is.EqualTo(actual).Using(new PostEqualityComparer()));
    //    }

    //    [Test]
    //    public async Task Insert_ValidCall()
    //    {
    //        //Arrange
    //        var mock = new Mock<AppDbContext>();
    //        var repository = new PostRepository(mock.Object);

    //        //Act
    //        Post post = GetSamplePost();
    //        var hej = await _repository.Insert(post);

    //        //Assert
    //        _mock.Verify(x => x.SaveChangesAsync(default), Times.Once);

    //    }

    //    [Test]
    //    public async Task Update_ValidCall()
    //    {
    //        //Arrange
    //        var mock = new Mock<AppDbContext>();
    //        var repository = new PostRepository(mock.Object);

    //        //Act
    //        Post post = GetSamplePost();
    //        post.Title = "New Title";
    //        post.Description = "New Description";
    //        post.City = "New City";
    //        post.EndDate = DateTime.Now;
    //        await repository.Update(post);

    //        //Assert
    //        mock.Verify(x => x.SaveChangesAsync(default), Times.Once);
    //    }

    //    [Test]
    //    public async Task DeletePost_ValidCall()
    //    {
    //        //Arrange
    //        Post post = GetSamplePost();
    //        var mock = new Mock<AppDbContext>();
    //        var repository = new PostRepository(mock.Object);

    //        //Act
    //        await repository.Delete(post);

    //        //Assert
    //        mock.Verify(x => x.SaveChangesAsync(default), Times.Once);
    //    }



    //    // Helper methods ---->
    //    private Post GetSamplePost()
    //    {
    //        return new Post
    //        {
    //            Id = new Guid("f9e6e8bb-c067-4c22-a7e0-41577a9b4b4c"),
    //            EmployerId = new Guid("d57d2761-191b-4e91-844c-bcc7133ffb11"),
    //            Title = "Shaw",
    //            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam blandit interdum justo finibus fringilla. Mauris ac justo quis enim fermentum vulputate eleifend eget libero. Ut vel sagittis libero. In ante neque, posuere ac iaculis eu, ullamcorper et diam. Curabitur rhoncus odio non molestie iaculis. Nam vitae velit accumsan, sollicitudin erat gravida, pharetra ipsum. Etiam vitae eros lectus.",
    //            City = "Berlin",
    //            EndDate = DateTime.Today,
    //            CreatedAt = DateTime.Today,
    //        };
    //    }

    //    private List<Post> GetSamplePosts()
    //    {
    //        var output = new List<Post> {

    //            new Post
    //            {
    //                Id = new Guid("f57e1313-ef91-43a3-8d13-0491fa61633b"),
    //                EmployerId = new Guid("d57d2761-191b-4e91-844c-bcc7133ffb11"),
    //                Title = "Bartender",
    //                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam blandit interdum justo finibus fringilla. Mauris ac justo quis enim fermentum vulputate eleifend eget libero. Ut vel sagittis libero. In ante neque, posuere ac iaculis eu, ullamcorper et diam. Curabitur rhoncus odio non molestie iaculis. Nam vitae velit accumsan, sollicitudin erat gravida, pharetra ipsum. Etiam vitae eros lectus.",
    //                City = "London",
    //                EndDate = DateTime.Today,
    //                CreatedAt = DateTime.Today,
    //            },
    //            new Post
    //            {
    //                Id = new Guid("5a3ffdde-e3d9-4407-ae14-dc02828080c1"),
    //                EmployerId = new Guid("0d324dd1-c9c3-4acb-bffa-eb7539386e04"),
    //                Title = "Plumber",
    //                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam blandit interdum justo finibus fringilla. Mauris ac justo quis enim fermentum vulputate eleifend eget libero. Ut vel sagittis libero. In ante neque, posuere ac iaculis eu, ullamcorper et diam. Curabitur rhoncus odio non molestie iaculis. Nam vitae velit accumsan, sollicitudin erat gravida, pharetra ipsum. Etiam vitae eros lectus.",
    //                City = "London",
    //                EndDate = DateTime.Today,
    //                CreatedAt = DateTime.Today,
    //            }
    //        };

    //        return output;
    //    }
    //}
}
