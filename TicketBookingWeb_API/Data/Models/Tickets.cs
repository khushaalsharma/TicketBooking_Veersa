namespace TicketBookingWeb_API.Data.Models
{
    public class Tickets
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public int TicketQty { get; set; }
        public Guid EventsId { get; set; }

        //Navigation properties
        public Events events { get; set; }
    }
}
