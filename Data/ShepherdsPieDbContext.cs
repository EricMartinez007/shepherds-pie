using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ShepherdsPie.Models;
using Microsoft.AspNetCore.Identity;

namespace ShepherdsPie.Data;
public class ShepherdsPieDbContext : IdentityDbContext<IdentityUser>
{
    private readonly IConfiguration _configuration;
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Pizza> Pizzas { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<Cheese> Cheeses { get; set; }
    public DbSet<Sauce> Sauces { get; set; }
    public DbSet<Topping> Toppings { get; set; }
    public DbSet<PizzaTopping> PizzaToppings { get; set; }


    public ShepherdsPieDbContext(DbContextOptions<ShepherdsPieDbContext> context, IConfiguration config) : base(context)
    {
        _configuration = config;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
            Name = "Admin",
            NormalizedName = "admin"
        });

        modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
        {
            Id = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
            UserName = "Administrator",
            Email = "admina@strator.comx",
            PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
        });

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
            UserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f"
        });
        modelBuilder.Entity<UserProfile>().HasData(new UserProfile
        {
            Id = 1,
            IdentityUserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
            FirstName = "Admina",
            LastName = "Strator",
            Address = "101 Main Street",
        });

        // An Order has two relationships to UserProfile (the order-taker and the
        // deliverer). By default EF cascade-deletes, which would (a) delete orders
        // when an employee is removed and (b) create two cascade paths to the same
        // table — an error on migration. Restrict turns cascade off for both.
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Employee)
            .WithMany()
            .HasForeignKey(o => o.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Deliverer)
            .WithMany()
            .HasForeignKey(o => o.DelivererId)
            .OnDelete(DeleteBehavior.Restrict);

        // --- Menu seed data ---

        modelBuilder.Entity<Size>().HasData(
            new Size { Id = 1, Name = "Small (10\")", Price = 10.00m },
            new Size { Id = 2, Name = "Medium (14\")", Price = 12.00m },
            new Size { Id = 3, Name = "Large (18\")", Price = 15.00m }
        );

        modelBuilder.Entity<Cheese>().HasData(
            new Cheese { Id = 1, Name = "Buffalo Mozzarella" },
            new Cheese { Id = 2, Name = "Four Cheese" },
            new Cheese { Id = 3, Name = "Vegan" },
            new Cheese { Id = 4, Name = "None" }
        );

        modelBuilder.Entity<Sauce>().HasData(
            new Sauce { Id = 1, Name = "Marinara" },
            new Sauce { Id = 2, Name = "Arrabbiata" },
            new Sauce { Id = 3, Name = "Garlic White" },
            new Sauce { Id = 4, Name = "None" }
        );

        modelBuilder.Entity<Topping>().HasData(
            new Topping { Id = 1, Name = "sausage", Price = 0.50m },
            new Topping { Id = 2, Name = "pepperoni", Price = 0.50m },
            new Topping { Id = 3, Name = "mushroom", Price = 0.50m },
            new Topping { Id = 4, Name = "onion", Price = 0.50m },
            new Topping { Id = 5, Name = "green pepper", Price = 0.50m },
            new Topping { Id = 6, Name = "black olive", Price = 0.50m },
            new Topping { Id = 7, Name = "basil", Price = 0.50m },
            new Topping { Id = 8, Name = "extra cheese", Price = 0.50m }
        );

        // Two sample orders so the list/detail views have data on first run.
        // OrderDate must be a fixed literal (not DateTime.Now) — HasData values are
        // compiled into the migration and have to be deterministic.
        modelBuilder.Entity<Order>().HasData(
            // Dine-in: TableNumber set, no deliverer.
            new Order
            {
                Id = 1,
                TableNumber = 12,
                Tip = 5.00m,
                OrderDate = new DateTime(2026, 6, 1, 18, 30, 0),
                EmployeeId = 1,   // Admina (the seeded UserProfile) took the order
                DelivererId = null
            },
            // Delivery: no TableNumber, DelivererId set (triggers the $5 surcharge).
            // Same employee delivers for demo simplicity — only one employee is seeded.
            new Order
            {
                Id = 2,
                TableNumber = null,
                Tip = 3.00m,
                OrderDate = new DateTime(2026, 6, 2, 19, 15, 0),
                EmployeeId = 1,
                DelivererId = 1
            }
        );

        // FK columns are set directly; HasData can't use navigation properties.
        modelBuilder.Entity<Pizza>().HasData(
            new Pizza { Id = 1, OrderId = 1, SizeId = 2, CheeseId = 1, SauceId = 1 },  // Medium, Buffalo Mozz, Marinara
            new Pizza { Id = 2, OrderId = 1, SizeId = 3, CheeseId = 2, SauceId = 2 },  // Large, Four Cheese, Arrabbiata
            new Pizza { Id = 3, OrderId = 2, SizeId = 1, CheeseId = 3, SauceId = 3 }   // Small, Vegan, Garlic White
        );

        modelBuilder.Entity<PizzaTopping>().HasData(
            new PizzaTopping { Id = 1, PizzaId = 1, ToppingId = 2 },  // pepperoni
            new PizzaTopping { Id = 2, PizzaId = 1, ToppingId = 3 },  // mushroom
            new PizzaTopping { Id = 3, PizzaId = 2, ToppingId = 1 },  // sausage
            new PizzaTopping { Id = 4, PizzaId = 3, ToppingId = 7 },  // basil
            new PizzaTopping { Id = 5, PizzaId = 3, ToppingId = 4 }   // onion
        );
    }
}