using E_commerence.DbContext;
using E_commerence.DTOs;
using E_commerence.Enums;
using E_commerence.Interfaces;
using E_commerence.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_commerence.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtService _jwtService;
    private readonly ApplicationDbContext _context;

    public AuthController(IJwtService jwtService, UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context)
    {
        _jwtService = jwtService;
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }
    
    
    
    [HttpPost("register")]
    public async Task<IActionResult> register([FromBody] RegisterDto user)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string token = _jwtService.GenerateRefreshToken();

            User userData = new User()
            {
                FullName = user.FullName,
                Email = user.Email,
                UserName =  user.UserName,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                SecurityStamp = token,
            };
            
            var result = await _userManager.CreateAsync(userData, user.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new {message = result.Errors});
            }
            
            await _userManager.AddToRoleAsync(userData, nameof(Roles.User));
            
            return Created(nameof(AuthController), new { data = new 
                {
                    id = userData.Id,
                    fullName = userData.FullName,
                    userName =  userData.UserName,
                    email = userData.Email,
                    DOB = userData.DateOfBirth,
                    phoneNumber = userData.PhoneNumber,
                    refreshToken = userData.SecurityStamp,
                    isVerified = userData.IsVerified,
                    passwordHash = userData.PasswordHash,
                },
                message = "successfully registered"
            });
        }
        catch (Exception e)
        {
            return BadRequest(new {message = e.Message});
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> login([FromBody] LoginDtos user)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userData = await _userManager.FindByEmailAsync(user.Email);

            if (userData == null)
            {
                return Unauthorized(new {message = "Email or password is incorrect"});
            }
            
            var checkPasswordSignInAsync = await _signInManager.CheckPasswordSignInAsync(userData, user.Password, false);

            if (!checkPasswordSignInAsync.Succeeded)
            {
                return Unauthorized(new {message = "Email or password is incorrect"});
            }
            
            var roles = await _userManager.GetRolesAsync(userData);

            var token = _jwtService.GenerateToken(userData, roles);

            return Ok(new
            {
                message = "successfully logged",
                data = new
                {
                    id = userData.Id,
                    fullName = userData.FullName,
                    email = userData.Email,
                    Token = token.Result
                }
            });

        }
        catch (Exception e)
        {
            return BadRequest(new {message = e.Message});
        }
    }
    
    [HttpPost("RefreshToken")]
    [Authorize]
    public async Task<IActionResult> RefreshToken([FromBody] Test tok)
    {
        try
        {
            var user = _userManager.Users.FirstOrDefault(c => c.SecurityStamp == tok.refresh);
            Console.WriteLine(user?.FullName ?? "");

            if (user == null)
            {
                return Unauthorized("Invalid Refresh Token");
            }
            
            var roles = await _userManager.GetRolesAsync(user);
            
            var token = _jwtService.GenerateToken(user, roles);

            return Ok(new
                {
                    message = "successfully refreshed",
                    data = new
                    {
                        Token = token.Result,
                    }
                }
            );
        }
        catch (Exception e)
        {
            return BadRequest(new {message = e.Message});
        }
    }

    [HttpPut("update-password/{userId}")]
    [Authorize]
    public async Task<IActionResult> UpdatePassword([FromBody] PasswordDto user, [FromRoute] string userId)
    {
        try
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if (userId.IsWhiteSpace()) return BadRequest();

            var data = await _userManager.FindByIdAsync(userId);
            if(data == null) return NotFound(new {message = "User not found"});
            
            var passwordHasher = new PasswordHasher<User>();

            var hashedPassword = passwordHasher.HashPassword(data, user.password);
            
            data.PasswordHash = hashedPassword;
            
            _context.Users.Update(data);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(new {message = e.Message}); 
        }
    }
    
    public class Test 
    {
        public string refresh { get; set; }  
    } 
}