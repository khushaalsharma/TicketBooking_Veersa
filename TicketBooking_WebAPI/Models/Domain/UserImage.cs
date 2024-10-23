using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBooking_WebAPI.Models.Domain
{
    public class UserImage
    {
        public Guid Id { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string Name { get; set; }
        public string FileExtension { get; set; }
        public long Length { get; set; }
        public string url { get; set; }
    }
}
