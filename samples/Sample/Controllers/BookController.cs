using Microsoft.AspNetCore.Mvc;
using Stio.Sample.Data.Models;
using Stio.Sample.Data.ValueTypes;

namespace Stio.Sample.Controllers;

[ApiController]
[Route("api/books")]
public class BookController : Controller
{
    [HttpGet]
    public Book[] ListBook()
    {
        return new Book[]
        {
            new Book()
            {
                Name = "Book 1",
            },
            new Book()
            {
                Name = "Book 2",
            },
        };
    }

    [HttpPost]
    public Book JsonBody(Book book)
    {
        return book;
    }

    [HttpGet("{id}")]
    public Book RouteParameter(BookId id)
    {
        return new Book()
        {
            Id = id,
            Name = "Book 1",
        };
    }

    [HttpGet("query")]
    public Book? QueryParameter(BookId? id)
    {
        if (id is null)
        {
            return null;
        }

        return new Book()
        {
            Id = id,
            Name = "Book 1",
        };
    }

    [HttpPost("form_parameter")]
    public Book FormParameter([FromForm] BookId id)
    {
        return new Book()
        {
            Id = id,
            Name = "Book 1",
        };
    }

    [HttpPost("form_model")]
    public Book FormModel([FromForm] Book book)
    {
        return book;
    }
}