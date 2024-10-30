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

        public async Task BuyTicket(Ticket ticket)
        {
            var ticketTypeData = await dbContext.TicketTypes.FindAsync(ticket.TicketTypeId);

            ticketTypeData.AvailableTickets = ticketTypeData.AvailableTickets - ticket.TicketQty; //updating available tickets
            
            await dbContext.Tickets.AddAsync(ticket);
            dbContext.TicketTypes.Update(ticketTypeData);
            await dbContext.SaveChangesAsync();
            return;
        }

        public async Task<bool> CheckTicketQty(Ticket ticket)
        {
            Console.WriteLine(ticket);
            var ticketTypeData = await dbContext.TicketTypes.FirstOrDefaultAsync(t => t.Id == ticket.TicketTypeId);
            if (ticketTypeData.AvailableTickets - ticket.TicketQty >= 0)
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
                                    .Include("TicketType")
                                    .Where(t => t.UserId == userId)
                                    .ToListAsync();

            return events;
        }

        public async Task<bool> DeleteTicket(Guid ticketId, string userId)
        {
            var ticketData = await dbContext.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);

            if(ticketData.UserId == userId)
            {
                var ticketTypeData = await dbContext.TicketTypes.FindAsync(ticketData.TicketTypeId);

                ticketTypeData.AvailableTickets = ticketTypeData.AvailableTickets + ticketData.TicketQty;

                dbContext.TicketTypes.Update(ticketTypeData);

                await dbContext.Tickets
                    .Where(t => t.Id == ticketId)
                    .ExecuteDeleteAsync();

                await dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
