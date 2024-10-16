using Application.Features.Blog.Queries.GetBlogById;
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
namespace Application.Features.Blog.Commands.CreateBlog
{
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, CreateBlogRespoance>
    {
        private readonly IRepository<de.Blog> repository;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public CreateBlogCommandHandler(IRepository<de.Blog> repository,
            IMapper mapper, ILogger<CreateBlogCommandHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<CreateBlogRespoance> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var createBlogRespoance = new CreateBlogRespoance();
            var Validator = new CreateBlogValidator();
            try
            {
                var validationResult = await Validator.ValidateAsync(request, cancellationToken);
                if (validationResult.Errors.Any())
                {
                    createBlogRespoance.Success = false;
                    createBlogRespoance.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        createBlogRespoance.ValidationErrors.Add(error);
                        this.logger.LogError($"Validation failed due to error :- {error}");
                    }
                }
                else
                {
                    var blogEntity = this.mapper.Map<de.Blog>(request.Blog);
                    var result = await this.repository.AddAsync(blogEntity);
                    if (result != null)
                    {
                        createBlogRespoance.Id = result.Id;
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                createBlogRespoance.Success = false;
                createBlogRespoance.Message = ex.Message;
            }
            return createBlogRespoance;
        }
    }
}
