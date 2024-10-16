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

namespace Application.Features.Blog.Queries.GetBlogById
{
    public class GetBlogByIdQueryHandler : IRequestHandler<GetBlogByIdQuery, GetBlogByIdRespoance>
    {
        private readonly IRepository<de.Blog> repository;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public GetBlogByIdQueryHandler(IRepository<de.Blog> repository,
            IMapper mapper, ILogger<GetBlogsQueryHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<GetBlogByIdRespoance> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            var blogRespoance = new GetBlogByIdRespoance();
            var Validator = new GetBlogByIdValidator();
            try
            {
                var validationResult = await Validator.ValidateAsync(request, cancellationToken);
                if(validationResult.Errors.Any())
                {
                    blogRespoance.Success = false;
                    blogRespoance.ValidationErrors = new List<string>();
                    foreach(var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        blogRespoance.ValidationErrors.Add(error);
                        this.logger.LogError($"Validation failed due to error :- {error}");
                    }
                }
                else
                {
                    var result = await this.repository.GetByIdAsync(request.Id);
                    if(result != null)
                    {
                        blogRespoance.Blog = this.mapper.Map<BlogDto>(result);
                    }
                }

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                blogRespoance.Success = false;
                blogRespoance.Message = ex.Message;
            }
            return blogRespoance;
        }
    }
}
