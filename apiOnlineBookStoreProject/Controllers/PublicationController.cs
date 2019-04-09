

using apiOnlineBookStoreProject.Models;
using coreBookStore.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apiOnlineBookStoreAdmin.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationController : ControllerBase
    {


        private readonly OnlineBookStoreAPIDbContext _context;

        public PublicationController(OnlineBookStoreAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publication>>> Get()
        {
            return await _context.Publications.ToListAsync();
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

                var pub = await _context.Publications.FindAsync(id);
                if (pub == null)
                {
                    return NotFound();
                }
                return Ok(pub);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Publication pub)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
               try
                {
                    _context.Publications.Add(pub);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(Get), new { id = pub.PublicationId, pub });
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
            var pub = await _context.Publications.FindAsync(id);
            if (pub == null)
            {
                return NotFound();
            }
            _context.Publications.Remove(pub);
            await _context.SaveChangesAsync();
            return Ok(pub);
        }


        [HttpPut("{id}")]

        public async Task<IActionResult> Put(int? id, [FromBody]Publication newPublication)
        {


            if (id == null)
            {
                return BadRequest();
            }

            if (id != newPublication.PublicationId)
            {
                return NotFound();
            }
            _context.Entry(newPublication).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(newPublication);


        }
    }
}