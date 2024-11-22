using Application.Features.ViewModel;
using CQRS.Pattern.AzureMessageQueue.Command.SendMessageQueue;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Pattern.AzureMessageQueue.Query.GetQueueMessages
{
    public class GetQueueMessageQueryHandler : IRequestHandler<GetQueueMessageQuery , GetQueueMessageRespoance>
    {
        private readonly IAzureQueueService<EmployeRequest> _azureQueueService;
        private readonly ILogger logger;
        public GetQueueMessageQueryHandler(IAzureQueueService<EmployeRequest> azureQueueService)
        {
            _azureQueueService = azureQueueService;
            this.logger = logger;
        }
        public async Task<GetQueueMessageRespoance> Handle(GetQueueMessageQuery request, CancellationToken cancellationToken)
        {
            var createAzureQueueRespoance = new GetQueueMessageRespoance();
            var validator = new GetQueueMessageValidator();
            try
            {
                var validationResult = validator.Validate(request);
                if (validationResult.Errors.Any())
                {
                    createAzureQueueRespoance.Success = false;
                    createAzureQueueRespoance.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        createAzureQueueRespoance.ValidationErrors.Add(error);
                        this.logger.LogError(error);
                    }
                }
                else
                {
                    var result = await _azureQueueService.GetAllQueueMessages(request.Id);
                    if(result.Any())
                    {
                        createAzureQueueRespoance.MessageRespoances = result;
                        createAzureQueueRespoance.StatusCode = 201;
                        createAzureQueueRespoance.Success = true;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                createAzureQueueRespoance.Success = false;
                createAzureQueueRespoance.StatusCode = 500;
            }
            return createAzureQueueRespoance;
        }
    }
}
