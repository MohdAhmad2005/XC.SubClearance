using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace XC.XSC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ContentResult Index()
        {
            return base.Content("<div>Hello</div>", "text/html");
        }
    }
}
