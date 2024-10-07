using Microsoft.AspNetCore.Identity;
using TicketBooking_WebAPI.Models.Domain;
using TicketBooking_WebAPI.Models.DTO;

namespace TicketBooking_WebAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetById(string id);
        Task<IdentityResult> UpdateProfile(UserData user, string userId);
        Task<IdentityResult> ChangePassword(string ogPassword, string newPassword, string userId);
    }
}
