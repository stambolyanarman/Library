using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Library.DTOs.Book;
using Library.Mappers;
using Library.UnitOfWorks;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache _cache;
        public BooksController(IUnitOfWork unitOfWork, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _unitOfWork.BookRepository.GetAllAsync();
            var bookDto = books.Select(x => x.ToBookDto());
            return Ok(bookDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
            
            await _cache.SetStringAsync(book.Id.ToString(), JsonSerializer.Serialize(book), new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(10)
            });
            var value = await _cache.GetStringAsync(book.Id.ToString());
            if (book == null)
            {
                return NotFound();
            }
            return Ok(value);

        }
        [HttpPost("{authorId}")]
        public async Task<IActionResult> PostBook(int authorId,CreatBookRequestDto creatDto)
        {
            if (!await _unitOfWork.AuthorRepository.AuthorExistsAsync(authorId))
            {
                return BadRequest();
            }
            var book = creatDto.ToBookFromCreatDto(authorId);
            await _unitOfWork.BookRepository.CreatAsync(book);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetBook), new{ id = book.Id}, book.ToBookDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutBook([FromRoute] int id, [FromBody] UpdateBookRequestDto updateDto)
        {
            var book = await _unitOfWork.BookRepository.UpdateAsync(id, updateDto.ToBookFromUpdateDto());
            await _unitOfWork.SaveAsync();
            if(book == null)
            {
                return NotFound();
            }
            return Ok(book.ToBookDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeteleBook([FromRoute] int id)
        {
            await _unitOfWork.BookRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

    }
}
