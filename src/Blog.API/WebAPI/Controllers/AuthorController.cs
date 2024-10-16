using Application.Features.Author.Query.GetAuthorByName;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ApiControllerBase
    {
        [HttpGet("{Name}")]
        public async Task<ActionResult<GetAuthorByNameRespoance>> GetAuthorByName(string Name)
        {
            var respoance = await this.Mediator.Send(new GetAuthorByNameQuery { Name = Name });
            return respoance;
        }
    }
}
