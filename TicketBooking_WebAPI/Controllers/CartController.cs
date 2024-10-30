using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TicketBooking_WebAPI.Models.Domain;
using TicketBooking_WebAPI.Models.DTO;
using TicketBooking_WebAPI.Repositories;

namespace TicketBooking_WebAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ICartRepository cartRepository;
        private readonly IMapper mapper;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> userManager;

        public CartController(IUserRepository userRepository, ICartRepository cartRepository, IMapper mapper, UserManager<User> userManager)
        {
            this.userRepository = userRepository;
            this.cartRepository = cartRepository;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        [Route("/GetCart")]
        public async Task<IActionResult> GetCart()
        {
            var cookieVal = Request.Cookies["BookerId"];
            var decodedVal = Convert.FromBase64String(cookieVal);
            var userId = Encoding.UTF8.GetString(decodedVal);

            var userCart = await cartRepository.GetCart(userId);
            if (userCart == null)
            {
                return Ok(new { message = "No item in cart, cart is empty" });
            }

            return Ok(mapper.Map<CartDTO>(userCart));
        }

        [HttpPost]
        [Route("UpdateCart")]
        public async Task<IActionResult> AddToCart([FromBody] CartItemDTO cartItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Decode user ID from cookie
            var cookieVal = Request.Cookies["BookerId"];
            var decodedVal = Convert.FromBase64String(cookieVal);
            var userId = Encoding.UTF8.GetString(decodedVal);

            // Check if a cart exists for this user
            var userCart = await cartRepository.GetCart(userId);

            if (userCart != null) // Cart exists for this user
            {
                var existingItem = userCart.CartItems
                    .FirstOrDefault(ci => ci.EventId == cartItemDto.EventId && ci.TicketTypeId == cartItemDto.TicketTypeId);

                if (existingItem == null) // Ticket is being added for the first time
                {
                    var newCartItem = new CartItem
                    {
                        Quantity = cartItemDto.Quantity,
                        Price = cartItemDto.Price,
                        Amount = cartItemDto.Amount,
                        EventId = cartItemDto.EventId,
                        TicketTypeId = cartItemDto.TicketTypeId,
                        CartId = userCart.Id
                    };

                    await cartRepository.AddInCart(newCartItem, userId);
                    return Ok(new { message = "Ticket added" });
                }
                else
                {
                    // Updating the quantity and amount of the existing cart item
                    existingItem.Quantity += cartItemDto.Quantity;
                    existingItem.Amount = existingItem.Quantity * existingItem.Price;

                    await cartRepository.UpdateCartItemQty(userId, existingItem);
                    return Ok(new { message = "Ticket updated in cart" });
                }
            }
            else
            {
                // Create a new cart
                var newCart = await cartRepository.CreateEmptyCart(userId);
                var userData = await userManager.FindByIdAsync(userId);

                var newCartItem = new CartItem
                {
                    Quantity = cartItemDto.Quantity,
                    Price = cartItemDto.Price,
                    Amount = cartItemDto.Amount,
                    EventId = cartItemDto.EventId,
                    TicketTypeId = cartItemDto.TicketTypeId,
                    CartId = newCart.Id
                };

                userData.CartId = newCart.Id;
                await cartRepository.AddInCart(newCartItem, userId);
                await userManager.UpdateAsync(userData);
                return Ok(new { message = "Ticket added to cart" });
            }
        }
    }
}
