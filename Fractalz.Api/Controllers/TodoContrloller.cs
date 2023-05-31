using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Fractalz.Application.Domains.Requests.Todo;
using Fractalz.Application.Domains.Responses.Todo;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;

namespace Fractalz.Api.Controllers
{
    [ApiController]
    [Route("/todoList/")]
    [DisplayName("Todo Листы")]
    [Produces("application/json")]
    public class TodoContrloller : ControllerBase
    {
        private readonly IMediator _mediator;
        public TodoContrloller(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Получить TodoList
        /// </summary>
        /// <param name="request">Если время сортировки не указано то возвращается за последние 3 дня</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("getList")]
        [SwaggerResponse(StatusCodes.Status200OK, "Получение списка задач по пользователю", typeof(GetTodoListResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Получение списка задач по пользователю", typeof(GetTodoListResponse))]
        public async Task<IActionResult> GetListByUserId([FromQuery] GetTodoListRequest request)
        {
            var resp = await _mediator.Send(request);

            if(resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }

        /// <summary>
        /// Создать задачу
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("createTask")]
        [SwaggerResponse(StatusCodes.Status200OK, "Создание одной задачи в сервисе Todo", typeof(CreateTaskResponse))]
        public async Task<IActionResult> CreateTaskAsync([FromBody] CreateTaskRequest request)
        {
            var resp = await _mediator.Send(request);

            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }

        /// <summary>
        /// Изменить статус задачи
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("updateStatusTask")]
        [SwaggerResponse(StatusCodes.Status200OK, "Обновление статуса одной задачи", typeof(UpdateStatusTaskResponse))]
        public async Task<IActionResult> UpdateStatusTaskAsync([FromBody] UpdateStatusTaskRequest request)
        {
            var resp = await _mediator.Send(request); 

            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }

        /// <summary>
        /// Изменить статус задачи
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("deleteTask")]
        [SwaggerResponse(StatusCodes.Status200OK, "Удаление задачи", typeof(DeleteTaskResponse))]
        public async Task<IActionResult> DeleteTaskAsync([FromQuery] DeleteTaskRequest request)
        {
            var resp = await _mediator.Send(request); 

            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }

    }
}
