using Microsoft.AspNetCore.Mvc;
using Stio.Sample.Models;

namespace Stio.Sample.Controllers;

[ApiController]
[Route("api/books")]
public class BookController : Controller
{
    [HttpGet]
    public BookDto[] ListBook()
    {
        return new BookDto[]
        {
            new BookDto()
            {
                Name = "Book 1",
            },
            new BookDto()
            {
                Name = "Book 2",
            },
        };
    }

    [HttpPost]
    public BookDto JsonBody(BookDto book)
    {
        return book;
    }

    [HttpGet("{id}")]
    public BookDto RouteParameter(BookId id)
    {
        return new BookDto()
        {
            Id = id,
            Name = "Book 1",
        };
    }

    [HttpGet("query")]
    public BookDto QueryParameter(BookId id)
    {
        return new BookDto()
        {
            Id = id,
            Name = "Book 1",
        };
    }

    [HttpPost("form_parameter")]
    public BookDto FormParameter([FromForm] BookId id)
    {
        return new BookDto()
        {
            Id = id,
            Name = "Book 1",
        };
    }

    [HttpPost("form_model")]
    public BookDto FormModel([FromForm] BookDto book)
    {
        return book;
    }
}