using Authentication.Helper;
using Authentication.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.DAL
{
    public class TokenService : ITokenService
    {
        private IUserService _userService;
        public TokenService(IUserService userService)
        {
            _userService = userService;
        }
        public string GenerateToken(TokenParametersModel model,string userName)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(model.SecretKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var usrModel = _userService.GetUserByUserName(userName);
            var claims = new List<Claim>();            
            claims.Add(new Claim(Constant.UserNameString, userName));
            claims.Add(new Claim(Constant.UserIDString, usrModel.Id.ToString()));

            var tokeOptions = new JwtSecurityToken(
                issuer: model.IssuerKey,
                audience: model.AudenceKey,
                claims: claims,
                expires: DateTime.Now.AddHours(Convert.ToDouble(model.ExperyTime)),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
       
    }
}
