//using Application.Interface;
//using Domain.Entities;
using Application.Interface;
using Domain.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthorService : IAuthorService
    {
        internal IUnitOfWork _unitofwork;
        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }
        public async Task<Author> AddNewAuthor(Author entity)
        {
            var result = new Author();
            if (entity != null)
            {
                result = await _unitofwork.AuthorRepository.AddAsync(entity);

                _unitofwork.Save();
            }
            return result;
        }

        public Task DeleteAuthor(Author entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Author>> GetAllAuthors()
        {
            throw new NotImplementedException();
        }

        public Task<Author> GetAuthorById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Author>> GetAuthorByName(string name)
        {
            return await _unitofwork.AuthorRepository.GetNyNameAsync(name);
        }

        public Task UpdateAuthor(Author entity)
        {
            throw new NotImplementedException();
        }
    }
}
