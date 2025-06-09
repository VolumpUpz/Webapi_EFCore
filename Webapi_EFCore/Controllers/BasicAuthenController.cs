using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Webapi_EFCore.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BasicAuthenController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult TestAuthen()
        {
            return Ok(new
            {
                message = "Authentication success"
            });
        }

    }
}
