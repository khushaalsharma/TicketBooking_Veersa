using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TicketBooking_WebAPI.Models.Domain;
using TicketBooking_WebAPI.Models.DTO;
using TicketBooking_WebAPI.Repositories;

namespace TicketBooking_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenRepository tokenRepository;
        private readonly UserManager<User> userManager;

        public AuthController(ITokenRepository tokenRepository, UserManager<User> userManager)
        {
            this.tokenRepository = tokenRepository;
            this.userManager = userManager;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> register([FromBody] RegisterDTO registerDto)
        {
            if (!ModelState.IsValid)
            {
                var Errors = ModelState.Values.SelectMany(v => v.Errors)
                          .Select(e => e.ErrorMessage);
                return BadRequest(new { message = "Validation failed", errors = Errors });
            }

            var checkEmail = userManager.FindByEmailAsync(registerDto.Email);
            if(checkEmail != null)
            {
                return BadRequest(new {message = "User exists with email. Please proceed to Login"});
            }

            var newUserData = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
            };

            var createResult = await userManager.CreateAsync(newUserData, registerDto.Password);

            if (createResult.Succeeded)
            {
                return Ok(new { message = "User Created" });
            }

            var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
            return BadRequest(new { error = "Error creating user", details = errors });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> login([FromBody] LoginRequestDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await userManager.FindByEmailAsync(loginDto.Username);
            if(existingUser == null)
            {
                return BadRequest("No user found");
            }

            var validUser = await userManager.CheckPasswordAsync(existingUser, loginDto.Password);

            if(validUser == true)
            {
                var roles = await userManager.GetRolesAsync(existingUser);
                if(roles != null)
                {
                    var token = tokenRepository.getJwtToken(existingUser, roles.ToList());
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTimeOffset.UtcNow.AddDays(1)
                    };
                    var encodedId = Encoding.UTF8.GetBytes(existingUser.Id);

                    Response.Cookies.Append("BookerId", Convert.ToBase64String(encodedId), cookieOptions); //setting cookie containing user id

                    var resp = new LoginRespDTO
                    {
                        JWTtoken = token,
                    };

                    return Ok(resp);
                }
            }

            return BadRequest("Wrong Credentials");
        }
    }
}
