using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetCart(string userId);
        Task AddInCart(CartItem cartItem, string userId);
        Task<Cart> CreateEmptyCart( string userId);
        Task UpdateCartItemQty(string userId, CartItem updatedCartItem);
        Task EmptyCart(string userId);
    }
}
