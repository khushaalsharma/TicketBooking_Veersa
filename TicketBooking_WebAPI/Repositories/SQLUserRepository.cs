using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketBooking_WebAPI.Database;
using TicketBooking_WebAPI.Models.Domain;
using TicketBooking_WebAPI.Models.DTO;

namespace TicketBooking_WebAPI.Repositories
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> userManager;
        private readonly BookerDbContext dbContext;
        private readonly IEventImageRepository eventImageRepository;

        public SQLUserRepository(UserManager<User> userManager, BookerDbContext dbContext, IEventImageRepository eventImageRepository)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.eventImageRepository = eventImageRepository;
        }
        public Task<User> GetById(string id)
        {
            var userData = userManager.FindByIdAsync(id);
            return userData;
        }

        public async Task<IdentityResult> UpdateProfile(UpdateUserDataDTO user, string userId)
        {
            var existingUser = await userManager.FindByIdAsync(userId);
            if (existingUser != null)
            {
                existingUser.PhoneNumber = user.PhoneNumber;    
                existingUser.Name = user.Name;
                existingUser.PreferredCurr = user.PreferredCurr;
                existingUser.PreferredLang = user.PreferredLang;

                var updateduser = await userManager.UpdateAsync(existingUser);
                //Console.WriteLine(updateduser);
                if (updateduser.Succeeded)
                {
                    return IdentityResult.Success;
                }

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

        public async Task<IdentityResult> UpdatePhoto(string userId, Guid imgId)
        {
            var userData = await userManager.FindByIdAsync(userId);

            if(userData != null)
            {
                userData.UserImageId = imgId;
                await userManager.UpdateAsync(userData);

                return IdentityResult.Success;
            }

            return IdentityResult.Failed();
        }

        public async Task<User> SetProfilePhotoNull(string userId)
        {
            var userData = await userManager.FindByIdAsync(userId);
            if(userData != null)
            {
                var imgId = (Guid) userData.UserImageId;
                await eventImageRepository.DeleteProfileImage(imgId, userId);

                userData.UserImageId = null;
                userData.UserImage = null;
                await userManager.UpdateAsync(userData);
            }

            return userData;
        }

        public async Task<IdentityResult> UpdateCartId(string userId, Guid cartId)
        {
            var userData = await userManager.FindByIdAsync(userId);
            if(userData != null)
            {
                userData.CartId = cartId;
                await userManager.UpdateAsync(userData);
                return IdentityResult.Success;
            }

            return IdentityResult.Failed();
        }
    }
}
