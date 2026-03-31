using E_commerence.DbContext;
using E_commerence.DTOs;
using E_commerence.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerence.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{userId}")]
    [Authorize]
    public async Task<IActionResult> GetUser([FromRoute] string userId)
    {
        try
        {
            if (userId.IsWhiteSpace()) return BadRequest();
            
            var findAsync = await _context.Users.FindAsync(userId);

            if (findAsync == null) return NotFound(new  { message = "User not found" });

            return Ok(new
            {
                data = new
                {
                    id = findAsync.Id,
                    fullName = findAsync.FullName,
                    email = findAsync.Email,
                    dob = findAsync.DateOfBirth,
                    phoneNumber = findAsync.PhoneNumber,
                    isVerified = findAsync.IsVerified,
                }
            });
        }
        catch (Exception e)
        {
            return BadRequest(new {message = e.Message}); 
        }
    }

    [HttpPut("{userId}")]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromRoute] string userId, [FromBody] UserDto user)
    {
        try
        {
            if (userId.IsWhiteSpace()) return BadRequest();
            
            var findAsync = await _context.Users.FindAsync(userId);
            if (findAsync == null) return NotFound(new  { message = "User not found" });
            
            findAsync.FullName = user.FullName ?? findAsync.FullName;
            findAsync.Email = user.Email ?? findAsync.Email;
            findAsync.DateOfBirth = user.DateOfBirth ?? findAsync.DateOfBirth;
            findAsync.PhoneNumber = user.PhoneNumber ?? findAsync.PhoneNumber;
            
            _context.Users.Update(findAsync);
            
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(new {message = e.Message});
        }
    }
}