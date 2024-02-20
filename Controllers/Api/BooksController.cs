using LibApp.Data;
using LibApp.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using LibApp.Dtos;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public BooksController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET /api/books
        [HttpGet]
        public IActionResult GetBooks()
        {
            return Ok(_context.Books
                .Include(c => c.Genre)
                .ToList()
                .Select(_mapper.Map<Book, BookDto>));
        }

        // GET /api/books/{id}
        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult GetBook(int id)
        {
            var customer = _context.Books.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BookDto>(customer));
        }

        // POST /api/books
        [HttpPost]
        public IActionResult CreateBook(BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Books.Add(_mapper.Map<Book>(bookDto));
            _context.SaveChanges();
            return CreatedAtRoute(nameof(GetBook), new { id = bookDto.Id }, bookDto);
        }

        // PUT /api/books/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customerInDb = _context.Books.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                return NotFound();
            }

            _mapper.Map(bookDto, customerInDb);

            return Ok(bookDto);
        }

        // DELETE /api/books/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var bookInDb = _context.Books.SingleOrDefault(c => c.Id == id);
            if (bookInDb == null)
            {
                return NotFound();
            }

            _context.Books.Remove(bookInDb);
            _context.SaveChanges();

            return Ok(bookInDb);
        }
    }
}
