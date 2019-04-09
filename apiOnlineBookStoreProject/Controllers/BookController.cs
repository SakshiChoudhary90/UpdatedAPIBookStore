using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiOnlineBookStoreProject.Models;
using coreBookStore.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiOnlineBookStoreProject.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly OnlineBookStoreAPIDbContext context;

        public BookController(OnlineBookStoreAPIDbContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            return await context.Books.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {

                var bk = await context.Books.FindAsync(id);
                if (bk == null)
                {
                    return NotFound();
                }
                return Ok(bk);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book bk)
        {
            context.Books.Add(bk);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = bk.BookId, bk });
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var bk = await context.Books.FindAsync(id);
            if (bk == null)
            {
                return NotFound();
            }
            context.Books.Remove(bk);
            await context.SaveChangesAsync();
            return NoContent();
        }



        [HttpPut("{id}")]

        public async Task<IActionResult> Put(int? id, [FromBody]Book newBook)
        {

            if (id == null)
            {
                return BadRequest();
            }

            if (id != newBook.BookId)
            {
                return NotFound();
            }
            context.Entry(newBook).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(newBook);


        }
    }
}