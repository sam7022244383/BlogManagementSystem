using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blog.Commands.CreateBlog
{
    public class CreateBlogCommand : IRequest<CreateBlogRespoance>
    {
        public BlogDto Blog { get; set; }
    }
}
