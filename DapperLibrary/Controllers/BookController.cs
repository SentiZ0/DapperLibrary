using Dapper;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using DapperLibrary.Models;
using DapperLibrary.Services.Interfaces;

namespace DapperLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<ActionResult> AddBook(Book book)
        {
            try
            {
                await _bookService.AddBookAsync(book);

                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();

            if(books == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(books);
            }
        }

        [HttpGet("{id}")]
        public async Task<dynamic> GetBook(int id)
        {
            var book = await _bookService.GetSingleBookAsync(id);

            if(book.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(book);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBook(int id)
        {
           var result = await _bookService.DeleteBookAsync(id);

            if (result == false)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> ModifyBook(int id, Book book)
        {
            var result = await _bookService.ModifyBookAsync(id, book);

            if(result == false)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}

