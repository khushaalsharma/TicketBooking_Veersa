using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBooking_WebAPI.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string Name { get; set; }
        public string FileExtension { get; set; }
        public long SizeInBytes { get; set; } //in bytes
        public string Url { get; set; }
    }
}
