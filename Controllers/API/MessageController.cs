using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_static.Models;

namespace la_mia_pizzeria_static.Controllers.API
{
    [Route("api/[controller]/[action]", Order = 1)]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost]

        public IActionResult Post([FromBody] Message message)

        { return Ok("message"); }
    }
}
