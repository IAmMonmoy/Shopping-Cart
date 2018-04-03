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

namespace Shopping_Cart_Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        
        private readonly ApplicationDbContext _appDbcontext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordHasher<ApplicationUser> _hasher;
        public AuthController(ApplicationDbContext applicationDbContext
                            , UserManager<ApplicationUser> userManager
                            , IPasswordHasher<ApplicationUser> hasher)
        {
            _appDbcontext = applicationDbContext;
            _userManager = userManager;
            _hasher = hasher;
        }

        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
           try{
               var user = await _userManager.FindByEmailAsync(model.Email);
               if(user != null)
               {
                   if(_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                   {
                       var claims = new[]
                       {
                           new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                           new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                       };

                       var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("FirstAuthTokenMadeByMonmoy"));
                       var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

                       var token = new JwtSecurityToken(
                           issuer : "http://ShoppingCartApiByMonmoy.org",
                           audience: "http://ShoppingCartApiByMonmoy.org",
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