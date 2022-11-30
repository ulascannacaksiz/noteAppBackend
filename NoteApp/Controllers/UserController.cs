using EntityLayer.Concrete;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using NoteApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NoteApp.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [EnableCors("AllowOrigin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]

        public async Task<IActionResult> Register([FromBody] UserCreateViewModel model)
        {

            if (ModelState.IsValid)
            {
                AppUser appuser = new AppUser()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Mail,
                    UserName = model.Username,
                    Gender = model.Gender,  
                    ImageUrl = "Deneme",
                };
                if(model.Password == model.ConfrimPassword)
                {
                    var result = await _userManager.CreateAsync(appuser, model.Password);
                    if (result.Succeeded)
                    {
                        return Ok("Kullannıcı başarıyla kayıt edildi");
                    }
                }
                else
                {
                    return BadRequest("Şifreler Eşleşmiyor");
                }
                
            }
            return BadRequest("Bilgilerinizi Kontrol ediniz");
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel p)
        {
            var user = await _userManager.FindByEmailAsync(p.Email);
            if(user !=null && await _userManager.CheckPasswordAsync(user, p.Password))
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKeyss"));
                var token  = new JwtSecurityToken(
                    issuer: "http://google.com",
                    audience: "http://google.com",
                    expires: DateTime.Now.AddDays(1),
                    claims: claims,
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(key,SecurityAlgorithms.HmacSha256)
                    );
                return Ok(
                    new{
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return BadRequest("Böyle bir kullanıcı mevcut değil");
        }
    }
}
