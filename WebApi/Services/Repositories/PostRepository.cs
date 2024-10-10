using JobPost.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities.Context;

namespace WebApi.Services.Repositories
{
    public class PostRepository : IPostRepository
    {
        private AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(Post entity)
        {
            var result = 0;
            _context.Remove(entity);
            result = await _context.SaveChangesAsync();
            return result == 1;
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _context.Posts.ToListAsync();
            throw new NotImplementedException();
        }

        public async Task<Post?> GetById(Guid id)
        {
            return await _context.Posts.Where(p => p.Id == id).FirstOrDefaultAsync();
            throw new NotImplementedException();
        }

        public async Task<bool> Insert(Post entity)
        {
            _context.Posts.Add(entity);
             var result = await _context.SaveChangesAsync();
            return result == 1;

        }

        public async Task<bool> Update(Post entity)
        {
            _context.Update(entity);
            var result = await _context.SaveChangesAsync();
            return result == 1;
        }

        public bool Exists(Guid id)
        {
            return _context.Employers.Any(e => e.Id == id);
        }
    }
}
