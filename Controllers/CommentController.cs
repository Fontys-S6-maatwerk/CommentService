using CommentService.Mappers;
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
        [Route("/comments/solutions/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CommentQueryModel[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult GetComments([FromRoute] Guid id, [FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            _logger.LogInformation("the retreival of a list of comments is requested");

            try
            {
                return Ok(CommentMapper.MapToQueryPage(
                    _repository.Get(id, pageNumber, pageSize)));
            } catch (Exception e)
            {
                _logger.LogError("error occured when  retrieving a list of comments", e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CommentQueryModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult CreateComment([FromBody] CommentQueryModel comment)
        {
            _logger.LogInformation("the creation of a comment was requested");

            try
            {
                return Ok(
                    CommentMapper.MapToQueryModel(
                        _repository.Create(
                            CommentMapper.MapToDataModel(comment))));
            } catch (Exception e)
            {
                _logger.LogError("error occured when creating a comment", e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
