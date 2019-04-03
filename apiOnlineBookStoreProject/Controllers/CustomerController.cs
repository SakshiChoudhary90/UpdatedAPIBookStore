using System.Collections.Generic;
using System.Threading.Tasks;
using apiOnlineBookStoreProject.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiOnlineBookStoreProject.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        
            OnlineBookStoreAPIDbContext context = new OnlineBookStoreAPIDbContext();
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Customer>>> Get()
            {
                return await context.Customers.ToListAsync();
            }
            [HttpGet("{id}")]
            public async Task<ActionResult<Customer>> Get(int id)
            {
                var cust = await context.Customers.FindAsync(id);
                if (cust == null)
                {
                    return NotFound();
                }
                return cust;
            }

        [HttpPost]
        public async Task<ActionResult<Customer>> Post([FromBody] Customer cust)
        {
            context.Customers.Add(cust);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = cust.CustomerId, cust });
        }
        [HttpDelete("{id}")]

            public async Task<ActionResult<Customer>> Delete(int id)
            {
                var cust = await context.Customers.FindAsync(id);
                if (cust == null)
                {
                    return NotFound();
                }
                context.Customers.Remove(cust);
                await context.SaveChangesAsync();
                return NoContent();
            }



            [HttpPut("{id}")]

            public async Task<ActionResult<Customer>> Put(int id, [FromBody]Customer newCustomer)
            {

                if (id != newCustomer.CustomerId)
                {
                    return BadRequest();
                }
                context.Entry(newCustomer).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return NoContent();
            }
    }
}
