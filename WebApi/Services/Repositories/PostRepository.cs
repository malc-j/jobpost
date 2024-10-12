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
            int result = 0;
            try
            {
                _context.Remove(entity);
                result = await _context.SaveChangesAsync();
            }
            catch (Exception) { throw; }
            return result == 1;
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            IEnumerable<Post> result = new List<Post>();
            try
            {
                result = await _context.Posts.ToListAsync();
            }
            catch (Exception) { throw; }
            return result;
        }

        public async Task<Post?> GetById(Guid id)
        {
            Post post;
            try
            {
                post = await _context.Posts.Where(e => e.Id.ToString() == id.ToString()).FirstOrDefaultAsync();
            }
            catch (Exception) { throw; }
            return post;
        }

        public async Task<bool> Insert(Post entity)
        {
            var result = 0;
            try
            {
                _context.Add(entity);
                result = await _context.SaveChangesAsync();
            }
            catch (Exception) { throw; }
            return result == 1;

        }

        public async Task<bool> Update(Post entity)
        {
            int result = 0;
            try
            {
                _context.Update(entity);
                result = await _context.SaveChangesAsync();
            }
            catch (Exception) { throw; };
            return result == 1;
        }

        public bool Exists(Guid id)
        {
            bool result = false;
            try
            {
                result = _context.Posts.Any(e => e.Id == id);
            }
            catch (Exception) { throw; }
            return result;
        }
    }
}
