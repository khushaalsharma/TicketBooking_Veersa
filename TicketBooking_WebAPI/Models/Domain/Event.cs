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
        public string EventVenue { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Category { get; set; }
        public int MinTicketPrice { get; set; }
        [ForeignKey(nameof(User))] //UserId is not of Guid type in Identity Core
        public string UserId { get; set; }
        
        //navigation property
        public User User { get; set; }
    }
}
