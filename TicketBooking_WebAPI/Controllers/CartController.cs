using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketBooking_WebAPI.Models.DTO;
using TicketBooking_WebAPI.Repositories;

namespace TicketBooking_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ICartRepository cartRepository;
        private readonly IMapper mapper;

        public CartController(IUserRepository userRepository, ICartRepository cartRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.cartRepository = cartRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("/GetCart/{userId: string}")]
        public async Task<IActionResult> GetCart([FromRoute] string userId)
        {
            var userCart = await cartRepository.GetCart(userId);
            if(userCart == null)
            {
                return Ok(new {message = "No item in cart, cart is empty"});
            }

            return Ok(mapper.Map<CartDTO>(userCart));
        }

        [HttpPost]
        [Route("UpdateCart")]
        public async Task<IActionResult> updateCart([FromBody] CartItemDTO cartItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //add logic

            return Ok();
        }
    }
}
