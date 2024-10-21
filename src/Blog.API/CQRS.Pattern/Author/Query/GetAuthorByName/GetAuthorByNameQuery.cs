using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Query.GetAuthorByName
{
    public class GetAuthorByNameQuery : IRequest<GetAuthorByNameRespoance>
    {
        public string Name { get; set; }
    }
}
