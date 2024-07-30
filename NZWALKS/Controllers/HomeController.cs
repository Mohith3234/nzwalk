using Microsoft.AspNetCore.Mvc;

namespace NZWALKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult GetResult()
        {
            string[] st = new string[] { "jone", "mike" };
            return Ok(st);
        }
    }
}
