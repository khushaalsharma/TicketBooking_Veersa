namespace TicketBooking_WebAPI.Models.DTO
{
    public class TicketResponseDTO
    {
        public Guid Id { get; set; }
        public int TicketQty { get; set; }
        public int Amount { get; set; }
        public string DateAndTime { get; set; }

        //Foreign keys
        public Guid EventId { get; set; }
    }
}
