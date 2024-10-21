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

namespace Application.Features.Blog.Queries.GetBlogs
{
    public class GetBlogsQueryHandler : IRequestHandler<GetBlogsQuery, GetBlogsResponse>
    {
        private readonly IRepository<de.Blog> repository;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public GetBlogsQueryHandler(IRepository<de.Blog> repository , 
            IMapper mapper , ILogger<GetBlogsQueryHandler> logger) 
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<GetBlogsResponse> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
           var getBlogRespoance = new GetBlogsResponse();
           var Validator = new GetBlogsValidator();
            try
            {
                var validationResult = await Validator.ValidateAsync(request,  cancellationToken);
                if(validationResult.Errors.Any())
                {
                    getBlogRespoance.Success = false;
                    getBlogRespoance.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        getBlogRespoance.ValidationErrors.Add(error);
                        this.logger.LogError($"Validation failed due to error :- {error}");
                    }
                }
                else
                {
                    var result = await this.repository.GetAllAsync();
                    getBlogRespoance.Blogs = this.mapper.Map<List<BlogDto>>(result);

                }
            }
            catch (Exception ex)
            {
                this.logger.LogError (ex.Message);
                getBlogRespoance.Success= false;
                getBlogRespoance.Message = ex.Message;
            }
            return getBlogRespoance;

        }
    }
}
