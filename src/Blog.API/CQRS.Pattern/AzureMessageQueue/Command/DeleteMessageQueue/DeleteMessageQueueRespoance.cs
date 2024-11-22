using Application.Features.ViewModel;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Pattern.AzureMessageQueue.Command.DeleteMessageQueue
{
    public class DeleteMessageQueueRespoance : BaseResponse
    {
        public List<AzureQueueMessageRespoance> MessageRespoances { get; set; }
    }
}
