using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {

        IPizzeriaRepository _pizzeriaRepository;

        public PizzaController (IPizzeriaRepository pizzeriaRepository)
        { _pizzeriaRepository = pizzeriaRepository; }

        public IActionResult Get()
        {
            List<Pizza> pizzas = _pizzeriaRepository.All();

            return Ok(pizzas);


        }



    }
}
