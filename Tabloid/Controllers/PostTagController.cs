using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tabloid.Repositories;
using Tabloid.Models;


namespace Tabloid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostTagController : ControllerBase
    {
        private readonly IPostTagRepository _postTagRepository;

        public PostTagController(IPostTagRepository postTagRepository)
        {
            _postTagRepository = postTagRepository;
        }

        // POST api/<ValuesController>
        [HttpGet("{postId}")]
        public IActionResult Get(int postId)
        {
            try
            {
                var postTags = _postTagRepository.Get(postId);
                return Ok(postTags);
            }
            catch
            {
                return NoContent();
            }
        }
        [HttpPost]
        public IActionResult Update(PostTag postTag)
        {
            var postTags = _postTagRepository.Get(postTag.PostId);
            if (postTags.Any(tag => tag.TagId == postTag.TagId))
                try
                {
                    _postTagRepository.Delete(postTag);
                    return Ok(_postTagRepository.Get(postTag.PostId));
                }
                catch
                {
                    return BadRequest();
                }
            else
                try
                {
                    _postTagRepository.Add(postTag);
                    return Ok(_postTagRepository.Get(postTag.PostId));
                }
                catch
                {
                    return BadRequest();
                }
        }

       
        [HttpDelete("{id}")]
        public IActionResult Delete(PostTag postTag)
        {
            _postTagRepository.Delete(postTag);
            return NoContent();
        }
    }
}