﻿using Microsoft.EntityFrameworkCore;
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

                dbContext.Carts.Update(userCart);
                await dbContext.SaveChangesAsync();
                return;
            }
                                    
        }

        public async Task<Cart> CreateEmptyCart(string userId)
        {
            var cart = new Cart
            {
                UserId = userId,
            };

            await dbContext.Carts.AddAsync(cart);
            await dbContext.SaveChangesAsync();

            return cart;
        }

        public async Task<CartItem> DeleteCartItem(Guid itemId)
        {
            var cartItem = await dbContext.CartItems.FirstOrDefaultAsync(ci => ci.Id == itemId);

            if (cartItem != null)
            {
                dbContext.CartItems.Remove(cartItem);

                // Debugging step to confirm deletion state
                var state = dbContext.Entry(cartItem).State;
                //Console.WriteLine($"State before saving: {state}"); // Should be "Deleted"

                try
                {
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                   // Console.WriteLine($"Error saving changes: {ex.Message}");
                    return null;
                }

                // Confirm if item was deleted by re-querying or checking state
                state = dbContext.Entry(cartItem).State;
                //Console.WriteLine($"State after saving: {state}"); // Should be "Detached" or "Unchanged" after deletion

                return cartItem;
            }

            Console.WriteLine("Item not found.");
            return null;
        }


        public async Task EmptyCart(string userId)
        {
            var userCart = await dbContext.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);

            if (userCart != null && userCart.CartItems.Any())
            {
                dbContext.CartItems.RemoveRange(userCart.CartItems);
                await dbContext.SaveChangesAsync();
            }
        }


        public async Task<Cart> GetCart(string userId)
        {
            var userCart = await dbContext.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Event)        // Include Event data for each CartItem
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.TicketType)   // Include TicketType data for each CartItem
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync();

            return userCart;
        }

        public async Task UpdateCartItemQty(string userId, CartItem updatedCartItem)
        {
            var userCart = await dbContext.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (userCart != null)
            {
                var existingItem = userCart.CartItems
                    .FirstOrDefault(ci => ci.EventId == updatedCartItem.EventId && ci.TicketTypeId == updatedCartItem.TicketTypeId);

                if (existingItem != null)
                {
                    // Update the cart item properties
                    existingItem.Quantity = updatedCartItem.Quantity;
                    existingItem.Amount = existingItem.Quantity * existingItem.Price;

                    await dbContext.SaveChangesAsync();
                }
            }
        }

    }
}
