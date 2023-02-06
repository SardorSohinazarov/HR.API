using HR.API.Models;
using HR.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var foundUser = await _userManager.FindByNameAsync(registerModel.Username);
            if (foundUser != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error" ,Message = "User already exists!" });

            var user = new AppUser
            {
                Email = registerModel.Email,
                UserName = registerModel.Username,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message= "User creation failed." });

            return Ok(new ResponseModel { Status = "Success", Message = "User Created Succesfull" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var foundUser = await _userManager.FindByNameAsync(loginModel.Username);
            if (foundUser != null && await _userManager.CheckPasswordAsync(foundUser, loginModel.Password))
            {
                var roles = await _userManager.GetRolesAsync(foundUser);
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, foundUser.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
           
                foreach(var role in roles)
                    claims.Add(new Claim(ClaimTypes.Role,role));

                var symmetricSecurity = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    _configuration["JWT:ValidIssuer"],
                    _configuration["JWT:ValidAudience"],
                    claims,
                    expires: DateTime.Now.AddHours(1), 
                    signingCredentials:new SigningCredentials(symmetricSecurity, 
                    SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    exception = token.ValidTo
                });
            }

            return Unauthorized();
        }
    }
}
