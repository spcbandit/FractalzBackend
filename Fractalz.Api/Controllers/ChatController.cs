using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Fractalz.Application.Domains.Requests.Chat;
using Fractalz.Application.Domains.Responses.Chat;
using Microsoft.AspNetCore.Authorization;

using System.Net;
using System.Text.Json.Nodes;
using Fractalz.Application.Domains.Responses.Chat;


namespace Fractalz.Api.Controllers
{
    [ApiController]
    [Route("/chat/")]
    [DisplayName("Управление чатом")]
    [Produces("application/json")]
    public class  ChatController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// ChatController
        /// </summary>
        /// <param name="mediator"></param>
        public ChatController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Получить список диалогов
        /// </summary>
        /// <param name="request">GetListDialogsRequest</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("getDialogs")]
        [SwaggerResponse(StatusCodes.Status200OK, "Получение списка диалогов по пользователю", typeof(GetListDialogsResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Получение списка диалогов по пользователю", typeof(GetListDialogsResponse))]
        public async Task<IActionResult> GetListDialogs([FromQuery] GetListDialogsRequest request)
        {
            var resp = await _mediator.Send(request);

            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }
        
        /// <summary>
        /// Поиск пользователя
        /// </summary>
        /// <param name="request">FindUserResponse</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("findUsers")]
        [SwaggerResponse(StatusCodes.Status200OK, "Получение списка диалогов по пользователю", typeof(FindUserResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Получение списка диалогов по пользователю", typeof(FindUserResponse))]
        public async Task<IActionResult> FindUsers([FromQuery] FindUserRequest request)
        {
            var resp = await _mediator.Send(request);

            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }

        /// <summary>
        /// Получить список сообщений
        /// </summary>
        /// <param name="request">GetListDialogsRequest</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("getMessages")]
        [SwaggerResponse(StatusCodes.Status200OK, "Получение списка сообщений по пользователю", typeof(GetMessageHistoryResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Получение списка сообщений по пользователю", typeof(GetMessageHistoryResponse))]
        public async Task<IActionResult> GetListMessages([FromQuery] GetMessageHistoryRequest request)
        {
            var resp = await _mediator.Send(request);

            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }

        /// <summary>
        /// СОздать диалог
        /// </summary>
        /// <param name="request">CreateDialog</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("createDialog")]
        [SwaggerResponse(StatusCodes.Status200OK, "Создание диалога", typeof(CreateDialogResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Создание диалога", typeof(CreateDialogResponse))]
        public async Task<IActionResult> CreateDialog([FromBody] CreateDialogRequest request)
        {
            var resp = await _mediator.Send(request);

            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }

        /// <summary>
        /// Удалить диалог
        /// </summary>
        /// <param name="request">DeleteDialog</param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("deleteDialog")]
        [SwaggerResponse(StatusCodes.Status200OK, "Удалить диалога", typeof(DeleteDialogResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Удалить диалога", typeof(DeleteDialogResponse))]
        public async Task<IActionResult> DeleteDialog([FromBody] DeleteDialogRequest request)
        {
            var resp = await _mediator.Send(request);

            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }

        /// <summary>
        /// Создать сообщение
        /// </summary>
        /// <param name="request">CreateMessage</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("createMessage")]
        [SwaggerResponse(StatusCodes.Status200OK, "Создание сообщения", typeof(CreateMessageResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Создание сообщения", typeof(CreateMessageResponse))]
        public async Task<IActionResult> CreateMessage([FromBody] CreateMessageRequest request)
        {
            var resp = await _mediator.Send(request);

            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }
        

        /// <summary>
        /// Обновить сообщение
        /// </summary>
        /// <param name="request">UpdateMessage</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("updateMessage")]
        [SwaggerResponse(StatusCodes.Status200OK, "Обновить сообщения", typeof(UpdateMessageResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Обновить сообщения", typeof(UpdateMessageResponse))]
        public async Task<IActionResult> UpdateMessage([FromBody] UpdateMessageRequest request)
        {
            var resp = await _mediator.Send(request);

            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }

        /// <summary>
        /// Удалить сообщение
        /// </summary>
        /// <param name="request">DeleteMessage</param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("deleteMessage")]
        [SwaggerResponse(StatusCodes.Status200OK, "Удалить сообщения", typeof(DeleteMessageResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Удалить сообщения", typeof(DeleteMessageResponse))]
        public async Task<IActionResult> DeleteMessage([FromBody] DeleteMessageRequest request)
        {
            var resp = await _mediator.Send(request);

            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }

        /// <summary>
        /// Отправить реакцию на сообщение
        /// </summary>
        /// <param name="request">SendReactionMessage</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("reaction")]
        [SwaggerResponse(StatusCodes.Status200OK, "Создание сообщения", typeof(SendReactionResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Создание сообщения", typeof(SendReactionResponse))]
        public async Task<IActionResult> SendReaction([FromBody] SendReactionRequest request)
        {
            var resp = await _mediator.Send(request);

            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }
        
        
        
        
        /// <summary>
        /// Отправка файла
        /// </summary>
        /// <param name="request">CreateMessage</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("fileTransfer")]
        [SwaggerResponse(StatusCodes.Status200OK, "Отправка файла", typeof(FileTransferResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Отправка файла", typeof(FileTransferResponse))]
        public async Task<JsonResult> FileTransfer([FromForm]FileTransferRequest request)
        {
            Request.ContentType = "multipart/form-data";
            var resp = await _mediator.Send(request);

            if (resp.Success)
                return new JsonResult(Ok(resp));
            else
                return new JsonResult(BadRequest(resp));
        }



        /// <summary>
        /// Скачать файл
        /// </summary>
        /// <param name="request">DownloadFile</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("downloadFile")]
        [SwaggerResponse(StatusCodes.Status200OK, "Скачивание файла", typeof(DownloadFileResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Скачивание файла", typeof(DownloadFileResponse))]
        public async Task<IActionResult> DownloadFile([FromQuery] DownloadFileRequest request)
        {
             var resp = await _mediator.Send(request);

             if (resp.Success)
                 return resp.FileStream;
             else
                 return BadRequest(resp);
        }
    }
}
