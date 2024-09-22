using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TicketBookingWeb_API.Data.Models;

namespace TicketBookingWeb_API.Data.DTOs
{
    public class TicketDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [MinLength(10, ErrorMessage ="Phone Number should be 10 Digits")]
        [MaxLength(10, ErrorMessage = "Phone Number should be 10 Digits")]
        public string UserPhone { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]        
        public string UserEmail { get; set; }
        [Required]
        public int TicketQty { get; set; }

        //reference properties
        [Required]
        public Guid EventsId { get; set; }
    }
}
