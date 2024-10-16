using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Query.GetAuthorByName
{
    public class GetAuthorByNameValidator : AbstractValidator<GetAuthorByNameQuery>
    {
        public GetAuthorByNameValidator() { 
            RuleFor(p => p.Name).NotEmpty().NotNull().WithMessage("Name Is Required");
        }
    }
}
