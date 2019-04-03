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
    public class AuthorController : ControllerBase
    {
    OnlineBookStoreAPIDbContext context = new OnlineBookStoreAPIDbContext();
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> Get()
        {
            return await context.Authors.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> Get(int id)
        {
            var pub = await context.Authors.FindAsync(id);
            if (pub == null)
            {
                return NotFound();
            }
            return pub;
        }

        [HttpPost]
        public async Task<ActionResult<Author>> Post([FromBody] Author aut)
        {
            context.Authors.Add(aut);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = aut.AuthorId, aut });
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Author>> Delete(int id)
        {
            var aut = await context.Authors.FindAsync(id);
            if (aut == null)
            {
                return NotFound();
            }
            context.Authors.Remove(aut);
            await context.SaveChangesAsync();
            return NoContent();
        }

        

        [HttpPut("{id}")]

        public async Task<ActionResult<Author>> Put(int id, [FromBody]Author newAuthor)
        {

            if (id != newAuthor.AuthorId)
            {
                return BadRequest();
            }
            context.Entry(newAuthor).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}