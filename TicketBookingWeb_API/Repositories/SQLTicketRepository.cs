using TicketBookingWeb_API.Data.Models;
using TicketBookingWeb_API.DatabaseContext;

namespace TicketBookingWeb_API.Repositories
{
    public class SQLTicketRepository : ITicketRepository
    {
        private readonly TicketBookingDbContext dbContext;

        public SQLTicketRepository(TicketBookingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Tickets> BookTicketAsync(Tickets ticket)
        {
            await dbContext.Tickets.AddAsync(ticket);
            await dbContext.SaveChangesAsync();
            return ticket;
        }
    }
}
