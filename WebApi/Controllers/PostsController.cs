using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPost.Models;
using WebApi.Entities.Context;
using WebApi.Services.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _repository;

        public PostsController(IPostRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            IEnumerable<Post> result = new List<Post>();
            try
            {
                result = await _repository.GetAll();

            }
            catch (Exception) { return BadRequest(); }
            return result.ToList();
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post?>> GetPost(Guid id)
        {
            try
            {
                var post = await _repository.GetById(id);
                if (post == null)
                {
                    return NotFound();
                }
                return post;
            }
            catch (Exception)
            {
                // TODO: Better handling and response ----->
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutPost(Guid id, Post post)
        {
            if (id != post.Id) { return BadRequest(); }

            try
            {
                var result = await _repository.Update(post);

                if (!result)
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

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            try
            {
                await _repository.Insert(post);
            }
            catch (DbUpdateException)
            {
                if (_repository.Exists(post.Id))
                {
                    return Conflict();
                }
                else
                {
                    return BadRequest();
                }
            }
            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {

            var post = await _repository.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            try
            {
                await _repository.Delete(post);

            }
            catch (Exception)
            {

                return BadRequest();
            }

            return Ok();
        }

    }
}
