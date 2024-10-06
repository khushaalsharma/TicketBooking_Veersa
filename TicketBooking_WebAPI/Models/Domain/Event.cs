using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBooking_WebAPI.Models.Domain
{
    public class Event
    {
        public Guid Id { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string DateAndTime { get; set; }
        public string EventVenue { get; set; }
        public int TicketPrice { get; set; }
        public int TotalTickets { get; set; }
        public int AvailableTickets { get; set; }
        [ForeignKey(nameof(User))] //USerId is not of Guid type in Identity Core
        public string UserId { get; set; }

        //navigation property
        public User User { get; set; }
    }
}
