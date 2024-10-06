using Microsoft.EntityFrameworkCore;
using TicketBooking_WebAPI.Database;
using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Repositories
{
    public class SQLTicketRepository : ITicketRepository
    {
        private readonly BookerDbContext dbContext;

        public SQLTicketRepository(BookerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Ticket> BuyTicket(Ticket ticket)
        {
            var eventData = await dbContext.Events.FindAsync(ticket.EventId);

            eventData.AvailableTickets = eventData.AvailableTickets - ticket.TicketQty; //updating available tickets
            
            await dbContext.Tickets.AddAsync(ticket);
            dbContext.Events.Update(eventData);
            await dbContext.SaveChangesAsync();

            return ticket;
        }

        public async Task<bool> CheckTicketQty(Ticket ticket)
        {
            var eventData = await dbContext.Events.FirstOrDefaultAsync(e => e.Id == ticket.EventId);
            if(eventData.AvailableTickets - ticket.TicketQty >= 0)
            {
                return true;
            }

            return false;
        }

        public async Task<List<Ticket>> GetTicketsByEventId(Guid eventId)
        {
            var events = await dbContext.Tickets
                                        .Include("User")
                                        .Where(t => t.EventId == eventId)
                                        .ToListAsync();


            
            return events;
        }

        public async Task<List<Ticket>> GetTicketsByUserId(string userId)
        {
            var events = await dbContext.Tickets
                                    .Include("Event")
                                    .Where(t => t.UserId == userId)
                                    .ToListAsync();

            return events;
        }

        public async Task DeleteTicket(Guid ticketId)
        {
            await dbContext.Tickets
                .Where(t => t.Id == ticketId)
                .ExecuteDeleteAsync();

            await dbContext.SaveChangesAsync();
        }
    }
}
