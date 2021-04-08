using Authentication.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;


namespace AuthenticationService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {        
        [HttpGet]
        public IActionResult Get()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(new { data = "Hello World",StatusCode=HttpStatusCode.OK });
            }
            return Unauthorized(new { Error = Constant.UnAuthorizedAccess, StatusCode =HttpStatusCode.Unauthorized });
        }

    }
}
