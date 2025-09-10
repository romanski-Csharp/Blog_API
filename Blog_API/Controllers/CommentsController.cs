using Blog_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private static List<Comment> comments = new List<Comment>();
        private static int nextId = 1;

        [HttpGet("{postId}")]
        public IActionResult GetByPost(int postId)
        {
            var list = comments.Where(c => c.PostId == postId).ToList();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Create(Comment comment)
        {
            comment.Id = nextId++;
            comments.Add(comment);
            return Ok(comment);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var comment = comments.FirstOrDefault(c => c.Id == id);
            if (comment == null) return NotFound();

            comments.Remove(comment);
            return Ok(new { message = "Comment deleted" });
        }
    }
}
