using Application.Interface;
using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class AzureMessage : IAzureMessage
    {
        private readonly QueueClient _queueClient;
        public AzureMessage(QueueServiceClient queueServiceClient) { 
            _queueClient = queueServiceClient.GetQueueClient("demo");
        }
        public async Task<Response<SendReceipt>> SendMessageAsync(string message)
        {
            Response<SendReceipt>? result = null ;
            try
            {
                await _queueClient.CreateIfNotExistsAsync();
                result =  await _queueClient.SendMessageAsync(message);

            }
            catch (Exception ex)
            {
                
            }
            return result;

        }

        public async Task<Response<SendReceipt>> SendMessageObjectAsync<T>(T mesaage)
        {
            Response<SendReceipt>? result = null;
            try
            {
                var SerializerConfig = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                };
                var msg = JsonSerializer.Serialize(mesaage , SerializerConfig);
                await _queueClient.CreateIfNotExistsAsync();
                result = await _queueClient.SendMessageAsync(msg);

            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
