using System.ComponentModel.DataAnnotations;

namespace TicketBooking_WebAPI.Models.DTO
{
    public class ImageUploadDTO
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
