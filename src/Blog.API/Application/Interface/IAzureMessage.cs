using Azure.Storage.Queues.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure;
using Application.Features.ViewModel;

namespace Application.Interface
{
    public interface IAzureMessage
    {
        Task<Response<SendReceipt>> SendMessageAsync(string message);

        Task<Response<SendReceipt>> SendMessageObjectAsync<T>(T mesaage);

        Task<List<AzureQueueMessageRespoance>> GetQueueMessages(int count);

        Task<List<AzureQueueMessageRespoance>> DeleteQueueMessages(int count);
    }
}
