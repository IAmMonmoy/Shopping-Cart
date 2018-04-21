using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Shopping_Cart_Api.Data;
using Shopping_Cart_Api.Models;
using Shopping_Cart_Api.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Shopping_Cart_Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        
        private readonly ApplicationDbContext _appDbcontext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPasswordHasher<ApplicationUser> _hasher;
        private readonly IConfiguration _config;
        public AuthController(ApplicationDbContext applicationDbContext
                            , UserManager<ApplicationUser> userManager
                            , IPasswordHasher<ApplicationUser> hasher
                            , IConfiguration config,
                            RoleManager<IdentityRole> roleManager)
        {
            _appDbcontext = applicationDbContext;
            _userManager = userManager;
            _hasher = hasher;
            _config = config;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
           try{
               var user = await _userManager.FindByEmailAsync(model.Email);
               if(user != null)
               {
                   if(_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                   {
                       var claims = new List<Claim>(new[]
                       {
                           new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                           new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                       });


                        //add role to claim
                       var roleNames = await _userManager.GetRolesAsync(user);
                       
                       foreach(var roleName in roleNames)
                       {
                           var role = await _roleManager.FindByNameAsync(roleName);
                           if(role != null)
                           {
                               var roleClaim = new Claim(JwtRegisteredClaimNames.Nonce, role.Name);
                               claims.Add(roleClaim);

                               var roleClaims = await _roleManager.GetClaimsAsync(role);
                               claims.AddRange(roleClaims);
                           }
                       }

                       var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtIssuerOptions:Key"]));
                       var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

                       var token = new JwtSecurityToken(
                           issuer : _config["JwtIssuerOptions:Issuer"],
                           audience: _config["JwtIssuerOptions:Audience"],
                           claims: claims,
                           expires: DateTime.UtcNow.AddMinutes(60),
                           signingCredentials: cred
                       );

                       return Ok( new {
                           token = new JwtSecurityTokenHandler().WriteToken(token),
                           expiration = token.ValidTo
                       });
                   }
               }
               return BadRequest("User Not Found");
           }
           catch(Exception ex)
           {
               return BadRequest(ex);
           }
        }
    }
}