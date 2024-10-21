using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Pattern.AzureMessageQueue.Command.CreateMessageQueueWithJsonBody
{
    public class SendMessageQueueWithJsonBodyValidator : AbstractValidator<SendMessageQueueWithJsonBodyCommand>
    {
        public SendMessageQueueWithJsonBodyValidator()
        {
            RuleFor(p => p.employeRequest.Address).NotEmpty().WithMessage(
                "Address is required");
            RuleFor(p => p.employeRequest.Id).NotEmpty().WithMessage(
                "Id is required");
            RuleFor(p => p.employeRequest.Name).NotEmpty().NotNull()
                .WithMessage("Name is reuired");

            RuleFor(p => p.employeRequest.LastName).NotNull().NotEmpty()
                .WithMessage("Last name is required");
        }

    }
}
