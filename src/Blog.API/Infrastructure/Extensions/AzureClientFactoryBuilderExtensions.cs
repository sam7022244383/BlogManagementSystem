using Azure.Core.Extensions;
using Azure.Storage.Queues;
using Microsoft.Extensions.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    internal static class AzureClientFactoryBuilderExtensions
    {
       public static IAzureClientBuilder<QueueServiceClient, QueueClientOptions> AddQueueServiceClient(this AzureClientFactoryBuilder builder, string 
           ServiceUriOrConnectionString, bool Prefermsi)
        {
            if(Prefermsi && Uri.TryCreate(ServiceUriOrConnectionString,UriKind.Absolute, out var serviceUri))
            {
                return builder.AddQueueServiceClient(serviceUri);
            }
            else
            {
                return builder.AddQueueServiceClient(ServiceUriOrConnectionString);
            }

        }
    }
}
