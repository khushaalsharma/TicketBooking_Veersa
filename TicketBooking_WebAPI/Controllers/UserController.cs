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


    }
}
