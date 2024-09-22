using Microsoft.EntityFrameworkCore;
using TicketBookingWeb_API.Data.Models;
using TicketBookingWeb_API.DatabaseContext;

namespace TicketBookingWeb_API.Repositories
{
    public class SQLEventsRepository : IEventsRepository
    {
        private readonly TicketBookingDbContext dbContext;

        public SQLEventsRepository(TicketBookingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Events>> GetAllAsync()
        {
            return await dbContext.Events.ToListAsync();
        }

        public async Task<Events> AddEventAsync(Events program)
        {
            await dbContext.Events.AddAsync(program);
            await dbContext.SaveChangesAsync();
            return program;
        }
    }
}
