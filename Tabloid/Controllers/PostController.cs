using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tabloid.Models;
using Tabloid.Repositories;

namespace Tabloid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepo;
        private readonly IUserProfileRepository _upRepo;

        public PostController(
            IPostRepository postRepository,
            IUserProfileRepository userProfileRepository)
        {
            _postRepo = postRepository;
            _upRepo = userProfileRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_postRepo.GetAllPosts());
        }

        [HttpGet("{id}")]
        public IActionResult GetPost(int id)
        {
            var post = _postRepo.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public IActionResult Post(Post post)
        {
            if (string.IsNullOrWhiteSpace(post.Title))
            {
                post.Title = null;
                return NoContent();
            }

            _postRepo.Add(post);

            return CreatedAtAction("Get", new { id = post.Id }, post);
        }
    }
}
