using JobPost.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities.Context;

namespace WebApi.Entities.Repositories
{
    public class PostRepository : IPostRepository
    {
        private AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<int> Delete(Post entity)
        {
            _context.Posts.Remove(entity);
            return await _context.SaveChangesAsync();
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

        public async Task<int> Insert(Post entity)
        {
            _context.Posts.Add(entity);
            return await _context.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public async Task<int> Update(Post entity)
        {
            _context.Posts.Update(entity);
            return await _context.SaveChangesAsync();
            throw new NotImplementedException();
        }
    }
}
