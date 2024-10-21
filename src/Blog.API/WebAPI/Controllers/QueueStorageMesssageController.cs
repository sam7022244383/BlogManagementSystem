using Application.Features.ViewModel;
using CQRS.Pattern.AzureMessageQueue.Command.CreateMessageQueueWithJsonBody;
using CQRS.Pattern.AzureMessageQueue.Command.SendMessageQueue;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueStorageMesssageController : ApiControllerBase
    {
        [HttpPost("{Name}")]
        public async Task<ActionResult<SendMessageQueueRespoance>> SendQueueMessage(string Name)
        {
            var respoance = await this.Mediator.Send(new SendMessageQueueCommand { Name = Name });
            return respoance;
        }

        [HttpPost("WithJsonBody")]
        public async Task<ActionResult<SendMessageQueueWithJsonBodyRespoance>> SendQueueMessagewithJsonBody(EmployeRequest employeRequest)
        {
            var respoance = await this.Mediator.Send(new SendMessageQueueWithJsonBodyCommand() { employeRequest = employeRequest });
            return respoance;
        }
    }
}
