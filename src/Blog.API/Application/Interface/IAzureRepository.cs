using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IAzureRepository<T> where T : class
    {
        Task<List<T>> GetAll();

        Task<T> GetById(string id);

        Task<bool> Add(T emp);

        Task<bool> Update(T emp, string id);

        Task<bool> DeleteById(string id);
    }
}
