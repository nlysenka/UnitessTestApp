using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UnitessTestApp.Api.Controllers
{
    [Route("api/cars")]
    public class CarsController : Controller
    {
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
