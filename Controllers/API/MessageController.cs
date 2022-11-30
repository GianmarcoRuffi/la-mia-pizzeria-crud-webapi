using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Data;

namespace la_mia_pizzeria_static.Controllers.API
{
    [Route("api/[controller]/[action]", Order = 1)]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private PizzaDbContext db;

        public MessageController(PizzaDbContext _db)
        {
            db = _db;
        }
        [HttpPost]

        public IActionResult Create([FromBody] Message message)
        {

            try
            {
                db.Messages.Add(message);
                db.SaveChanges();

            }
            catch (Exception e)
            {
                return UnprocessableEntity(e.Message);
            }

            return Ok(message);
        }
    }
}
