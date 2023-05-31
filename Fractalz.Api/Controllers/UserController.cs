using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;


using Fractalz.Application.Domains.Requests.User;
using Fractalz.Application.Domains.Responses.User;

using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace Fractalz.Api.Controllers
{
    [ApiController]
    [Route("/user/")]
    [DisplayName("Работа с пользователем")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public UserController(IMediator mediator, IConfiguration configuration)
        {
            _configuration = configuration;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Получить TodoList
        /// </summary>
        /// <param name="request">Если время сортировки не указано то возвращается за последние 3 дня</param>
        /// <returns></returns>
        [HttpGet]
        [Route("login")]
        [SwaggerResponse(StatusCodes.Status200OK, "Войти в систему", typeof(LoginResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Вход в систему Success = false", typeof(LoginResponse))]
        public async Task<IActionResult> Login([FromQuery] LoginRequest request)
        {
            var resp = await _mediator.Send(request);

            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }

        /// <summary>
        /// Получить TodoList
        /// </summary>
        /// <param name="request">Если время сортировки не указано то возвращается за последние 3 дня</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("registration")]
        [SwaggerResponse(StatusCodes.Status200OK, "Регистрация в системе", typeof(RegistrationResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Регистрация в системе Success = false", typeof(RegistrationResponse))]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequest request)
        {
            var resp = await _mediator.Send(request);

            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }
        /// <summary>
        /// Создать пользователя и получить ЭЦП
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("digSignUserCreate")]
        [SwaggerResponse(StatusCodes.Status200OK, "Регистрация в системе с помощью ЭЦП", typeof(DigSignUserCreateResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Регистрация в системе Success = false", typeof(DigSignUserCreateResponse))]
        public async Task<IActionResult> DigSignUserCreate([FromBody] DigSignUserCreateRequest request)
        {
            var resp = await _mediator.Send(request);

            if (resp.Success)
                return resp.FileStream;
            else
                return BadRequest(resp);
        }
        /// <summary>
        /// Получить ЭЦП существующего пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("digSignGet")]
        [SwaggerResponse(StatusCodes.Status200OK, "Получение ЭЦП", typeof(DigSignUserCreateResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Получение ЭЦП Success = false", typeof(DigSignUserCreateResponse))]
        public async Task<IActionResult> DigSignGet([FromQuery] DigSignGetRequest request)
        {
            var resp = await _mediator.Send(request);

            if (resp.Success)
                return resp.FileStreamResult;
            else
                return BadRequest(resp);
        }

        /// <summary>
        /// Обновить профиль
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("updateProfile")]
        [SwaggerResponse(StatusCodes.Status200OK, "Обновить профиль", typeof(UpdateProfileResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Обновить профиль", typeof(UpdateProfileResponse))]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            var resp = await _mediator.Send(request);

            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }

        /// <summary>
        /// Обновить статус
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("updateStatus")]
        [SwaggerResponse(StatusCodes.Status200OK, "Обновить профиль", typeof(UpdateStatusResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Обновить профиль", typeof(UpdateStatusResponse))]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusRequest request)
        {
            var resp = await _mediator.Send(request);

            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }
        [HttpPut]
        [AllowAnonymous]
        [Route("passwordReset")]
        [SwaggerResponse(StatusCodes.Status200OK, "Сменить пароль", typeof(UpdateStatusResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Сменить пароль = false", typeof(UpdateStatusResponse))]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetRequest request)
        {
            var resp = await _mediator.Send(request);

            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }
        [HttpPut]
        [AllowAnonymous]
        [Route("sendCode")]
        [SwaggerResponse(StatusCodes.Status200OK, "Отправить код", typeof(UpdateStatusResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Отправить код = false", typeof(UpdateStatusResponse))]
        public async Task<IActionResult> SendCode([FromBody] CodeGenerateRequest request)
        {
            var resp = await _mediator.Send(request);

            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }
        [HttpPut]
        [AllowAnonymous]
        [Route("validCode")]
        [SwaggerResponse(StatusCodes.Status200OK, "Сверить код", typeof(UpdateStatusResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Сверить код = false", typeof(UpdateStatusResponse))]
        public async Task<IActionResult> ValidateCode([FromBody] CodeValidRequest request)
        {
            var resp = await _mediator.Send(request);

            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }
    }
}
