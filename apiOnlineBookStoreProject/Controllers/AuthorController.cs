using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

using coreBookStore.Models;
using apiOnlineBookStoreProject.Models;

namespace apiOnlineBookStoreAdmin.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly OnlineBookStoreAPIDbContext _context;

        public AuthorController(OnlineBookStoreAPIDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> Get()
        {
            return await _context.Authors.ToListAsync();
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

                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    return NotFound();
                }
                return Ok(author);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Author aut)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else

            {
                try
                {
                    _context.Authors.Add(aut);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(Get), new { id = aut.AuthorId, aut });
                }

                catch (Exception)
                {
                    return BadRequest();
                }
            }



        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return Ok(author);
        }






        [HttpPut("{id}")]

        public async Task<IActionResult> Put(int? id, [FromBody]Author newAuthor)
        {


            if (id == null)
            {
                return BadRequest();
            }

            if (id != newAuthor.AuthorId)
            {
                return NotFound();
            }
            _context.Entry(newAuthor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(newAuthor);


        }
    }
}
