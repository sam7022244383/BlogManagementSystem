using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Author.Query.GetAuthorByName
{
    public class GetAuthorByNameRespoance :  BaseResponse
    {
        public List<AuthorDto> author { get; set; }
    }
}
