using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        [Authorize]
        public IActionResult Post(Post post)
        {
            if (string.IsNullOrWhiteSpace(post.Title))
            {
                post.Title = null;
                return NoContent();
            }
            var userId = GetCurrentUserProfileId();
            if (userId.HasValue)
            {
                post.UserProfileId = (int)userId;
                post.IsApproved = true;
                _postRepo.Add(post);


                return CreatedAtAction("Get", new { id = post.Id }, post);
            }
            return StatusCode(403);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            _postRepo.Delete(id);
            return NoContent();
        }

        //[HttpPut]
        //public IActionResult Put(int id, Post post)
        //{
        //    if (id != post.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _postRepo.Update(post);
        //    return NoContent();
        //}

        
        private int? GetCurrentUserProfileId()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                var user = _upRepo.GetByFirebaseUserId(claim.Value);
                    if(user != null)
                {
                    return user.Id;
                }
                return null;
            }
            return null;
        }
    }
}
