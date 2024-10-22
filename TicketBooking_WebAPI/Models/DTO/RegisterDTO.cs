using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace TicketBooking_WebAPI.Models.DTO
{
    public class RegisterDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? PreferredLang { get; set; }
        public string? PreferredCurr { get; set; }
        public IFormFile? ProfileImage { get; set; }
    }
}


