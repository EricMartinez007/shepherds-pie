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
public class ToppingsController : ControllerBase
{
    private readonly ShepherdsPieDbContext _dbContext;
    private readonly IMapper _mapper;

    public ToppingsController(ShepherdsPieDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetToppings()
    {
        var toppings = _dbContext
            .Toppings
            .ToList();

        return Ok(_mapper.Map<List<ToppingDTO>>(toppings));
    }
}