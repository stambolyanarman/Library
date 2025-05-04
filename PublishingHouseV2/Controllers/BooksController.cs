using Microsoft.AspNetCore.Mvc;
using Library.BusinessLayer.Interfaces;
using Library.BusinessLayer.DTOs.Book;
using Library.BusinessLayer.Mappers;


namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(IBooksService service) : ControllerBase
    {
        private readonly IBooksService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            return Ok(await _service.GetBooks());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _service.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpPost("{authorId}")]
        public async Task<IActionResult> PostBook(int authorId,CreatBookRequestDto creatDto)
        {
            var book = await _service.CreateBook(authorId, creatDto);
            return CreatedAtAction(nameof(GetBook), new{ id = book.Id}, book.ToBookDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutBook([FromRoute] int id, [FromBody] UpdateBookRequestDto updateDto)
        {
            return Ok(await _service.UpdateBook(id, updateDto));
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeteleBook([FromRoute] int id)
        {
            await _service.DeleteBook(id);
            return NoContent();
        }

    }
}
