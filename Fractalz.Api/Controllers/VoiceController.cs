using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Fractalz.Application.Domains.Requests.Voice;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fractalz.Api.Controllers
{  
    [ApiController]
    [Route("/voice/")]
    [DisplayName("Работа с голосовыми серверами")]
    [Produces("application/json")]
    public class VoiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VoiceController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        [HttpGet]
        [Route("findOtherServer")]
        public async Task<IActionResult> FindOtherServer([FromQuery] FindServerRequest request)
        {
            var resp = await _mediator.Send(request);
            
            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }

        [HttpGet]
        [Route("getOtherServers")]
        public async Task<IActionResult> GetOtherServers([FromQuery] GetOtherServersRequest request)
        {
            var resp = await _mediator.Send(request);
            
            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }
        
        [HttpGet]
        [Route("getMyServers")]
        public async Task<IActionResult> GetMyServers([FromQuery] GetMyServersRequest request)
        {
            var resp = await _mediator.Send(request);
            
            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }

        [HttpPost]
        [Route("createMyServer")]
        public async Task<IActionResult> CreateMyServer([FromBody] CreateMyServerRequest request)
        {
            var resp = await _mediator.Send(request);
            
            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }

        [HttpPut]
        [Route("editMyServer")]
        public async Task<IActionResult> EditMyServer([FromBody] EditMyServerRequest request)
        {
            var resp = await _mediator.Send(request);
            
            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }

        [HttpDelete]
        [Route("deleteMyServer")]
        public async Task<IActionResult> DeleteMyServer([FromBody] DeleteMyServerRequest request)
        {
            var resp = await _mediator.Send(request);
            
            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }
        
        [HttpDelete]
        [Route("deleteRoom")]
        public async Task<IActionResult> DeleteRoom([FromBody] DeleteRoomRequest request)
        {
            var resp = await _mediator.Send(request);
            
            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }
        
        [HttpGet]
        [Route("getRooms")]
        public async Task<IActionResult> GetRooms([FromQuery] GetRoomsRequest request)
        {
            var resp = await _mediator.Send(request);
            
            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }
        
        [HttpGet]
        [Route("getUsersRoom")]
        public async Task<IActionResult> GetUsersRoom([FromQuery] GetUsersRoomRequest request)
        {
            var resp = await _mediator.Send(request);
            
            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }
        
        [HttpPost]
        [Route("createRoom")]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomRequest request)
        {
            var resp = await _mediator.Send(request);
            
            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }
        
        [HttpPut]
        [Route("editRoom")]
        public async Task<IActionResult> EditRoom([FromBody] EditRoomRequest request)
        {
            var resp = await _mediator.Send(request);
            
            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }
        
        [HttpPost]
        [Route("insertUserInRoom")]
        public async Task<IActionResult> InsertUserInRoom([FromBody] InsertUserInRoomRequest request)
        {
            var resp = await _mediator.Send(request);
            
            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }
        
        [HttpDelete]
        [Route("deleteUserFromRoom")]
        public async Task<IActionResult> DeleteUserFromRoom([FromBody] DeleteUserFromRoomRequest request)
        {
            var resp = await _mediator.Send(request);
            
            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }
        
        [HttpPost]
        [Route("addOtherServer")]
        public async Task<IActionResult> AddOtherServer([FromBody] AddOtherServerRequest request)
        {
            var resp = await _mediator.Send(request);
            
            if (resp.Success)
                return Ok(resp);
            else
                return BadRequest(resp);
        }
    }
}