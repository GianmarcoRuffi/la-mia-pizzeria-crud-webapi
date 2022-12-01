using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
namespace la_mia_pizzeria_static.Data



{
    public class PizzaDbContext : IdentityDbContext<IdentityUser>
    {

        public DbSet<Pizza> Pizzas { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Ingrediente> Ingredienti { get; set; }

        public DbSet<Message> Messages { get; set; }


        public PizzaDbContext(DbContextOptions<PizzaDbContext> options)
    : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source = localhost; Initial Catalog =db-pizzeria; Integrated Security=True; Encrypt=false;");
        //}
    }
}
