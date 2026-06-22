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
public class OrdersController : ControllerBase
{
    private readonly ShepherdsPieDbContext _dbContext;
    private readonly IMapper _mapper;

    public OrdersController(ShepherdsPieDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetAllOrders([FromQuery] DateTime? date = null)
    {
        var filterDate = (date ?? DateTime.Today).Date;

        var orders = _dbContext.Orders
            .Include(o => o.Employee)
            .Include(o => o.Deliverer)
            .Include(o => o.Pizzas).ThenInclude(p => p.Size)
            .Include(o => o.Pizzas).ThenInclude(p => p.PizzaToppings).ThenInclude(pt => pt.Topping)
            .Where(o => o.OrderDate.Date == filterDate)
            .OrderByDescending(o => o.OrderDate)
            .ToList();

        return Ok(_mapper.Map<List<OrderDTO>>(orders));
    }

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetOrder(int id)
    {
        Order? order = _dbContext.Orders
            .Include(o => o.Employee)
            .Include(o => o.Deliverer)
            .Include(o => o.Pizzas).ThenInclude(p => p.Size)
            .Include(o => o.Pizzas).ThenInclude(p => p.Cheese)
            .Include(o => o.Pizzas).ThenInclude(p => p.Sauce)
            .Include(o => o.Pizzas).ThenInclude(p => p.PizzaToppings).ThenInclude(pt => pt.Topping)
            .SingleOrDefault(o => o.Id == id);

        return order is null ? NotFound() : Ok(_mapper.Map<OrderDTO>(order));
    }

    [HttpPost]
    [Authorize]
    public IActionResult CreateOrder(CreateOrderDTO newOrderDTO)
    {
        var order = new Order
        {
            TableNumber = newOrderDTO.TableNumber,
            EmployeeId = newOrderDTO.EmployeeId,
            OrderDate = DateTime.Now
        };

        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();

        return Created($"/api/orders/{order.Id}", new { order.Id });
    }

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult UpdateOrder(int id, UpdateOrderDTO updatedOrderDTO)
    {
        Order? order = _dbContext.Orders.SingleOrDefault(o => o.Id == id);
        if (order is null) return NotFound();

        order.TableNumber = updatedOrderDTO.TableNumber;
        order.Tip = updatedOrderDTO.Tip;
        order.DelivererId = updatedOrderDTO.DelivererId;

        _dbContext.SaveChanges();
        return NoContent();
    }


    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult DeleteOrder(int id)
    {
        Order? orderToDelete = _dbContext.Orders.SingleOrDefault(o => o.Id == id);
        if (orderToDelete == null)
        {
            return NotFound();
        }

        _dbContext.Orders.Remove(orderToDelete);
        _dbContext.SaveChanges();

        return NoContent();
    }
}