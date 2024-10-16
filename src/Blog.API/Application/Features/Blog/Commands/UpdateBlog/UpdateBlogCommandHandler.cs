using Application.Features.Blog.Commands.CreateBlog;
using Application.Features.Blog.Queries.GetBlogs;
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

namespace Application.Features.Blog.Commands.UpdateBlog
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, UpdateBlogRespoance>
    {
        private readonly IRepository<de.Blog> repository;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public UpdateBlogCommandHandler(IRepository<de.Blog> repository,
            IMapper mapper, ILogger<UpdateBlogCommandHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<UpdateBlogRespoance> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var UpdateBlogRespoance = new UpdateBlogRespoance();
            var validator = new UpdateBlogValidator(this.repository);
            try
            {
                var ValidatorResult = await validator.ValidateAsync(request, cancellationToken);
                if (ValidatorResult.Errors.Any())
                {
                    UpdateBlogRespoance.Success = false;
                    UpdateBlogRespoance.ValidationErrors = new List<string>();
                    foreach (var error in ValidatorResult.Errors.Select(x => x.ErrorMessage))
                    {
                        UpdateBlogRespoance.ValidationErrors.Add(error);
                        this.logger.LogError($"Validation failed due to error :- {error}");
                    }
                }

                var blogEntity = await this.repository.GetByIdAsync(request.Id);
                this.mapper.Map(request.Blog, blogEntity);
                if(blogEntity != null)
                {
                    await this.repository.UpdateAsync(blogEntity);
                    UpdateBlogRespoance.Id = blogEntity.Id;
                }
                 
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                UpdateBlogRespoance.Success = false;
                UpdateBlogRespoance.Message = ex.Message;
            }
            return UpdateBlogRespoance;
        }
    }
}
