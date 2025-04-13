using Microsoft.AspNetCore.Mvc;
using Library.DTOs.Author;
using Library.Mappers;
using Library.BusinessLayer.Interfaces;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase 
    {
        private readonly IAuthorsService _service;

        public AuthorsController(IAuthorsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _service.Get(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author.ToAuthorDto());
        }
        [HttpPost]
        public async Task<IActionResult> PostAuthor([FromBody] CreatAuthorRequestDto authorDto)
        {
           var author = await _service.Create(authorDto);
            return CreatedAtAction(nameof(GetAuthor), new {id = author.Id }, author.ToAuthorDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutAuthor([FromRoute] int id, [FromBody] UpdateAuthorRequestDto updateAuthorRequestDto)
        {
            return Ok(await _service.Update(id, updateAuthorRequestDto));
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
        {
            await _service.Delete(id);
            return NoContent();
        }


    }
}
