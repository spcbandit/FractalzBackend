using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Fractalz.Application.Domains.Requests.Books;
using Fractalz.Application.Domains.Requests.Books.Get;
using Fractalz.Application.Domains.Requests.Books.Update;
using Fractalz.Application.Domains.Requests.User;
using Fractalz.Application.Domains.Responses.Books;
using Fractalz.Application.Domains.Responses.Books.Get;
using Fractalz.Application.Domains.Responses.Books.Update;
using Fractalz.Application.Domains.Responses.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace Fractalz.Api.Controllers;

[ApiController]
[Route("/books/")]
[DisplayName("Работа с документами")]
[Produces("application/json")]
public class BooksController:ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    public BooksController(IMediator mediator, IConfiguration configuration)
    {
        _configuration = configuration;
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpPost]
    [Route("CreateBook")]
    [SwaggerResponse(StatusCodes.Status200OK, "Создать книгу", typeof(CreateBooksResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Создать книгу Success = false", typeof(CreateBooksResponse))]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
            return Ok(resp);
        else
            return BadRequest(resp);
    }
    [HttpGet]
    [Route("GetBook")]
    [SwaggerResponse(StatusCodes.Status200OK, "Создать книгу", typeof(GetBookResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Создать книгу Success = false", typeof(GetBookResponse))]
    public async Task<IActionResult> GetBook([FromQuery] GetBookRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
            return Ok(resp);
        else
            return BadRequest(resp);
    }
    
    [HttpPut]
    [Route("UpdateBook")]
    [SwaggerResponse(StatusCodes.Status200OK, "Обновить книгу", typeof(UpdateBookResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Обновить книгу Success = false", typeof(UpdateBookResponse))]
    public async Task<IActionResult> UpadateBook([FromQuery] UpdateBookRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
            return Ok(resp);
        else
            return BadRequest(resp);
    }
    [HttpDelete]
    [Route("DeleteBook")]
    [SwaggerResponse(StatusCodes.Status200OK, "Создать книгу", typeof(DeleteBookResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Создать книгу Success = false", typeof(DeleteBookResponse))]
    public async Task<IActionResult> DeleteBook([FromQuery] DeleteBookRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
            return Ok(resp);
        else
            return BadRequest(resp);
    }
    
    [HttpPost]
    [Route("CreateBookSection")]
    [SwaggerResponse(StatusCodes.Status200OK, "Создать раздел книги", typeof(CreateBookSectionResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Создать раздел книги Success = false", typeof(CreateBookSectionResponse))]
    public async Task<IActionResult> CreateBookSection([FromBody] CreateBookSectionRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
            return Ok(resp);
        else
            return BadRequest(resp);
    }
    [HttpGet]
    [Route("GetBookSection")]
    [SwaggerResponse(StatusCodes.Status200OK, "Создать раздел книги", typeof(GetBookSectionResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Создать раздел книги Success = false", typeof(GetBookSectionResponse))]
    public async Task<IActionResult> CreateBookSection([FromQuery] GetBookSectionRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
            return Ok(resp);
        else
            return BadRequest(resp);
    }
    [HttpPut]
    [Route("UpdateBookSection")]
    [SwaggerResponse(StatusCodes.Status200OK, "Обновить раздел книги", typeof(UpdateBookSectionResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Обновить раздел книги Success = false", typeof(UpdateBookSectionResponse))]
    public async Task<IActionResult> UpdateBookSection([FromBody] UpdateBookSectionRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
            return Ok(resp);
        else
            return BadRequest(resp);
    }
    
    [HttpDelete]
    [Route("DeleteBookSection")]
    [SwaggerResponse(StatusCodes.Status200OK, "Создать раздел книги", typeof(DeleteSectionsResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Создать раздел книги Success = false", typeof(DeleteSectionsResponse))]
    public async Task<IActionResult> DeleteBookSection([FromQuery] DeleteSectionRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
            return Ok(resp);
        else
            return BadRequest(resp);
    }
    [HttpPost]
    [Route("CreateBookSheets")]
    [SwaggerResponse(StatusCodes.Status200OK, "Создать страницу раздела", typeof(CreateBookSheetsResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Создать страницу раздела Success = false", typeof(CreateBookSheetsResponse))]
    public async Task<IActionResult> CreateBookSheets([FromBody] CreateBookSheetsRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
            return Ok(resp);
        else
            return BadRequest(resp);
    }
    [HttpPut]
    [Route("GetBookSheets")]
    [SwaggerResponse(StatusCodes.Status200OK, "Создать страницу раздела", typeof(GetBookSheetsResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Создать страницу раздела Success = false", typeof(GetBookSheetsResponse))]
    public async Task<IActionResult> CreateBookSheets([FromQuery] GetBookSheetsRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
            return Ok(resp);
        else
            return BadRequest(resp);
    }
    
    [HttpPut]
    [Route("UpdateBookSheets")]
    [SwaggerResponse(StatusCodes.Status200OK, "Создать страницу раздела", typeof(UpdateBookSheetsResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Создать страницу раздела Success = false", typeof(UpdateBookSheetsResponse))]
    public async Task<IActionResult> UpdateBookSheets([FromBody] UpdateBookSheetsRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
            return Ok(resp);
        else
            return BadRequest(resp);
    }
    [HttpPost]
    [Route("CreateWorkSpace")]
    [SwaggerResponse(StatusCodes.Status200OK, "Создать рабочую область", typeof(CreateWorkSpaceResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Создать рабочую область Success = false", typeof(CreateWorkSpaceResponse))]
    public async Task<IActionResult> CreateWorkSpace([FromBody] CreateWorkSpaceRequest request)
    {
        var resp = await _mediator.Send(request);

        if (resp.Success)
            return Ok(resp);
        else
            return BadRequest(resp);
    }
}