using Blog_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private static List<Post> posts = new List<Post>();
        private static int nextId = 1;

        [HttpGet]
        public IActionResult GetAll() => Ok(posts);

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var post = posts.FirstOrDefault(p => p.Id == id);
            return post == null ? NotFound() : Ok(post);
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            post.Id = nextId++;
            posts.Add(post);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Post updated)
        {
            var post = posts.FirstOrDefault(p => p.Id == id);
            if (post == null) return NotFound();

            post.Title = updated.Title;
            post.Content = updated.Content;
            return Ok(post);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var post = posts.FirstOrDefault(p => p.Id == id);
            if (post == null) return NotFound();

            posts.Remove(post);
            return Ok(new { message = "Post deleted" });
        }
    }
}
