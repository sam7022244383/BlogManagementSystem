using Azure.Storage.Queues.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure;

namespace Application.Interface
{
    public interface IAzureMessage
    {
        Task<Response<SendReceipt>> SendMessageAsync(string message);

        Task<Response<SendReceipt>> SendMessageObjectAsync<T>(T mesaage);
    }
}
