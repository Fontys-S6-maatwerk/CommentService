using CommentService.Models;
using CommentService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace CommentService.Controllers
{
    [ApiController]
    [Route("/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private IRepository _repository;

        public CommentController(ILogger<CommentController> logger, IRepository repository)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CommentQueryModel[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult GetComments([FromQuery] int pageSize, [FromQuery] int sectionNumber)
        {
            throw new NotImplementedException();
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
