using System.Xml.Linq;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stio.Prefix.Id.Dapper.SqlMappers;
using Stio.Sample.Data.Context;
using Stio.Sample.Data.Models;
using Stio.Sample.Data.ValueTypes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Stio.Sample.Controllers;

[ApiController]
[Route("api/database")]
public class DatabaseController : Controller
{
    private readonly ApplicationDbContext dbContext;

    public DatabaseController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpPost("book")]
    public async Task<Book> CreateBook(string name)
    {
        var book = new Book()
        {
            Name = name,
        };

        await this.dbContext.Books.AddAsync(book);
        await this.dbContext.SaveChangesAsync();

        return await this.dbContext.Books.FirstAsync(x => x.Id == book.Id);
    }

    [HttpPost("comment")]
    public async Task<Comment> CreateComment(string text, BookId? bookId)
    {
        var comment = new Comment()
        {
            BookId = bookId,
            Text = text,
        };

        await this.dbContext.Comments.AddAsync(comment);
        await this.dbContext.SaveChangesAsync();

        return await this.dbContext.Comments.FirstAsync(x => x.Id == comment.Id);
    }

    [HttpPost("book_dapper")]
    public async Task<Book> CreateBookDapper(string name)
    {
        var book = new Book()
        {
            Name = name,
        };

        await this.dbContext.Database.GetDbConnection().ExecuteAsync(
            """
            INSERT INTO books (id, name) VALUES (@Id, @Name)
            """,
            new
            {
                Id = book.Id,
                Name = book.Name,
            });

        var bookGet = await this.dbContext.Database.GetDbConnection()
            .QueryFirstAsync<Book>(
                """
                SELECT * FROM books WHERE id = @Id
                """,
                new
                {
                    Id = book.Id,
                });

        return bookGet;
    }

    [HttpPost("comment_dapper")]
    public async Task<Comment> CreateCommentDapper(string text, BookId? bookId)
    {
        var comment = new Comment()
        {
            BookId = bookId,
            Text = text,
        };

        await this.dbContext.Database.GetDbConnection().ExecuteAsync(
            """
            INSERT INTO comments (id, book_id, text) VALUES (@Id, @BookId, @Text)
            """,
            new
            {
                Id = comment.Id,
                BookId = comment.BookId,
                Text = comment.Text,
            });

        var commentGet = await this.dbContext.Database.GetDbConnection()
            .QueryFirstAsync<Comment>(
                """
                SELECT * FROM comments WHERE id = @Id
                """,
                new
                {
                    Id = comment.Id,
                });

        return commentGet;
    }

    [HttpGet("comments")]
    public async Task<Comment[]> ListComments()
    {
        var comments = await this.dbContext.Database.GetDbConnection()
            .QueryAsync<Comment>(
                """
                SELECT * FROM comments
                """);

        return comments.ToArray();
    }
}