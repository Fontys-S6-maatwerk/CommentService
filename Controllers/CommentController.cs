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
        private DbContextRepository _repository;

        public CommentController(ILogger<CommentController> logger, DbContextRepository repository)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CommentQueryModel[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult GetComments([FromQuery] int pageSize, [FromQuery] int sectionNumber)
        {
            throw new NotImplementedException();
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
                        _repository.CreateComment(
                            CommentMapper.MapToDataModel(comment))));
            } catch (Exception e)
            {
                _logger.LogError("error occured when creating a comment", e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
