using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Pattern.AzureMessageQueue.Command.SendMessageQueue
{
    public class SendMessageQueueValidator : AbstractValidator<SendMessageQueueCommand>
    {
        public SendMessageQueueValidator()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("Name Must be not empty and lenght should be greater than 1");

        }

    }
}
