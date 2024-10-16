using Application.Interface;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppContext appContext) : base(appContext) { }

        public async Task<List<Author>> GetNyNameAsync(string name)
        {
           return await _appcontext.Author.Where(x => x.Name == name).ToListAsync();
        }
    }
}
