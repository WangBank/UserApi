using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Users.Api.Controllers
{
    [Route("[Controller]")]
    public class HealthCheckController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("")]
        [HttpHead("")]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}