using System.Linq;
using System.Text;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using TaskMediatrFluentValidation.Entity;

namespace TaskAuth.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Authorize]
    public class UserController : ControllerBase
    {
        public UserController()
        {

        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Authenticate(Users u)
        {
            var users = new List<Users>(){
                new Users(){username ="anais", password ="doe"},
                new Users(){username ="anais", password ="ug7"},
                new Users(){username ="nicole", password ="kuiyuy8"},
                new Users(){username ="anais", password ="ug7Td90h"},
                new Users(){username ="anais", password ="OIUG678trfg"}
            
            };
            var _user = users.Find(e => e.username == u.username);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, _user.username),
                    new Claim(ClaimTypes.Sid, _user.password)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ini secretnya kurang panjaaaaaangggggg banget")), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);

            var tokenResponse = new {
            token = tokenHandler.WriteToken(token),
            user = _user.username
            };

            return Ok(tokenResponse);
        }
    }
}