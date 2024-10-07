using Microsoft.AspNetCore.Identity;
using TicketBooking_WebAPI.Database;
using TicketBooking_WebAPI.Models.Domain;
using TicketBooking_WebAPI.Models.DTO;

namespace TicketBooking_WebAPI.Repositories
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> userManager;
        private readonly BookerDbContext dbContext;

        public SQLUserRepository(UserManager<User> userManager, BookerDbContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }
        public Task<User> GetById(string id)
        {
            var userData = userManager.FindByIdAsync(id);
            return userData;
        }

        public async Task<IdentityResult> UpdateProfile(UserData user, string userId)
        {
            var existingUser = await userManager.FindByIdAsync(userId);
            if (existingUser != null)
            {
                existingUser.PhoneNumber = user.PhoneNumber;    
                existingUser.UserName = user.Email;
                existingUser.Email = user.Email;
                existingUser.Name = user.Name;

                var updateduser = await userManager.UpdateAsync(existingUser);

                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError { Description = "Unable to update" });
        }

        public async Task<IdentityResult> ChangePassword(string ogPassword, string newPassword, string userId)
        {
            var existingUser = await userManager.FindByIdAsync(userId);
            if(existingUser != null)
            {
                await userManager.ChangePasswordAsync(existingUser, ogPassword, newPassword);

                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError { Description = "Unable to update password"});
        }
    }
}
