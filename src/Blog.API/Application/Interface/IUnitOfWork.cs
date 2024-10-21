using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IUnitOfWork : IDisposable
    {
      IAuthorRepository AuthorRepository { get; }
      IAzureMessage azureMessageQueueRepository { get; }

        Task CommitAsync();
        Task<int>  Save();
    }
}
