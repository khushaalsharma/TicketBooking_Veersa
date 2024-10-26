using Microsoft.EntityFrameworkCore;
using TicketBooking_WebAPI.Database;
using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Repositories
{
    public class SQLCartRepository : ICartRepository
    {
        private readonly BookerDbContext dbContext;

        public SQLCartRepository(BookerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddInCart(CartItem cartItem, string userId)
        {
            var userCart = await dbContext.Carts.Include(c => c.CartItems).Where(c => c.UserId == userId).FirstOrDefaultAsync();

            if(userCart != null)
            {
                userCart.CartItems.Add(cartItem); //adding item to cartitems collection
                await dbContext.SaveChangesAsync();
                return;
            }
                                    
        }

        public async Task DeleteItem(Guid itemId, string userId)
        {
            var userCart = await dbContext.Carts.Include(c => c.CartItems).Where(c => c.UserId == userId).FirstOrDefaultAsync();

            if(userCart != null)
            {
                var existingItem = userCart.CartItems.FirstOrDefault(ci => ci.Id == itemId);
                if (existingItem != null)
                {
                    userCart.CartItems.Remove(existingItem); //removing from collection
                    await dbContext.SaveChangesAsync();
                    return;
                }
            }
        }

        public async Task<Cart> GetCart(string userId)
        {
            var userCart = await dbContext.Carts.Include(c => c.CartItems).Where(c => c.UserId == userId).FirstOrDefaultAsync();

            return userCart;            
        }

        public async Task UpdateCartItemQty(string userId, CartItem updatedCartItem)
        {
            var userCart = await dbContext.Carts.Include(c => c.CartItems).Where(c => c.UserId == userId).FirstOrDefaultAsync();

            var existingItem = userCart.CartItems.FirstOrDefault(ci => ci.Id ==  updatedCartItem.Id);   
            if(existingItem != null)
            {
                //updating the cart item
                existingItem.Quantity = updatedCartItem.Quantity;
                existingItem.Price = updatedCartItem.Price;
                existingItem.Amount = updatedCartItem.Amount;

                await dbContext.SaveChangesAsync();
                return;
            }
        }
    }
}
