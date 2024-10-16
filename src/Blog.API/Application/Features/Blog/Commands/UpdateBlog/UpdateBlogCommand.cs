using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blog.Commands.UpdateBlog
{
    public class UpdateBlogCommand : BaseCommandQuery , IRequest<UpdateBlogRespoance>
    {
        public BlogDto Blog { get; set; }
    }
}
