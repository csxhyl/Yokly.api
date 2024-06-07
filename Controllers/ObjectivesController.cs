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
    public class ObjectivesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ObjectivesController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetClientTask([FromQuery] ClientTaskRequest request, CancellationToken cancellationToken)
        {
            DataResponse dataResponse = new DataResponse();

            System.Net.IPAddress remoteIpAddress = this.Request.HttpContext.Connection.RemoteIpAddress;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var header = Request.Headers["Authorization"];

            dataResponse = await _serviceManager.ClientTaskService.GetData(request, identity, header, remoteIpAddress, cancellationToken);

            return Ok(dataResponse);

        }

        [HttpPost("addclienttaskobjectives")]
        public async Task<IActionResult> PostClientTaskObjectives(ClientTaskObjectivesDto request, CancellationToken cancellationToken)
        {

            BaseResponse response = new BaseResponse();

            System.Net.IPAddress remoteIpAddress = this.Request.HttpContext.Connection.RemoteIpAddress;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var header = Request.Headers["Authorization"];

            response = await _serviceManager.ClientTaskService.InsertClietTaskObjectives(request, identity, header, remoteIpAddress, cancellationToken);

            return Ok(response);
        }

        [HttpGet("getallclienttaskobjectivesbyid")]
        public async Task<IActionResult> GetAllClientTaskObjectivesById([FromQuery] ClientTaskObjectiveRequest request, CancellationToken cancellationToken)
        {
            DataResponse dataResponse = new DataResponse();

            System.Net.IPAddress remoteIpAddress = this.Request.HttpContext.Connection.RemoteIpAddress;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var header = Request.Headers["Authorization"];

            dataResponse = await _serviceManager.ClientTaskService.GetClientTasksByClient(request, identity, header, remoteIpAddress, cancellationToken);

            return Ok(dataResponse);

        }
    }
}
