using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payments> MakePayment(Payments paymentData);
    }
}
