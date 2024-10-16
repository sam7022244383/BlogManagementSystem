using Application.Features;
using Application.Features.Blog.Commands.CreateBlog;
using Application.Features.Blog.Commands.DeleteBlog;
using Application.Features.Blog.Commands.UpdateBlog;
using Application.Features.Blog.Queries.GetBlogById;
using Application.Features.Blog.Queries.GetBlogs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetBlogsResponse>> GetBlogs()
        {
            var respoance = await Mediator.Send(new GetBlogsQuery());
            return respoance;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetBlogByIdRespoance>> GetById(int id)
        {
            var respoance = await Mediator.Send(new GetBlogByIdQuery { Id = id });
            return respoance;
        }

        [HttpPost]
        public async Task<ActionResult<CreateBlogRespoance>> CreateBlog(BlogDto blogDto)
        {
            var respoance = await Mediator.Send(new CreateBlogCommand { Blog = blogDto });
            return respoance;
        }

        [HttpPut("UpdateBlog/{id}")]
        public async Task<ActionResult<UpdateBlogRespoance>> UpdateBlog(int id , BlogDto blogDto)
        {
            var respoance = await Mediator.Send(new UpdateBlogCommand { Id = id, Blog = blogDto });
            return respoance;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<DeleteBlogRespoance>> DeleteBlog(int id)
        {
            var respoance = await Mediator.Send(new DeleteBlogCOmmand { Id = id });
            return respoance;
        }
    }
}
