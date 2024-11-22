using Application.Features.ViewModel;
using CQRS.Pattern.AzureMessageQueue.Query.GetQueueMessages;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Pattern.AzureMessageQueue.Command.DeleteMessageQueue
{
    public class DeleteMessageQueueCommandHandler : IRequestHandler<DeleteMessageQueueCommand, DeleteMessageQueueRespoance>
    {
        private readonly IAzureQueueService<EmployeRequest> _azureQueueService;
        private readonly ILogger logger;
        public DeleteMessageQueueCommandHandler(IAzureQueueService<EmployeRequest> azureQueueService)
        {
            _azureQueueService = azureQueueService;
            this.logger = logger;
        }


        public async Task<DeleteMessageQueueRespoance> Handle(DeleteMessageQueueCommand request, CancellationToken cancellationToken)
        {
            var createAzureQueueDeleteRespoance = new DeleteMessageQueueRespoance();
            var validator = new DeleteMessageQueueValidator();
            try
            {
                var validationResult = validator.Validate(request);
                if (validationResult.Errors.Any())
                {
                    createAzureQueueDeleteRespoance.Success = false;
                    createAzureQueueDeleteRespoance.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        createAzureQueueDeleteRespoance.ValidationErrors.Add(error);
                        this.logger.LogError(error.ToString());
                    }
                }
                else
                {
                    var result = await _azureQueueService.DeleteQueueMessages(request.Id);
                    if (result.Any())
                    {
                        createAzureQueueDeleteRespoance.MessageRespoances = result;
                        createAzureQueueDeleteRespoance.StatusCode = 201;
                        createAzureQueueDeleteRespoance.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                createAzureQueueDeleteRespoance.Success = false;
                createAzureQueueDeleteRespoance.StatusCode = 500;
            }
            return createAzureQueueDeleteRespoance;
        }
    }
}