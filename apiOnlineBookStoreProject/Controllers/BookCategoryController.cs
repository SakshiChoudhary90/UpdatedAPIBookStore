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
    public class BookCategoryController : ControllerBase
    {
        private readonly OnlineBookStoreAPIDbContext context;

        public BookCategoryController(OnlineBookStoreAPIDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookCategory>>> Get()
        {
            return await context.BookCategories.ToListAsync();
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

                var bk = await context.BookCategories.FindAsync(id);
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
        public async Task<IActionResult> Post([FromBody] BookCategory bkc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    context.BookCategories.Add(bkc);
                    await context.SaveChangesAsync();
                    return CreatedAtAction(nameof(Get), new { id = bkc.BookCategoryId, bkc });
                }

                catch (Exception)
                {
                    return BadRequest();
                }
            }
                                                  

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var bkc = await context.BookCategories.FindAsync(id);
            if (bkc == null)
            {
                return NotFound();
            }
            context.BookCategories.Remove(bkc);
            await context.SaveChangesAsync();
            return Ok(bkc);
        }



        [HttpPut("{id}")]

        public async Task<IActionResult> Put(int? id, [FromBody]BookCategory newBookCategory)
        {


            if (id == null)
            {
                return BadRequest();
            }

            if (id != newBookCategory.BookCategoryId)
            {
                return NotFound();
            }
            context.Entry(newBookCategory).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(newBookCategory);


        }
    }
}