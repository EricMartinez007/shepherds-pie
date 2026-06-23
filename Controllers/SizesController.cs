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
public class SizesController : ControllerBase
{
    private readonly ShepherdsPieDbContext _dbContext;
    private readonly IMapper _mapper;

    public SizesController(ShepherdsPieDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetSizes()
    {
        var sizes = _dbContext
            .Sizes
            .ToList();

        return Ok(_mapper.Map<List<SizeDTO>>(sizes));
    }
}