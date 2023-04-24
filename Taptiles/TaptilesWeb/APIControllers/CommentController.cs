using Microsoft.AspNetCore.Mvc;
using Taptiles.Entity;
using Taptiles.Service;

namespace TaptilesWeb.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentService _commentService = new CommentServiceEF();

        [HttpGet]
        public IEnumerable<Coment> GetComment()
        {
            return _commentService.GetLastComment();
        }

        [HttpPost]
        public void PostComment(Coment coment)
        {
            coment.PlayedAt = DateTime.Now;
            _commentService.AddComment(coment);
        }
    }
}
