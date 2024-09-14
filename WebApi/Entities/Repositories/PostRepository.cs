using JobPost.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Entities.Repositories
{
    public class PostRepository : IPostRepository
    {
        private AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task Delete(Post entity)
        {
            _context.Posts.Remove(entity);
            await _context.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _context.Posts.ToListAsync();
            throw new NotImplementedException();
        }

        public async Task<Post?> GetById(Guid id)
        {
            return await _context.Posts.Where(p => p.Id== id).FirstOrDefaultAsync();
            throw new NotImplementedException();
        }

        public async Task Insert(Post entity)
        {
            _context.Posts.Add(entity);
            await _context.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public Task Update(Post entity)
        {
            _context.Posts.Update(entity);
            _context.SaveChanges();
            throw new NotImplementedException();
        }
    }
}
