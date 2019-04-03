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
    public class PublicationController : ControllerBase
    {
        OnlineBookStoreAPIDbContext context = new OnlineBookStoreAPIDbContext();
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publication>>> Get()
        {
            return await context.Publications.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Publication>> Get(int id)
        {
            var pub = await context.Publications.FindAsync(id);
            if (pub == null)
            {
                return NotFound();
            }
            return pub;
        }

        [HttpPost]
        public async Task<ActionResult<Publication>> Post([FromBody] Publication pub)
        {
            context.Publications.Add(pub);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = pub.PublicationId, pub });
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Publication>> Delete(int id)
        {
            var pub = await context.Publications.FindAsync(id);
            if (pub == null)
            {
                return NotFound();
            }
            context.Publications.Remove(pub);
            await context.SaveChangesAsync();
            return NoContent();
        }

        

        [HttpPut("{id}")]

        public async Task<ActionResult<Publication>> Put(int id, [FromBody]Publication newpublication)
        {

            if (id != newpublication.PublicationId)
            {
                return BadRequest();
            }
            context.Entry(newpublication).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
