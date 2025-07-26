using API_ElasticSearch.Models;
using API_ElasticSearch.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ElasticSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookSearchService _bookSearchService;
        
        public BookController(
            IBookSearchService bookSearchService
            )
        {
            _bookSearchService = bookSearchService;
        }
        
        
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            return Ok(new List<Book>());
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Book>>> Get(string query)
        {
            var books = await _bookSearchService.GetBooksAsync(query);
            return Ok(books);
        }
    }
}
