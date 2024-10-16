using Application.Interface;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using de = Domain.Entities;
namespace Application.Features.Blog.Commands.DeleteBlog
{
    public class DeleteblogValidator : AbstractValidator<DeleteBlogCOmmand>
    {
        private readonly IRepository<de.Blog> _repository;
        public DeleteblogValidator(IRepository<de.Blog> repository)
        {
            _repository = repository;
            RuleFor(p => p.Id).GreaterThan(0).NotEmpty().WithMessage("Id is Not valid");
            RuleFor(p => p.Id).MustAsync(IsExistBlog).WithMessage("Id Does not exist");
        }

        private async Task<bool> IsExistBlog(int BlogId, CancellationToken cancellationToken)
        {
            var blog = await _repository.GetByIdAsync(BlogId).ConfigureAwait(false);
            return blog == null ? false : (blog.Id > 0 ? true : false);
        }
    }
}
