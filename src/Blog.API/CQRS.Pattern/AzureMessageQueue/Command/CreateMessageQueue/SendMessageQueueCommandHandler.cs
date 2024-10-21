using Application.Features.Blog.Commands.CreateBlog;
using Application.Features.ViewModel;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Pattern.AzureMessageQueue.Command.SendMessageQueue
{
    public class SendMessageQueueCommandHandler : IRequestHandler<SendMessageQueueCommand,SendMessageQueueRespoance>
    {
        private readonly IAzureQueueService<EmployeRequest> _azureQueueService;
        private readonly ILogger logger;
        public SendMessageQueueCommandHandler(IAzureQueueService<EmployeRequest> azureQueueService)
        {
            _azureQueueService = azureQueueService;
            this.logger = logger;
        }

        public async Task<SendMessageQueueRespoance> Handle(SendMessageQueueCommand request, CancellationToken cancellationToken)
        {
            var createAzureQueueRespoance = new SendMessageQueueRespoance();
            var validator = new SendMessageQueueValidator();
            try
            {
                var validationResult = validator.Validate(request);
                if(validationResult.Errors.Any())
                {
                    createAzureQueueRespoance.Success = false;
                    createAzureQueueRespoance.ValidationErrors = new List<string>();
                    foreach(var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        createAzureQueueRespoance.ValidationErrors.Add(error);
                        this.logger.LogError(error);
                    }
                }
                else
                {
                    var result = await _azureQueueService.SendSingleMessage(request.Name);
                    if (result.GetRawResponse().Status == 201)
                    {
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
