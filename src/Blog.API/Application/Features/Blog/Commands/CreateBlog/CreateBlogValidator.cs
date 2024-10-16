using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blog.Commands.CreateBlog
{
    public class CreateBlogValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogValidator() {
            RuleFor(p => p.Blog.Name).NotEmpty().NotNull().WithMessage("Name should have value");

            RuleFor(p => p.Blog.Description).NotEmpty().NotNull().WithMessage("Description Should have value");
        }
    }
}
