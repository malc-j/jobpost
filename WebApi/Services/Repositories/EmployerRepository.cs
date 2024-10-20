using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Entities.Context;

namespace WebApi.Services.Repositories
{
    public class EmployerRepository : IEmployerRepository
    {
        private AppDbContext _context;
        private ILogger<EmployerRepository> _logger;

        public EmployerRepository(AppDbContext context, ILogger<EmployerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Delete(Employer entity)
        {
            int result = 0;
            try
            {
                _context.Remove(entity);
                result = await _context.SaveChangesAsync();
            }
            catch (Exception e) {

                _logger.LogCritical(e, "Database operation failed!");
                throw;
            }
            return result == 1;
        }

        public async Task<IEnumerable<Employer>> GetAll()
        {
            IEnumerable<Employer> result = new List<Employer>();
            try
            {
                result = await _context.Employers.ToListAsync();
            }
            catch (Exception) { throw;}
            return result;
        }

        public async Task<Employer?> GetById(Guid id)
        {
            Employer employer;
            try
            {
                employer = await _context.Employers.Where(e => e.Id.ToString() == id.ToString()).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {

                _logger.LogCritical(e, "Database operation failed!");
                throw;
            }
            return employer ;
        }

        public async Task<bool> Insert(Employer entity)
        {
            var result = 0;
            try
            {
                _context.Add(entity);
                result = await _context.SaveChangesAsync();
            }
            catch (Exception) { throw;}
            return result == 1;
        }

        public async Task<bool> Update(Employer entity)
        {
            int result = 0;
            try
            {
                _context.Update(entity);
                result = await _context.SaveChangesAsync();
            }
            catch (Exception) { throw;};
            return result == 1;
        }

        public bool Exists(Guid id)
        {
            bool result = false;
            try
            {
                result = _context.Employers.Any(e => e.Id == id);
            }
            catch (Exception) { throw;}
            return result;
        }
    }
}
