namespace TicketBooking_WebAPI.Models.DTO
{
    public class TicketTypeRespDTO
    {
        public Guid Id { get; set; }
        public string TicketCategory { get; set; }
        public int Price { get; set; }
        public int TotalTickets { get; set; }
        public int AvailableTickets { get; set; }
    }
}
