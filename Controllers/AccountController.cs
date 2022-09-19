using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using Events.Models;
using Microsoft.AspNetCore.Authorization;
using Events.Repositories;
using Events.App_Data;

namespace Events.Controllers {
    [ApiController]
    public class AccountController : Controller {

        private IPersonsRepository repository;

        public AccountController(IPersonsRepository repo) {
            this.repository = repo;
        }

        /// <summary>
        /// Returns the generated token for the user
        /// </summary>
        /// <param name="pers">The Person-object to which the token needs to be issued</param>
        [HttpPost("/token")]
        public IActionResult Token(Person pers) {
            var claims = GetIdentity(pers.Login, pers.Password);
            if (claims == null) {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new {
                access_token = encodedJwt,
                username = pers.Login
            };

            return Json(response);
        }


        /// <summary>
        /// Simulation of providing data only to an authorized user
        /// </summary>
        [HttpGet("/data")]
        [Authorize]
        public IActionResult Get() {
            return StatusCode(202);
        }

        private List<Claim>? GetIdentity(string username, string password) {
            Person? person = repository.Persons.FirstOrDefault(x => x.Login == username && x.Password == password);
            if (person != null) {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Login) };
                return claims;
            }
            return null;
        }
    }
}
