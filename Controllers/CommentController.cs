using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;

        public CommentController(ILogger<CommentController> logger)
        {
            _logger = logger;
        }

        [HttpGet("all")]
        public IEnumerable<Comment> All()
        {
            return Enumerable.Range(1, 5).Select(index => new Comment
            {
                Id = index,
                SolutionId = index,
                AuthorId = index,
                Content = "Comment content"
            })
            .ToArray();
        }
    }
}
