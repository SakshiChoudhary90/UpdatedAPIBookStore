using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiOnlineBookStoreProject.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiOnlineBookStoreProject.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {

    }
}