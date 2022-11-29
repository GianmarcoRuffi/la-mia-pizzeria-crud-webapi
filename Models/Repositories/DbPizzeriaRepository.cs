    using Azure;
using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models.Form;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Server;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class DbPizzeriaRepository : IPizzeriaRepository

    {

        PizzaDbContext db;
       

        public DbPizzeriaRepository()
        {
            db = new PizzaDbContext();
        }


        public List<Pizza> All()
        {
            return AllWithRelations();
        }

        public List<Pizza> AllWithRelations()
        {
            return db.Pizzas.Include(pizza => pizza.Category).Include(pizza => pizza.Ingrediente).ToList();
        }

        public Pizza GetById(int id)
        {
            return db.Pizzas.Where(p => p.Id == id).Include("Category").Include("Ingrediente").FirstOrDefault();
        }



        // metodo create
        public void Create(Pizza pizza, List<int> IngredientiSelezionati)
        {
            if (IngredientiSelezionati == null)
            {
                IngredientiSelezionati = new List<int>();
            }
            // associazione tag selezionati dall'utente al modello
            pizza.Ingrediente = new List<Ingrediente>();
            foreach (int IngredienteId in IngredientiSelezionati)
            {

                Ingrediente ingrediente = db.Ingredienti.Where(item => item.Id == IngredienteId).FirstOrDefault();
               pizza.Ingrediente.Add(ingrediente);

            }


            db.Pizzas.Add(pizza);
            db.SaveChanges();
        }


        // metodo update

        public void Modifica(Pizza pizza, Pizza formData, List<int>? IngredientiSelezionati)
        {
            if (IngredientiSelezionati == null)
            {
                IngredientiSelezionati = new List<int>();
            }

            pizza.Name = formData.Name;
            pizza.Description = formData.Description;
            pizza.Prezzo = formData.Prezzo;
            pizza.Image = formData.Image;
            pizza.CategoryId = formData.CategoryId;
            pizza.Ingrediente.Clear();


            foreach (int ingredienteId in IngredientiSelezionati)
            {
                Ingrediente ingrediente = db.Ingredienti.Where(item => item.Id == ingredienteId).FirstOrDefault();
                pizza.Ingrediente.Add(ingrediente);
            }

            db.SaveChanges();

        }


        // metodo delete

        public void Delete(Pizza pizza)
        {
            db.Pizzas.Remove(pizza);
            db.SaveChanges();
        }

        //metodo per ricerca per nome
        public List<Pizza> SearchByName(string? name)
        {
            IQueryable<Pizza> query = db.Pizzas.Include("Category").Include("Ingrediente");

            //name null restituisce lista pizze
            if (name == null)
                return query.ToList();

            return query.Where(pizza => pizza.Name.ToLower().Contains(name.ToLower())).ToList();
        }



    }
}
