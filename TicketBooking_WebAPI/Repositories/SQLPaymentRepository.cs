using Microsoft.EntityFrameworkCore;
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

        public async Task UpdateCoupon(string code)
        {
            var couponData = await dbContext.Coupons.FirstOrDefaultAsync(c => c.Code == code);

            if (couponData != null)
            {
                couponData.UsesLeft = couponData.UsesLeft - 1;
                dbContext.Coupons.Update(couponData);
                await dbContext.SaveChangesAsync();
            }

            return;
        }
    }
}
