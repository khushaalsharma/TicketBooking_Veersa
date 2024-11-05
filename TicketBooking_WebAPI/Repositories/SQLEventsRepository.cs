using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<List<Event>> GetAllEvents(string? searchVal, DateOnly? From, DateOnly? To, string? sortBy, bool isAsc = true)
        {
            var query = dbContext.Events.AsQueryable();

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(searchVal))
            {
                query = query.Where(e =>
                    e.EventName.ToLower().Contains(searchVal.ToLower()) ||
                    e.EventVenue.ToLower().Contains(searchVal.ToLower()) ||
                    e.Category.ToLower().Contains(searchVal.ToLower())
                );
            }

            // Apply date range filter if provided
            if (From != null && To != null)
            {
                query = query.Where(e => e.Date >= From && e.Date <= To);
            }

            // Apply sorting if provided
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("price", StringComparison.OrdinalIgnoreCase))
                {
                    query = isAsc ? query.OrderBy(e => e.MinTicketPrice) : query.OrderByDescending(e => e.MinTicketPrice);
                }
                else if (sortBy.Equals("date", StringComparison.OrdinalIgnoreCase))
                {
                    query = isAsc ? query.OrderBy(e => e.Date) : query.OrderByDescending(e => e.Date);
                }
            }

            return await query.ToListAsync();
        }
    }
}
