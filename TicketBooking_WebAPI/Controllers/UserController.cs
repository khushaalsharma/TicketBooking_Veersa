using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TicketBooking_WebAPI.Models.DTO;
using TicketBooking_WebAPI.Repositories;

namespace TicketBooking_WebAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("UserProfile")]
        public async Task<IActionResult> UserProfile()
        {
            var cookieVal = Request.Cookies["BookerId"];
            var decodeVal = Convert.FromBase64String(cookieVal);

            var userData = await userRepository.GetById(Encoding.UTF8.GetString(decodeVal));

            if (userData != null)
            {
                return Ok(mapper.Map<UserData>(userData));
            }

            return BadRequest("User not found");
        }

        [HttpPut]
        [Route("updateUser")]
        public async Task<IActionResult> updateUser(UserData newUserData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cookieVal = Request.Cookies["BookerId"];
            var decodeVal = Convert.FromBase64String(cookieVal);

            var updateSuccess = await userRepository.UpdateProfile(newUserData, Encoding.UTF8.GetString(decodeVal));

            if (updateSuccess.Succeeded)
            {
                return Ok("Update Succesful");
            }

            return BadRequest("Can't update");
        }

        [HttpPut]
        [Route("changePassword")]
        public async Task<IActionResult> changePassword(string oldPassword, string newPassword)
        {
            var cookieVal = Request.Cookies["BookerId"];
            var decodeVal = Convert.FromBase64String(cookieVal);

            var passwordChangeSuccess = await userRepository.ChangePassword(oldPassword, newPassword, Encoding.UTF8.GetString(decodeVal));

            if (passwordChangeSuccess.Succeeded)
            {
                return Ok("Password Changed");
            }

            return BadRequest("Error in changing password");
        }
    }
}
