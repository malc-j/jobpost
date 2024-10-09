using JobPost.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities.Context;

namespace WebApi.Services.Repositories
{
    public class EmployerRepository : IEmployerRepository
    {
        private AppDbContext _context;

        public EmployerRepository(AppDbContext context)
        {
            _context = context;

        }

        public async Task<int> Delete(Employer entity)
        {
            _context.Remove(entity);
            return await _context.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employer>> GetAll()
        {
            return await _context.Employers.ToListAsync();
            throw new NotImplementedException();
        }

        public async Task<Employer?> GetById(Guid id)
        {
            return await _context.Employers.Where(e => e.Id.ToString() == id.ToString()).FirstOrDefaultAsync();
            throw new NotImplementedException();
        }

        public async Task<int> Insert(Employer entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public async Task<int> Update(Employer entity)
        {
            _context.Update(entity);
            return await _context.SaveChangesAsync();
            throw new NotImplementedException();
        }
    }
}
