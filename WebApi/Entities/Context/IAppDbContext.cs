using WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Entities.Context
{
    public interface IAppDbContext: IDisposable
    {
        DbSet<Employer> Employers { get; }
        DbSet<Post> Posts{ get; }
        int SaveChanges();
        void MarkAsModified(Employer item);
        void MarkAsModified(Post item);
    }
}
