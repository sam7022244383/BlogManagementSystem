using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.ViewModel;
using Azure;
using Azure.Storage.Queues.Models;

namespace Services.Interfaces
{
    public interface IAzureQueueService<in T> where T : class
    {
        Task<Response<SendReceipt>> SendSingleMessage(string message);

        Task<Response<SendReceipt>> SendeMessageJsonBody(T message);

        Task<List<AzureQueueMessageRespoance>> GetAllQueueMessages(int count);

        Task<List<AzureQueueMessageRespoance>> DeleteQueueMessages(int count);
    }

}
