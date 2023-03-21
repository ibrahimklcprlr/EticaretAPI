using EticaretAPI.Aplication.Features.Commands.User.CreateUser;
using EticaretAPI.Aplication.Features.Commands.User.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
        {
            CreateUserCommandResponse response=await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest request)
        {
            LoginUserCommandResponse response= await _mediator.Send(request);
            return Ok(response);
        }
    }
}
