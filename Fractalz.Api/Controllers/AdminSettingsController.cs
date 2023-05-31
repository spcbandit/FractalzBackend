 using MediatR;
 using Microsoft.AspNetCore.Http;
 using Microsoft.AspNetCore.Mvc;
 using Swashbuckle.AspNetCore.Annotations;
 using System;
 using System.Threading.Tasks;
 using Microsoft.AspNetCore.Authorization;
 using Fractalz.Application.Domains.Requests.AdminSetting;
 using Fractalz.Application.Domains.Responses.AdminSetting;


 namespace Fractalz.Api.Controllers;

 public class AdminSettingsController : ControllerBase
 {
     private readonly IMediator _mediator;

     /// <summary>
     /// AdminSettingsController
     /// </summary>
     /// <param name="mediator"></param>
     public AdminSettingsController(IMediator mediator)
     {
         _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
     }
     
     /// <summary>
     /// Получить таблицу настроек администратора
     /// </summary>
     /// <param name="request">GetListAdminSettingsRequest</param>
     /// <returns></returns>
     [HttpGet]
     [Authorize]
     [Route("getListAdminSettings")]
     [SwaggerResponse(StatusCodes.Status200OK, "Получение таблицы настроек администратора", typeof(GetAdminSettingResponse))]
     [SwaggerResponse(StatusCodes.Status400BadRequest, "Получение таблицы настроек администратора", typeof(GetAdminSettingResponse))]
     
       public async Task<IActionResult> GetListAdminSettings([FromQuery] GetAdminSettingRequest request)
        {
               var resp = await _mediator.Send(request);

             if (resp.Success)
                return Ok(resp);
             else
                 return BadRequest(resp);
         }

     /// <summary>
     /// Получить активные настройки администратора
     /// </summary>
     /// <param name="request">GetListAdminSettingsRequest</param>
     /// <returns></returns>
     [HttpGet]
     [Authorize]
     [Route("getListActiveAdminSettings")]
     [SwaggerResponse(StatusCodes.Status200OK, "Получение активных настроек администратора", typeof(GetActiveAdminSettingResponse))]
     [SwaggerResponse(StatusCodes.Status400BadRequest, "Получение активных настроек администратора", typeof(GetActiveAdminSettingResponse))]
     
     public async Task<IActionResult> GetListActiveAdminSettings([FromQuery] GetActiveAdminSettingRequest request)
     {
         var resp = await _mediator.Send(request);

         if (resp.Success)
             return Ok(resp);
         else
             return BadRequest(resp);
     }
     
     /// <summary>
     /// Удалить таблицу настроек администратора
     /// </summary>
     /// <param name="request">DeleteListAdminSettingsRequest</param>
     /// <returns></returns>
     [HttpDelete]
     [Authorize]
     [Route("deleteListAdminSettings")]
     [SwaggerResponse(StatusCodes.Status200OK, "Удаление таблицы настроек администратора", typeof(DeleteAdminSettingResponse))]
     [SwaggerResponse(StatusCodes.Status400BadRequest, "Удаление таблицы настроек администратора", typeof(DeleteAdminSettingResponse))]
     [SwaggerResponse(StatusCodes.Status500InternalServerError, "Удаление таблицы настроек администратора", typeof(DeleteAdminSettingResponse))]

     public async Task<IActionResult> DeleteListAdminSettings([FromQuery] DeleteAdminSettingRequest request)
     {
         var resp = await _mediator.Send(request);
         if (resp.Success)
             return Ok(resp);
         else
             return BadRequest(resp);
     }
     
     /// <summary>
     /// Обновить таблицу настроек администратора
     /// </summary>
     /// <param name="request">UpdateListAdminSettingsRequest</param>
     /// <returns></returns>
     [HttpPut]
     [Authorize]
     [Route("updateListAdminSettings")]
     [SwaggerResponse(StatusCodes.Status200OK, "Обновление таблицы настроек администратора", typeof(UpdateAdminSettingResponse))]
     [SwaggerResponse(StatusCodes.Status400BadRequest, "Обновление таблицы настроек администратора", typeof(UpdateAdminSettingResponse))]
     
     public async Task<IActionResult> UpdateListAdminSettings([FromQuery] UpdateAdminSettingRequest request)
     {
         var resp = await _mediator.Send(request);

         if (resp.Success)
             return Ok(resp);
         else
             return BadRequest(resp);
     }
     /// <summary>
     /// Добавить таблицу настроек администратора
     /// </summary>
     /// <param name="request">CreateListAdminSettingsRequest</param>
     /// <returns></returns>
     [HttpPost]
     [Authorize]
     [Route("createListAdminSettings")]
     [SwaggerResponse(StatusCodes.Status200OK, "Добавление таблицы настроек администратора", typeof(CreateAdminSettingResponse))]
     [SwaggerResponse(StatusCodes.Status400BadRequest, "Добавление таблицы настроек администратора", typeof(CreateAdminSettingResponse))]
     
     public async Task<IActionResult> CreateListAdminSettings([FromQuery] CreateAdminSettingRequest request)
     {
         var resp = await _mediator.Send(request);

         if (resp.Success)
             return Ok(resp);
         else
             return BadRequest(resp);
     }
     
 }