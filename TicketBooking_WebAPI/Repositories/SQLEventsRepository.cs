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

        public async Task<Event> GetEventById(Guid eventId)
        {
            var eventData = await dbContext.Events
                                           .Include(e => e.User) // Use lambda expression for Include
                                           .FirstOrDefaultAsync(e => e.Id == eventId); // Materialize the query
            return eventData;
        }


        public async Task<List<Event>?> GetEventsByUserId(string userId)
        {
            var events = await dbContext.Events
                                         .Where(e => e.UserId == userId)
                                         .ToListAsync();
            return events;
        }

        public async Task<List<Event>> GetAllEvents(string? name, string? venue, string? category, DateOnly? From, DateOnly? To, string? sortBy, bool isAsc = true)
        {
            var events = await dbContext.Events.ToListAsync();

            if (!string.IsNullOrEmpty(name))
            {
                events = events.Where(e => e.EventName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if(!string.IsNullOrEmpty(venue))
            {
                events = events.Where(e => e.EventVenue.Contains(venue, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(category))
            {
                events = events.Where(e => e.Category.Contains(category, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if(From != null && To != null)
            {
                events = events.Where(e => e.Date >= From && e.Date <= To).ToList();
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                if(sortBy.Equals("price", StringComparison.OrdinalIgnoreCase))
                {
                    events = isAsc ? events.OrderBy(e => e.MinTicketPrice).ToList() : events.OrderByDescending(e => e.MinTicketPrice).ToList();
                }
                if (sortBy.Equals("date", StringComparison.OrdinalIgnoreCase))
                {
                    events = isAsc ? events.OrderBy(e => e.Date).ToList() : events.OrderByDescending(e => e.Date).ToList();
                }
            }

            return events;
        }
    }
}
