using JobPost.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Entities
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employer> Employers { get; set; }

        public DbSet<Post> Posts { get; set; }
    }
}
