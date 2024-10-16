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

namespace Application.Features.Author.Query.GetAuthorByName
{
    public class GetAuthorsByNameQueryHandler : IRequestHandler<GetAuthorByNameQuery, GetAuthorByNameRespoance>
    {
        private readonly IAuthorRepository _AuthorRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        public GetAuthorsByNameQueryHandler(IAuthorRepository authorRepository,
            IMapper mapper, ILogger<GetAuthorsByNameQueryHandler> logger)
        {
            _AuthorRepository = authorRepository;
            this.mapper = mapper;
            this.logger = logger;
        }



        public async Task<GetAuthorByNameRespoance> Handle(GetAuthorByNameQuery request, CancellationToken cancellationToken)
        {
            var getByNameAuthorRespoance = new GetAuthorByNameRespoance();
            var Validator = new GetAuthorByNameValidator();
            try
            {
                var validationResult = await Validator.ValidateAsync(request);
                if (validationResult.Errors.Any())
                {
                    getByNameAuthorRespoance.Success = false;
                    getByNameAuthorRespoance.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        getByNameAuthorRespoance.ValidationErrors.Add(error);
                        this.logger.LogError($"Validation failed due to error :- {error}");
                    }
                }
                else
                {
                    var result = await _AuthorRepository.GetNyNameAsync(request.Name);
                    getByNameAuthorRespoance.author = this.mapper.Map<List<AuthorDto>>(result);

                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                getByNameAuthorRespoance.Success = false;
                getByNameAuthorRespoance.Message = ex.Message;
            }
            return getByNameAuthorRespoance;
        }
    }
}
