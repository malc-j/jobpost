using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPost.Models;
using WebApi.Entities;
using WebApi.Entities.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Employers
        [HttpGet]
        public async Task<IEnumerable<Employer>> GetEmployers()
        {
            var repo = new EmployerRepository(_context);
            return await repo.GetAll();
        }

        // GET: api/Employers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employer>> GetEmployer(Guid id)
        {
            var repo = new EmployerRepository(_context);
            var employer = await repo.GetById(id);

            if (employer == null)
            {
                return NotFound();
            }

            return employer;
        }

        // PUT: api/Employers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployer(Guid id, Employer employer)
        {
            var repo = new EmployerRepository(_context);
            if (id != employer.Id)
            {
                return BadRequest();
            }

            

            try
            {
                await repo.Update(employer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employer>> PostEmployer(Employer employer)
        {
            var repo = new EmployerRepository(_context);
            try
            {
                await repo.Insert(employer);
            }
            catch (DbUpdateException)
            {
                if (EmployerExists(employer.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmployer", new { id = employer.Id }, employer);
        }

        // DELETE: api/Employers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployer(Guid id)
        {
            var repo = new EmployerRepository(_context);
            var employer = await repo.GetById(id);
            if (employer == null)
            {
                return NotFound();
            }

            await repo.Delete(employer);

            return NoContent();
        }

        private bool EmployerExists(Guid id)
        {
            return _context.Employers.Any(e => e.Id == id);
        }
    }
}
