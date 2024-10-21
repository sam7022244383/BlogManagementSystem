using Application.Features.ViewModel;
using CQRS.Pattern.AzureMessageQueue.Command.SendMessageQueue;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Pattern.AzureMessageQueue.Command.CreateMessageQueueWithJsonBody
{
    public class SendMessageQueueWithJsonBodyCommandHandler
        : IRequestHandler<SendMessageQueueWithJsonBodyCommand, SendMessageQueueWithJsonBodyRespoance>
    {
        private readonly IAzureQueueService<EmployeRequest> _azureQueueService;
        private readonly ILogger logger;
        public SendMessageQueueWithJsonBodyCommandHandler(IAzureQueueService<EmployeRequest> azureQueueService )
        {
            _azureQueueService = azureQueueService;
            this.logger = logger;
        }



        public async Task<SendMessageQueueWithJsonBodyRespoance> Handle(SendMessageQueueWithJsonBodyCommand request, CancellationToken cancellationToken)
        {
            var createAzureQueuewithJsonBodyRespoance = new SendMessageQueueWithJsonBodyRespoance();
            var validator = new SendMessageQueueWithJsonBodyValidator();
            try
            {
                var validationResult = validator.Validate(request);
                if (validationResult.Errors.Any())
                {
                    createAzureQueuewithJsonBodyRespoance.Success = false;
                    createAzureQueuewithJsonBodyRespoance.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        createAzureQueuewithJsonBodyRespoance.ValidationErrors.Add(error);
                        this.logger.LogError(error);
                    }
                }
                else
                {
                    var result = await _azureQueueService.SendeMessageJsonBody(request.employeRequest);
                    if (result.GetRawResponse().Status == 201)
                    {
                        createAzureQueuewithJsonBodyRespoance.StatusCode = 201;
                        createAzureQueuewithJsonBodyRespoance.Success = true;
                    }
                }
            }
            catch ( Exception ex )
            {
                this.logger.LogError(ex.Message);
                createAzureQueuewithJsonBodyRespoance.Success = false;
                createAzureQueuewithJsonBodyRespoance.StatusCode = 500;
            }
            return createAzureQueuewithJsonBodyRespoance;
        }
    }
}
