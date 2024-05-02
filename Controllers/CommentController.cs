using AutoMapper;
using FinSharkAPI.Dtos.Comment;
using FinSharkAPI.Extensions;
using FinSharkAPI.IRepositories;
using FinSharkAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinSharkAPI.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController:ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository commentRepository, IMapper mapper, 
                                 IStockRepository stockRepository, UserManager<AppUser> userManager)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _stockRepository = stockRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments=await _commentRepository.GetAllAsync();
            var commentDtos=_mapper.Map<List<CommentDto>>(comments);
            return Ok(commentDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment=await _commentRepository.GetByIdAsync(id);
            if(comment is null)
            {
                return NotFound();
            }
            var commentDto=_mapper.Map<CommentDto>(comment);
            return Ok(commentDto);
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> CreateAsync([FromRoute]int stockId,CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isStockExist=await _stockRepository.StockExist(stockId);
            
            if (!isStockExist)
            {
                return BadRequest("Stock does not exist!");
            }

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            
            var comment=_mapper.Map<Comment>(commentDto);
            comment.StockId=stockId;
            comment.AppUserId=appUser.Id;
            comment=await _commentRepository.CreateAsync(comment);
            return CreatedAtAction(nameof(GetById),new { id=comment.Id},_mapper.Map<CommentDto>(comment));
        }


         [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = _mapper.Map<Comment>(commentDto);
            comment = await _commentRepository.UpdateAsync(id,comment);
            if(comment is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CommentDto>(comment));
        }

         [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment=await _commentRepository.DeleteAsync(id);
            if(comment is null)
            {
                return NotFound();
            }
            var commentDto=_mapper.Map<CommentDto>(comment);
            return Ok(commentDto);
        }
    }
}