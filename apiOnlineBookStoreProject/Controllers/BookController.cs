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
        OnlineBookStoreAPIDbContext context = new OnlineBookStoreAPIDbContext();
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            return await context.Books.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            var bk = await context.Books.FindAsync(id);
            if (bk == null)
            {
                return NotFound();
            }
            return bk;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Post([FromBody] Book bk)
        {
            context.Books.Add(bk);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = bk.BookId, bk });
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Book>> Delete(int id)
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

        public async Task<ActionResult<Book>> Put(int id, [FromBody]Book newBook)
        {

            if (id != newBook.BookId)
            {
                return BadRequest();
            }
            context.Entry(newBook).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}