using Microsoft.EntityFrameworkCore;
using TicketBooking_WebAPI.Database;
using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Repositories
{
    public class SQLTicketTypeRepository : ITicketTypeRepository
    {
        private readonly BookerDbContext dbContext;

        //constructor
        public SQLTicketTypeRepository(BookerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //adding a tikcet type to database
        public async Task<TicketType> addTicketType(TicketType ticketType)
        {
            await dbContext.TicketTypes.AddAsync(ticketType);
            await dbContext.SaveChangesAsync();

            return ticketType;
        }

        //fetch ticket types for the event
        public Task<List<TicketType>> GetTicketTypesForEvents(Guid eventId)
        {
            var ticketTypes = dbContext.TicketTypes
                                       .Where(t => t.EventId == eventId)
                                       .ToListAsync();

            return ticketTypes;
        }
    }
}
