using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepherdsPie.Data;
using ShepherdsPie.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using ShepherdsPie.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace ShepherdsPie.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PizzasController : ControllerBase
{
    private readonly ShepherdsPieDbContext _dbContext;
    private readonly IMapper _mapper;

    public PizzasController(ShepherdsPieDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }
    [HttpPost]
    [Authorize]
    public IActionResult CreatePizza(CreatePizzaDTO newPizzaDTO)
    {
        Pizza pizza = new Pizza
        {
            OrderId = newPizzaDTO.OrderId,
            SizeId = newPizzaDTO.SizeId,
            CheeseId = newPizzaDTO.CheeseId,
            SauceId = newPizzaDTO.SauceId,
            PizzaToppings = newPizzaDTO.ToppingIds
                .Select(toppingId => new PizzaTopping { ToppingId = toppingId })
                .ToList()
        };

        _dbContext.Pizzas.Add(pizza);
        _dbContext.SaveChanges();   

        return Created($"/api/pizzas/{pizza.Id}", new { pizza.Id });
    }

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult UpdatePizza(int id, UpdatePizzaDTO updatedPizzaDTO)
    {
        Pizza? pizza = _dbContext.Pizzas
            .Include(p => p.PizzaToppings)          
            .SingleOrDefault(p => p.Id == id);

        if (pizza is null) return NotFound();

        pizza.SizeId = updatedPizzaDTO.SizeId;
        pizza.CheeseId = updatedPizzaDTO.CheeseId;
        pizza.SauceId = updatedPizzaDTO.SauceId;

        // the toppings: clear the old join rows, build new ones from the submitted ids
        pizza.PizzaToppings.Clear();
        foreach (int toppingId in updatedPizzaDTO.ToppingIds)
        {
            pizza.PizzaToppings.Add(new PizzaTopping { ToppingId = toppingId });
        }

        _dbContext.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult DeletePizza(int id)
    {
        Pizza? pizzaToDelete = _dbContext.Pizzas.SingleOrDefault(p => p.Id == id);
        if (pizzaToDelete == null)
        {
            return NotFound();
        }

        _dbContext.Pizzas.Remove(pizzaToDelete);
        _dbContext.SaveChanges();

        return NoContent();
    }
}