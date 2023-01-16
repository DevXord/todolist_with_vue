using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Web.Http.Cors;
using ToDoAPI.Database;

namespace ToDoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [EnableCors(origins: "https://localhost:7235/api/", headers: "*", methods: "*")]
    public class DatebaseController : Controller
    {

        [HttpPost]
        public IActionResult SetDatebaseString([FromBody] Datebase date)
        {
        
            if (date != null) {
                Datebase.SetDatebaseAddress(date);
                return Ok();
            }
            else return BadRequest();
            

 
        }
    }
}
