using Application.Interface;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using de = Domain.Entities;

namespace Application.Features.Blog.Commands.UpdateBlog
{
    public class UpdateBlogValidator : AbstractValidator<UpdateBlogCommand>
    {
        private readonly IRepository<de.Blog> _repository;
        public UpdateBlogValidator(IRepository<de.Blog> repository)
        {
            _repository = repository;

            RuleFor(p => p.Blog.Name).NotEmpty().NotNull().WithMessage("Name Should have a value");

            RuleFor(p => p.Blog.Description).NotNull().NotEmpty()
                .WithMessage("Description should have a value");

            RuleFor(p => p.Id).MustAsync(IsExistBlog).WithMessage("Id Does not exist");
        
        }

        private async Task<bool> IsExistBlog(int BlogId, CancellationToken cancellationToken)
        {
            var blog = await _repository.GetByIdAsync(BlogId).ConfigureAwait(false);
            return blog == null? false : (blog.Id > 0 ? true : false);
        }

    }
}
