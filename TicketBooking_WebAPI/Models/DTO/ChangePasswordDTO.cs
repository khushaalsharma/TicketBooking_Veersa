using System.ComponentModel.DataAnnotations;

namespace TicketBooking_WebAPI.Models.DTO
{
    public class ChangePasswordDTO
    {
        [Required]
        [DataType(DataType.Password)]
        public string oldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }
    }
}
