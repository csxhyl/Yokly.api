using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Yokly.Application.Services.Abstraction;
using Yokly.Domain.DTO.Request;
using Yokly.Domain.DTO.Response;
using Yokly.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Yokly.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ClientController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpPost]
        public async Task<IActionResult> PostClient(ClientDto request,CancellationToken cancellationToken)
        {

            BaseResponse response = new BaseResponse();

            System.Net.IPAddress remoteIpAddress = this.Request.HttpContext.Connection.RemoteIpAddress;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var header = Request.Headers["Authorization"];

            response = await _serviceManager.ClientService.InsertClient(request, identity, header, remoteIpAddress, cancellationToken);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> PutClient(ClientDto request, CancellationToken cancellationToken)
        {

            BaseResponse response = new BaseResponse();

            System.Net.IPAddress remoteIpAddress = this.Request.HttpContext.Connection.RemoteIpAddress;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var header = Request.Headers["Authorization"];

            response = await _serviceManager.ClientService.UpdateClient(request, identity, header, remoteIpAddress, cancellationToken);

            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetClients([FromQuery] ClientRequest request, CancellationToken cancellationToken)
        {
            DataResponse dataResponse = new DataResponse();

            System.Net.IPAddress remoteIpAddress = this.Request.HttpContext.Connection.RemoteIpAddress;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var header = Request.Headers["Authorization"];

            dataResponse = await _serviceManager.ClientService.GetClients(request, identity, header, remoteIpAddress, cancellationToken);

            return Ok(dataResponse);
    
        }
    }
}
