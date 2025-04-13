using Library.BusinessLayer.Interfaces;
using Library.DTOs.Author;
using Library.Mappers;
using Library.Models;
using Library.UnitOfWorks;

namespace Library.BusinessLayer.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Author>> GetAll()
        {

            var authors = await _unitOfWork.AuthorRepository.GetAllAsync();
            var authorsDto = authors.Select(x => x.ToAuthorDto());
            return authors;

        }

        public async Task<Author> Get(int id)
        {
            var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);

            if (author == null)
            {
                return null;
            }

            return author;
        }

        public async Task<Author> Create( CreatAuthorRequestDto authorDto)
        {
            var author = authorDto.ToAuthorFromCreatDto();
            await _unitOfWork.AuthorRepository.CreatAsync(author);
            await _unitOfWork.SaveAsync();
            return author;
        }

        public async Task<Author> Update(int id, UpdateAuthorRequestDto updateAuthorRequestDto)
        {
            var authorModel = await _unitOfWork.AuthorRepository.UpdateAsync(id, updateAuthorRequestDto.ToAuthorFromUpdateDto());
            await _unitOfWork.SaveAsync();
            if (authorModel == null)
            {
                return null;
            }
            return authorModel;
        }

        public async Task Delete(int id)
        {
            var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);
            await _unitOfWork.AuthorRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
