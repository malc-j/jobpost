using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Entities.Context;
using WebApi.Services.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _repository;
        private readonly ILogger<PostsController> _logger;

        public PostsController(IPostRepository repository, ILogger<PostsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            IEnumerable<Post> result = new List<Post>();
            try
            {
                result = await _repository.GetAll();

            }
            catch (Exception e) { 
                _logger.LogError(e, "Bad exception caught at {0}", DateTime.UtcNow); 
                return BadRequest(); 
            }
            return result.ToList();
        }

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
            catch (Exception e)
            {
                _logger.LogError(e, "Bad exception caught at {0}", DateTime.UtcNow);
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
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogError(e, "Concurrency exception caught at {0}", DateTime.UtcNow);
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
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            try
            {
                await _repository.Insert(post);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, "Bad Update exception caught at {0}", DateTime.UtcNow);
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
            catch (Exception e)
            {
                _logger.LogError(e, "Bad exception caught at {0}", DateTime.UtcNow);
                return BadRequest();
            }

            return Ok();
        }

    }
}
