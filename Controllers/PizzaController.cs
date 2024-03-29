﻿using Azure;
using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Form;
using la_mia_pizzeria_static.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Server;

namespace la_mia_pizzeria_static.Controllers
{

    [Authorize]
    [Route("[controller]/[action]/{id?}", Order = 0)]
    public class PizzaController : Controller

    {


        public PizzaDbContext db;
        IPizzeriaRepository pizzeriaRepository;


        public PizzaController(IPizzeriaRepository _pizzeriaRepository, PizzaDbContext _db) : base()
        {
            db = _db;
            pizzeriaRepository = _pizzeriaRepository;
        }


        // Index e Detail
        public IActionResult Index()
        {
            List<Pizza> pizze = pizzeriaRepository.All();
            return View(pizze);
        }

        public IActionResult Dettaglio(int id)
        {

            Pizza pizza = pizzeriaRepository.GetById(id);
            if (pizza == null)
                return NotFound();
            else

                return View(pizza);
        }




        // Create

        [HttpGet]
        public IActionResult Create()

        {
            PizzaForm formData = new PizzaForm();

            formData.Pizza = new Pizza();
            formData.Categories = db.Categories.ToList();
            formData.Ingredienti = new List<SelectListItem>();

            List<Ingrediente> ListaIngredienti = db.Ingredienti.ToList();

            foreach (Ingrediente ingrediente in ListaIngredienti)
            {
                formData.Ingredienti.Add(new SelectListItem(ingrediente.Name, ingrediente.Id.ToString()));
            };
            return View(formData);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaForm formData)
        {


            if (!ModelState.IsValid)
            {
                formData.Categories = db.Categories.ToList();
                formData.Ingredienti = new List<SelectListItem>();

                List<Ingrediente> ListaIngredienti = db.Ingredienti.ToList();

                foreach (Ingrediente ingrediente in ListaIngredienti)
                {
                    formData.Ingredienti.Add(new SelectListItem(ingrediente.Name, ingrediente.Id.ToString()));
                }

                return View(formData);
            }

            pizzeriaRepository.Create(formData.Pizza, formData.IngredientiSelezionati);

            return RedirectToAction("Index");
        }

        // Update

        [HttpGet]
        public IActionResult Modifica(int id)

        {

            Pizza pizza = pizzeriaRepository.GetById(id);

            if (pizza == null)
                return NotFound();

            PizzaForm formData = new PizzaForm();

            formData.Pizza = pizza;
            formData.Categories = db.Categories.ToList();
            formData.Ingredienti = new List<SelectListItem>();

            List<Ingrediente> listaIngredienti = db.Ingredienti.ToList();

            foreach (Ingrediente ingrediente in listaIngredienti)
            {
                formData.Ingredienti.Add(new SelectListItem(
                    ingrediente.Name,
                    ingrediente.Id.ToString(),
                    pizza.Ingrediente.Any(t => t.Id == ingrediente.Id)
                   ));
            }

            return View(formData);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Modifica(int id, PizzaForm formData)

        {

            if (!ModelState.IsValid)
            {
                formData.Pizza.Id = id;
                formData.Categories = db.Categories.ToList();
                formData.Ingredienti = new List<SelectListItem>();

                List<Ingrediente> ListaIngredienti = db.Ingredienti.ToList();

                foreach (Ingrediente ingrediente in ListaIngredienti)
                {
                    formData.Ingredienti.Add(new SelectListItem(ingrediente.Name, ingrediente.Id.ToString()));
                }

                return View(formData);
            }

            // Update con nuovo riferimento alla repository

            Pizza pizza = pizzeriaRepository.GetById(id);

            if (pizza == null)
            {
                return NotFound();
            }

            pizzeriaRepository.Modifica(pizza, formData.Pizza, formData.IngredientiSelezionati);


            return RedirectToAction("Index");
        }


        //Delete//

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)

        {
            Pizza pizza = pizzeriaRepository.GetById(id);

            if (pizza == null)

                return NotFound();

            else

                pizzeriaRepository.Delete(pizza);

            return RedirectToAction("Index");



        }




    }



}
