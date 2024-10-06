using Microsoft.EntityFrameworkCore;
using TicketBooking_WebAPI.Database;
using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Repositories
{
    public class SQLEventsRepository : IEventsRepository
    {
        private readonly BookerDbContext dbContext;

        public SQLEventsRepository(BookerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Event> AddEvent(Event newEvent)
        {
            await dbContext.Events.AddAsync(newEvent);
            await dbContext.SaveChangesAsync();

            return newEvent;
        }

        public async Task<List<Event>> GetAllEvents()
        {
            return await dbContext.Events.ToListAsync();
        }

        public async Task<List<Event>?> GetEventsByUserId(string userId)
        {
            var events = await dbContext.Events
                                         .Where(e => e.UserId == userId)
                                         .ToListAsync();
            return events;
        }
    }
}
