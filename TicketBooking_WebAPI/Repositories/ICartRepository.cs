using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetCart(string userId);
        Task AddInCart(CartItem cartItem, string userId);
        Task UpdateCartItemQty(string userId, CartItem updatedCartItem);
        Task DeleteItem(Guid itemId, string userId);
    }
}
