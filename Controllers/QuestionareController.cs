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
    public class QuestionareController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public QuestionareController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetQuestions([FromQuery] QuestionareRequest request, CancellationToken cancellationToken)
        {
            DataResponse dataResponse = new DataResponse();

            System.Net.IPAddress remoteIpAddress = this.Request.HttpContext.Connection.RemoteIpAddress;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var header = Request.Headers["Authorization"];

            dataResponse = await _serviceManager.QuestionareService.GetQuestions(request, identity, header, remoteIpAddress, cancellationToken);

            return Ok(dataResponse);

        }

        [HttpGet("interviews")]
        public async Task<IActionResult> GetInterviews([FromQuery] InterviewRequest request, CancellationToken cancellationToken)
        {
            DataResponse dataResponse = new DataResponse();

            System.Net.IPAddress remoteIpAddress = this.Request.HttpContext.Connection.RemoteIpAddress;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var header = Request.Headers["Authorization"];

            dataResponse = await _serviceManager.QuestionareService.GetInterview(request, identity, header, remoteIpAddress, cancellationToken);

            return Ok(dataResponse);

        }

        [HttpPost("answer")]
        public async Task<IActionResult> PostInterview(InterviewDto request, CancellationToken cancellationToken)
        {

            BaseResponse response = new BaseResponse();

            System.Net.IPAddress remoteIpAddress = this.Request.HttpContext.Connection.RemoteIpAddress;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var header = Request.Headers["Authorization"];

            response = await _serviceManager.QuestionareService.AddAnswer(request, identity, header, remoteIpAddress, cancellationToken);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> PutQuestion(QuestionareDto request, CancellationToken cancellationToken)
        {

            BaseResponse response = new BaseResponse();

            System.Net.IPAddress remoteIpAddress = this.Request.HttpContext.Connection.RemoteIpAddress;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var header = Request.Headers["Authorization"];

            response = await _serviceManager.QuestionareService.PutQuestionare(request, identity, header, remoteIpAddress, cancellationToken);

            return Ok(response);
        }
    }
}
