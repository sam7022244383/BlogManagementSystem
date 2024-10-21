using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blog.Queries.GetBlogById
{
    public class GetBlogByIdRespoance : BaseResponse
    {
        public BlogDto Blog { get; set; }
    }
}
