using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAuthorByName(string name);

        Task<IReadOnlyList<Author>> GetAllAuthors();

        Task<Author> GetAuthorById(int id);

        Task<Author> AddNewAuthor(Author entity);

        Task UpdateAuthor(Author entity);

        Task DeleteAuthor(Author entity);
    }
}
