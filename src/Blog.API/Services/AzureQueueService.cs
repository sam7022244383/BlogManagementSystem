using Application.Interface;
using Azure;
using Azure.Storage.Queues.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AzureQueueService<T> : IAzureQueueService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public AzureQueueService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<SendReceipt>> SendeMessageJsonBody(T message)
        {
           var result = await _unitOfWork.azureMessageQueueRepository
                .SendMessageObjectAsync(message);
            await _unitOfWork.CommitAsync();
            return result;
        }

        public async Task<Response<SendReceipt>> SendSingleMessage(string message)
        {
           var result = await _unitOfWork.azureMessageQueueRepository.SendMessageAsync(message);
            await _unitOfWork.CommitAsync();
            return result;

    
        }
    }
}
