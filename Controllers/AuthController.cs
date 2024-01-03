using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto) 
        {
            var identityUser = new IdentityUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Username
            };

           var identityResult = await userManager.CreateAsync(identityUser, registerDto.Password);
            if(identityResult.Succeeded) 
            {
                //Add roles to the user
                if (registerDto.Roles != null & registerDto.Roles.Any())
                { 
                 identityResult = await userManager.AddToRoleAsync(identityUser, registerDto.Roles);

                 if (identityResult.Succeeded) 
                    {
                        return Ok("Registration Successful! Proceed to Login");
                    }
                }
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto) 
        {
           var user =  await userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user != null) 
            {
                var checkPassword = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPassword) 
                {
                    //Get User Roles
                   var roles = await userManager.GetRolesAsync(user);

                    if (roles != null) {
                        //Create Token
                      var jwtToken =  tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };

                    return Ok(response);
                    
                    }
                }
            }

            return BadRequest("Username or Password is incorrect");
        }
    }
}