using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TicketBooking_WebAPI.Database;
using TicketBooking_WebAPI.Models.Domain;
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
        private readonly BookerDbContext dbContext;
        private readonly IEventImageRepository eventImageRepository;

        public UserController(IUserRepository userRepository, IMapper mapper, BookerDbContext dbContext, IEventImageRepository eventImageRepository)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.eventImageRepository = eventImageRepository;
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
                var img = await dbContext.UserImages
                                   .FirstOrDefaultAsync(ui => ui.Id == userData.UserImageId);
                                   
                var profileData = new UserData
                {
                    Name = userData.Name,
                    Email = userData.Email,
                    PhoneNumber = userData.PhoneNumber,
                    PreferredCurr = userData.PreferredCurr,
                    PreferredLang = userData.PreferredLang,
                    url = img.url
                };
                return Ok(profileData);
            }

            return BadRequest(new { message = "User not found" });
        }

        [HttpPut]
        [Route("updateUser")]
        public async Task<IActionResult> updateUser(UpdateUserDataDTO newUserData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cookieVal = Request.Cookies["BookerId"];
            var decodeVal = Convert.FromBase64String(cookieVal);

            var updateSuccess = await userRepository.UpdateProfile(newUserData, Encoding.UTF8.GetString(decodeVal));

            if (updateSuccess.Succeeded)
            {
                return Ok(new { message = "Update Successful" });
            }

            return BadRequest(new { message = "Cannot update" });
        }

        [HttpPut]
        [Route("changePassword")]
        public async Task<IActionResult> changePassword([FromBody] ChangePasswordDTO passwordDTO)
        {
            var cookieVal = Request.Cookies["BookerId"];
            var decodeVal = Convert.FromBase64String(cookieVal);

            var passwordChangeSuccess = await userRepository.ChangePassword(passwordDTO.oldPassword,passwordDTO.newPassword, Encoding.UTF8.GetString(decodeVal));

            if (passwordChangeSuccess.Succeeded)
            {
                return Ok(new { message ="Password Changed" });
            }

            return BadRequest(new { message = "Cannot update password" });
        }

        [HttpPut]
        [Route("UpdateProfilePhoto")]
        
        public async Task<IActionResult> UpdatePhoto([FromForm] UpdateProfilePhoto data) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cookieVal = Request.Cookies["BookerId"];
            var decodeVal = Convert.FromBase64String(cookieVal);

            string userID = Encoding.UTF8.GetString(decodeVal);

            var userdata = await userRepository.SetProfilePhotoNull(userID);

            var newImage = new UserImage
            {
                File = data.ProfilePhoto,
                Name = Path.GetFileNameWithoutExtension(data.ProfilePhoto.FileName),
                FileExtension = Path.GetExtension(data.ProfilePhoto.FileName),
                Length = data.ProfilePhoto.Length,
            };

            newImage = await eventImageRepository.AddProfileImage(newImage, userID);

            var result = await userRepository.UpdatePhoto(userID, newImage.Id);

            if (result.Succeeded)
            {
                return Ok(newImage);
            }

            return NotFound();
        }
    }
}
