using Application.Features.Blog.Commands.CreateBlog;
using Application.Features.Blog.Commands.UpdateBlog;
using Application.Interface;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using de = Domain.Entities;
namespace Application.Features.Blog.Commands.DeleteBlog
{
    public class DeleteBlogHandler : IRequestHandler<DeleteBlogCOmmand , DeleteBlogRespoance>
    {
        private readonly IRepository<de.Blog> repository;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public DeleteBlogHandler(IRepository<de.Blog> repository,
            IMapper mapper, ILogger<CreateBlogCommandHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<DeleteBlogRespoance> Handle(DeleteBlogCOmmand request, CancellationToken cancellationToken)
        {
            var deleblofrespoance = new DeleteBlogRespoance();
            var validator = new DeleteblogValidator(this.repository);
            try
            {
                var ValidatorResult = await validator.ValidateAsync(request, cancellationToken);
                if (ValidatorResult.Errors.Any())
                {
                    deleblofrespoance.Success = false;
                    deleblofrespoance.ValidationErrors = new List<string>();
                    foreach (var error in ValidatorResult.Errors.Select(x => x.ErrorMessage))
                    {
                        deleblofrespoance.ValidationErrors.Add(error);
                        this.logger.LogError($"Validation failed due to error :- {error}");
                    }
                }
                else if(deleblofrespoance.Success == true)
                {
                    var blogEntity = await this.repository.GetByIdAsync(request.Id);
                    if (blogEntity != null)
                    {
                        await this.repository.DeleteAsync(blogEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                deleblofrespoance.Success = false;
                deleblofrespoance.Message = ex.Message;
            }
            return deleblofrespoance;

        }
    }
}
