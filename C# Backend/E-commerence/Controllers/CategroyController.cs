using E_commerence.DbContext;
using E_commerence.DTOs;
using E_commerence.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerence.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class CategroyController : Controller
{
    private readonly ApplicationDbContext _context;
    public CategroyController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateAsync(CategroyDto categroy)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = new Categroy()
            {
                Name = categroy.Name,
                Description = categroy.Description,
            };
            
            _context.Categroies.Add(data);
            
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("Get", new { id = data.Id }, data);

        }
        catch (Exception e)
        {
            return BadRequest(new {message = e.Message});
        }
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync([FromQuery] int id)
    {
        try
        {
            if (!id.Equals(default(int)))
            {
                return BadRequest(new {message = "Invalid id"});
            }
            
            var findAsync = await _context.Categroies.FindAsync(id);
            
            if (findAsync == null) return NotFound();

            return Ok(new
            {
                message = "Successfully retrieved categroy",
                data = new
                {
                    id = findAsync.Id,
                    name = findAsync.Name,
                    description = findAsync.Description
                }
            });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [HttpGet("categories")]
    [AllowAnonymous]
    public IActionResult GetCategories()
    {
        try
        {
            var findAll = _context.Categroies
                .Select(c => new
                {
                    id = c.Id,
                    name = c.Name,
                    description = c.Description
                }).ToList();

            return Ok(new
            {
                message = "Successfully  retrieved categories",
                data = findAll
            });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [HttpPut("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateAsync([FromBody] CategroyUpdateDto categroy, [FromQuery] int id)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (!id.Equals(default(int)))
            {
                return BadRequest(new {message = "Invalid id"});
            }
            
            var findAsync = await _context.Categroies.FindAsync(id);
            
            if (findAsync == null) return NotFound();
            
            findAsync.Name = categroy.Name ?? findAsync.Name;
            findAsync.Description = categroy.Description ?? findAsync.Description;

            _context.Categroies.Update(findAsync);
            
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        try
        {
            if (!id.Equals(default(int)))
            {
                return BadRequest(new {message = "Invalid id"});
            }
            
            var findAsync = await _context.Categroies.FindAsync(id);
            
            _context.Categroies.Remove(findAsync);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
}