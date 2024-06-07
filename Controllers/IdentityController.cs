using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Yokly.Application.Services.Abstraction;
using Yokly.Domain.DTO.Request;
using Yokly.Domain.DTO.Response;
using Yokly.Domain.Entities;

namespace Yokly.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public IdentityController(IServiceManager serviceManager) => _serviceManager = serviceManager;


        [HttpPost("login")]
        public async Task<IActionResult> Login(CancellationToken cancellationToken)
        {

            DataResponse dataResponse = new DataResponse();
            System.Net.IPAddress remoteIpAddress = this.Request.HttpContext.Connection.RemoteIpAddress;
            var header = Request.Headers["Authorization"];

            var accountsDto = await _serviceManager.IdentityService.Login(header, remoteIpAddress, cancellationToken);

            return Ok(accountsDto);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request, CancellationToken cancellationToken)
        {
            System.Net.IPAddress remoteIpAddress = this.Request.HttpContext.Connection.RemoteIpAddress;

            BaseResponse dataResponse = new BaseResponse();


            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var header = Request.Headers["Authorization"];

            dataResponse = await _serviceManager.IdentityService.Register(request, remoteIpAddress, cancellationToken);

            return Ok();
        }
    }
}
