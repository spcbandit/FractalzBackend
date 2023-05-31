using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Application.Domains.Entities.Profile;
using Fractalz.Application.Domains.Entities.Websocket;
using Fractalz.Application.Domains.MappingEntities.Chat;
using Fractalz.Application.Domains.Requests.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Fractalz.Api.Controllers
{
    [ApiController]
    [DisplayName("Контроллер вебсокета")]
    [Produces("application/json")]
    [Route("/ws/")]
    public class WebSocketController : ControllerBase
    {
        private readonly ILinkedEventService _linkedEventService;
        private readonly IRepository<User> _repositoryUser; 
        private readonly IRepository<Dialog> _repositoryDialog; 
        private Guid idUser;
        private WebSocket webSocket;
        private int _conferenceId;

        public WebSocketController(ILinkedEventService linkedEventService, IRepository<User> repositoryUser, IRepository<Dialog> repositoryDialog)
        {
            _linkedEventService = linkedEventService ?? throw new ArgumentException(nameof(linkedEventService));
            _repositoryUser = repositoryUser ?? throw new ArgumentException(nameof(repositoryUser));
            _repositoryDialog = repositoryDialog ?? throw new ArgumentException(nameof(repositoryDialog));
        }

        [HttpGet("subscribe")]
        public async Task Subscribe([FromQuery] Guid idUser)
        {
            try
            {
                if (HttpContext.WebSockets.IsWebSocketRequest)
                {
                    this.idUser = idUser;
                    webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                    await Echo(HttpContext, webSocket);
                }
                else
                {
                    HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                }
            }
            catch (Exception e)
            {
                
            }
        }
        
        [HttpGet("voiceChatTest")]
        [AllowAnonymous]
        public async Task SubscribeVoice([FromQuery] Guid idUser)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                this.idUser = idUser ;
                webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await EchoAudio(HttpContext, webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }    
        }

        private async Task EchoAudio(HttpContext httpContext, WebSocket webSocket1)
        {   
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
         
            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
              
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
        }

        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            try
            {
                var buffer = new byte[1024];
                WebSocketReceiveResult result =
                    await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                var stringRecieve = GetStringFromByte(buffer);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    if (stringRecieve.Contains("message"))
                    {
                        _linkedEventService.GetMessageEvent += LinkedEventServiceOnGetMessageEvent;
                        if(webSocket.State == WebSocketState.Open)
                            await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType,
                            result.EndOfMessage, CancellationToken.None);
                    }

                    if (stringRecieve.Contains("dialog"))
                    {
                        _linkedEventService.DialogUpdateEvent += LinkedEventServiceOnDialogUpdateEvent;
                        if(webSocket.State == WebSocketState.Open)
                            await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType,
                            result.EndOfMessage, CancellationToken.None);
                    }

                    if (stringRecieve.Contains("users"))
                    {
                        _linkedEventService.UserUpdateStatusEvent += LinkedEventServiceOnUserUpdateStatusEvent;
                        if(webSocket.State == WebSocketState.Open)
                            await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType,
                            result.EndOfMessage, CancellationToken.None);
                    }
                    
                    if (stringRecieve.Contains("noty"))
                    {
                        _linkedEventService.SendNotificationEvent += LinkedEventServiceOnSendNotyEvent;
                        if(webSocket.State == WebSocketState.Open)
                            await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType,
                            result.EndOfMessage, CancellationToken.None);
                    }
                }

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                while (!result.CloseStatus.HasValue)
                {
                    if(webSocket.State == WebSocketState.Open)
                        await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType,
                        result.EndOfMessage, CancellationToken.None);

                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                }
            }
            catch (Exception e)
            {

            }
        }

        private async void LinkedEventServiceOnUserUpdateStatusEvent(User user)
        {
            try
            {
                var userResult = _repositoryUser
                .GetWithInclude(x => x.Id == user.Id, x => x.Dialogs)
                .FirstOrDefault();
            if (userResult is null)
            { return; }

            foreach (var dialog in userResult.Dialogs)
            {
                var dialogResult = _repositoryDialog
                    .GetWithInclude(x => x.Id == dialog.Id, x => x.Users)
                    .FirstOrDefault();
                if (dialogResult?.Users.FirstOrDefault(x => x.Id == idUser) != null)
                {
                    var message = new BasicWsEntities()
                    {
                        Type = WsMessageType.User,
                        Data = user
                    };
                    var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message, Formatting.Indented));
                    if(webSocket.State == WebSocketState.Open)
                        await webSocket.SendAsync(new ArraySegment<byte>(bytes,0, bytes.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
            }
            catch (Exception e)
            {
            }
        }
 
        private async void LinkedEventServiceOnGetMessageEvent(MessageMappedDto messageData)
        {
            try
            {
                var dialog = _repositoryDialog.GetWithInclude(
                    x => x.Id == messageData.DialogId,
                    x => x.Users).FirstOrDefault();

                if (dialog?.Users.FirstOrDefault(x => x.Id == idUser) != null)
                {
                    var message = new BasicWsEntities()
                    {
                        Type = WsMessageType.Message,
                        Data = messageData
                    };
                    var str = JsonConvert.SerializeObject(message);
                    var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                    if (webSocket.State == WebSocketState.Open)
                        await webSocket.SendAsync(new ArraySegment<byte>(bytes, 0, bytes.Length),
                            WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
            catch (Exception e)
            {
            }
        }
        private async void LinkedEventServiceOnSendNotyEvent(SendNotificationRequest messageData)
        {
            try
            {
                if (messageData.UsersId.Any(x=> x == idUser))
                {
                    var message = new BasicWsEntities()
                    {
                        Type = WsMessageType.Noty,
                        Data = new { Title = messageData.Title, Message = messageData.Message, From = messageData.FromUser }
                    };
                    var str = JsonConvert.SerializeObject(message);
                    var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                    if (webSocket.State == WebSocketState.Open)
                        await webSocket.SendAsync(new ArraySegment<byte>(bytes, 0, bytes.Length),
                            WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
            catch (Exception e)
            {
            }
        }

        private async void LinkedEventServiceOnDialogUpdateEvent(DialogsMappedDto dialog)
        {
            try
            {
                if (dialog.Users.FirstOrDefault(x => x.Id == idUser) != null)
                {
                    var message = new BasicWsEntities()
                    {
                        Type = WsMessageType.Dialog,
                        Data = dialog
                    };
                    var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                    if (webSocket.State == WebSocketState.Open)
                        await webSocket.SendAsync(new ArraySegment<byte>(bytes, 0, bytes.Length),
                            WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
            catch (Exception e)
            {
            }
        }

        private string GetStringFromByte(byte[] buffer)
            => System.Text.Encoding.UTF8.GetString(buffer);
    }
}
