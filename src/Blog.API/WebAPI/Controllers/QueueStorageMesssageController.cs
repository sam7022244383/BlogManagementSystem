using Application.Features.ViewModel;
using CQRS.Pattern.AzureMessageQueue.Command.CreateMessageQueueWithJsonBody;
using CQRS.Pattern.AzureMessageQueue.Command.DeleteMessageQueue;
using CQRS.Pattern.AzureMessageQueue.Command.SendMessageQueue;
using CQRS.Pattern.AzureMessageQueue.Query.GetQueueMessages;
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

        [HttpGet]
        public async Task<ActionResult<GetQueueMessageRespoance>>
            GetQueueMessages(int count)
        {
            var respoance = await this.Mediator.Send(new GetQueueMessageQuery()
            {
                Id = count
            });
            return respoance;
        }

        [HttpDelete("DeleteQueue/{count}")]
        public async Task<ActionResult<DeleteMessageQueueRespoance>>
            DeleteQueueMessage(int count)
        {
            var respoance = await this.Mediator.Send(new
                DeleteMessageQueueCommand()
            {
                Id = count
            });

            return respoance;
        }


    }
}
