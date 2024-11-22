using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Pattern.AzureMessageQueue.Command.DeleteMessageQueue
{
    public class DeleteMessageQueueValidator : AbstractValidator<DeleteMessageQueueCommand>
    {
        public DeleteMessageQueueValidator()
        {
            RuleFor(p => p.Id).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Count is not valid");
        }
    }
}
