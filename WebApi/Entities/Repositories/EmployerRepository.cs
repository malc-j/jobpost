using JobPost.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Entities.Repositories
{
    public class EmployerRepository : IEmployerRepository
    {
        private AppDbContext _context;

        public EmployerRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task Delete(Employer entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employer>> GetAll()
        {
            return await _context.Employers.ToListAsync();
            throw new NotImplementedException();
        }

        public async Task<Employer?> GetById(int id)
        {
            return await _context.Employers.Where(e => e.Id.ToString() == id.ToString()).FirstOrDefaultAsync();
            throw new NotImplementedException();
        }

        public async Task Insert(Employer entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public async Task Update(Employer entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            throw new NotImplementedException();
        }
    }
}
