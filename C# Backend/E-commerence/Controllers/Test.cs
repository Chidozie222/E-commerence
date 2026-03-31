using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerence.Controllers
{
    [Authorize]
    [ApiController] 
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok(new
            {
                message = "Protected Hello world!"
            });
        }
    }
}
