using Application.Interface;
using Azure.Storage.Queues;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        internal readonly AppContext _appcontext;
        public IAuthorRepository _authorRepository { get; }

        public IAuthorRepository AuthorRepository => _authorRepository;
        public IAzureMessage _iAzureMessage { get; }
        public IAzureMessage azureMessageQueueRepository => _iAzureMessage;

        public UnitOfWork(AppContext appContext, IAuthorRepository authorRepository
            ,IAzureMessage iAzureMessage)
        {
            _appcontext = appContext;
            _authorRepository = authorRepository;
            _iAzureMessage = iAzureMessage;
        }

        public async Task<int> Save()
        {
            return _appcontext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                _appcontext.Dispose();
            }
        }

        public async Task CommitAsync()
        {
            await Task.CompletedTask;
        }
    }
}
