using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiOnlineBookStoreProject.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiOnlineBookStoreProject.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        
            OnlineBookStoreAPIDbContext context = new OnlineBookStoreAPIDbContext();
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Admin>>> Get()
            {
                return await context.Admins.ToListAsync();
            }
            [HttpGet("{id}")]
            public async Task<ActionResult<Admin>> Get(int id)
            {
                var pub = await context.Admins.FindAsync(id);
                if (pub == null)
                {
                    return NotFound();
                }
                return pub;
            }

            



            [HttpPut("{id}")]

            public async Task<ActionResult<Admin>> Put(string id, [FromBody]Admin newAdmin)
            {

                if (id != newAdmin.AdminUserName)
                {
                    return BadRequest();
                }
                context.Entry(newAdmin).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return NoContent();
            }
    }
}

