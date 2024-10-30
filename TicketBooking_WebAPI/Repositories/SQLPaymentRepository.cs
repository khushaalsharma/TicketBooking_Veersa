using TicketBooking_WebAPI.Database;
using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Repositories
{
    public class SQLPaymentRepository : IPaymentRepository
    {
        private readonly BookerDbContext dbContext;

        public SQLPaymentRepository(BookerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Payments> MakePayment(Payments paymentData)
        {
            await dbContext.Payments.AddAsync(paymentData);
            await dbContext.SaveChangesAsync();

            return paymentData;
        }
    }
}
