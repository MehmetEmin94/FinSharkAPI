using AutoMapper;
using FinSharkAPI.Dtos.Comment;
using FinSharkAPI.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace FinSharkAPI.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController:ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments=await _commentRepository.GetAllAsync();
            var commentDtos=_mapper.Map<List<CommentDto>>(comments);
            return Ok(commentDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment=await _commentRepository.GetByIdAsync(id);
            var commentDto=_mapper.Map<CommentDto>(comment);
            return Ok(commentDto);
        }
    }
}