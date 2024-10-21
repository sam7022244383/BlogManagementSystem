using Application.Features.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Pattern.AzureMessageQueue.Command.CreateMessageQueueWithJsonBody
{
    public class SendMessageQueueWithJsonBodyCommand :  IRequest<SendMessageQueueWithJsonBodyRespoance>
    {
        public EmployeRequest employeRequest { get; set; }
    }
}
