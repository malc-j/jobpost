﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPost.Models;
using WebApi.Entities.Context;
using WebApi.Services.MockData;
using WebApi.Services.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private readonly IEmployerRepository _repository;

        public EmployersController(IEmployerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employer>>> GetEmployers()
        {
            IEnumerable<Employer> list = new List<Employer>();
            try
            {
                list = await _repository.GetAll();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return list.ToList();       
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employer>> GetEmployer(Guid id)
        {
            try
            {
                var employer = await _repository.GetById(id);
                if (employer == null)
                {
                    return NotFound();
                }
                return employer;
            }
            catch (Exception)
            {
                // TODO: Better handling and response ----->
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutEmployer(Guid id, Employer employer)
        {
            if (id != employer.Id) {    return BadRequest(); }

            try
            {
               var result = await _repository.Update(employer);

                if(!result)
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Employer>> PostEmployer(Employer employer)
        {
            try
            {
                await _repository.Insert(employer);
            }
            catch (DbUpdateException)
            {
                if (_repository.Exists(employer.Id))
                {
                    return Conflict();
                }
                else
                {
                    return BadRequest();
                }
            }

            return CreatedAtAction("GetEmployer", new { id = employer.Id }, employer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployer(Guid id)
        {
            var employer = await _repository.GetById(id);
            if (employer == null)
            {
                return NotFound();
            }
            try
            {
               await _repository.Delete(employer);

            }
            catch (Exception)
            {

                return BadRequest();
            }

            return Ok();
        }

    }
}
