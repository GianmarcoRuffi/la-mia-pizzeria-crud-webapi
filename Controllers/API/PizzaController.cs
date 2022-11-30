using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Controllers.API
{
    [Route("api/[controller]/[action]", Order = 1)]
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


        [HttpGet]
        public IActionResult Get(string? name)
        {

            List<Pizza> pizzas = _pizzeriaRepository.SearchByName(name);

            return Ok(pizzas);

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Pizza pizza = _pizzeriaRepository.GetById(id);

            return Ok(pizza);
        }

        [HttpPost]
        public IActionResult Modifica(Pizza pizza)
        {
            return Ok("ok");
        }

    }
}
