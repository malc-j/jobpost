using JobPost.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Entities.Context
{
    public class AppDbContext : DbContext, IAppDbContext
    {

        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Employer> Employers { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public void MarkAsModified(Employer item)
        {
            throw new NotImplementedException();
        }

        public void MarkAsModified(Post item)
        {
            throw new NotImplementedException();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        IConfigurationRoot configuration = new ConfigurationBuilder()
        //           .SetBasePath(Directory.GetCurrentDirectory())
        //           .AddJsonFile("appsettings.json")
        //           .Build();
        //        var connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
        //        optionsBuilder.UseSqlServer(connectionString);
        //    }
        //}

    }


}
