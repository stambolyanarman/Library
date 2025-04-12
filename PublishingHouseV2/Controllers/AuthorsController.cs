using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Library.DTOs.Author;
using Library.Mappers;
using Library.Models;
using Library.UnitOfWorks;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memoryCache;

        public AuthorsController( IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var ifExists = _memoryCache.TryGetValue("Authors", out IEnumerable<Author> cachedAuthors);
            if (!ifExists)
            {
                var authors = await _unitOfWork.AuthorRepository.GetAllAsync();
                _memoryCache.Set("Authors",authors);
                return Ok(authors);
            }
            else
            {
                return Ok(cachedAuthors);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }
        [HttpPost]
        public async Task<IActionResult> PostAuthor([FromBody] CreatAuthorRequestDto authorDto)
        {
            var authorModel =  authorDto.ToAuthorFromCreatDto();
            await _unitOfWork.AuthorRepository.CreatAsync(authorModel);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetAuthor), new {id = authorModel.Id }, authorModel.ToAuthorDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutAuthor([FromRoute] int id, [FromBody] UpdateAuthorRequestDto updateAuthorRequestDto)
        {
            var authorModel = await _unitOfWork.AuthorRepository.UpdateAsync(id, updateAuthorRequestDto.ToAuthorFromUpdateDto());
            await _unitOfWork.SaveAsync();

            if (authorModel == null)
            {
                return NotFound();
            }
            return Ok(authorModel.ToAuthorDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
        {
            await _unitOfWork.AuthorRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }


    }
}
