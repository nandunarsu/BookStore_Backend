using BussinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Response;
using ModelLayer.UserRegistration;
using RepositoryLayer.Entity;

namespace BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookInterfacebl _book;
        public BookController(BookInterfacebl book)
        {
            _book = book;
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(Book book)
        {
            try
            {
                await _book.addBook(book);

                var response = new ResponseModel<string>
                {
                    Success = true,
                    Message = "Book Added Successfully",
                    Data = true

                };
                return Ok(response);
            }
            catch (Exception ex)
            {

                var response = new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetBook()
        {
            try
            {
                var book = await _book.getAllBook();

                var response = new ResponseModel<IEnumerable<Book>>
                {
                    Success = true,
                    Message = "Book Details",
                    Data = book

                };
                return Ok(book);
            }
            catch (Exception ex)
            {

                var response = new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = ex.Message
                };
                return Ok(response);
            }

        }
        [HttpGet("{bId}")]
        public async Task<IActionResult> GetBookbyid(int bId)
        {
            try
            {
                var book = await _book.getBookById(bId);

                var response = new ResponseModel<Book>
                {
                    Success = true,
                    Message = "Book by id",
                    Data = book

                };
                return Ok(book);
            }
            catch (Exception ex)
            {

                var response = new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }

        }
    }
}
