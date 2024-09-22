namespace TicketBookingWeb_API.Data.Models
{
    public class Events
    {
        public Guid Id { get; set; }
        public string EventName { get; set; }
        public string EventVenue { get; set; }
        public string DateAndTime { get; set; }
        public int TicketPrice { get; set; }
        public string EventDescription { get; set; }
    }
}
