using Authentication.DAL;
using Authentication.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net;
using Authentication.Helper;

namespace AuthenticationService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly TokenParametersModel _tokenParameters;

        public AuthController(IUserService userService, ITokenService tokenService,IOptions<TokenParametersModel> tokenParameter)
        {
            _userService = userService;
            _tokenService = tokenService;
            _tokenParameters = tokenParameter.Value;
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Post([FromBody] UserModel usr)
        {
            if (string.IsNullOrEmpty(usr.UserName))            
                return BadRequest(new { Error =Constant.UserNameEmpty });
            if (string.IsNullOrEmpty(usr.Password))
                return BadRequest(new { Error = Constant.PasswordEmpty });
            if (_userService.IsValidUser(usr))
            {
                string token = _tokenService.GenerateToken(_tokenParameters,usr.UserName);
                return Ok(new { access_token = token, StatusCode = HttpStatusCode.OK });
            }
            return Unauthorized(new { Error = Constant.UnAuthorizedAccess, StatusCode = HttpStatusCode.Unauthorized });
        }

       
    }
}
