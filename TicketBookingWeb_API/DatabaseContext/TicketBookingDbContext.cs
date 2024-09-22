using Microsoft.EntityFrameworkCore;
using TicketBookingWeb_API.Data.Models;

namespace TicketBookingWeb_API.DatabaseContext
{
    public class TicketBookingDbContext : DbContext
    {
        //constructor DbContext
        public TicketBookingDbContext(DbContextOptions<TicketBookingDbContext> dbContextOptions) : base(dbContextOptions)
        {}

        //attributes for this DbContext
        public DbSet<Events> Events { get; set; }
        public DbSet<Tickets> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //data seeding for Events 
            //dummy data 

            var events = new List<Events>()
            {
                new Events()
                {
                    Id = Guid.Parse("bd759470-fed0-4b95-b0f5-602959f29682"),
                    EventName = "Tech Conference 2024",
                    EventVenue = "Bharat Mandapam, Pragati Maidan, Delhi",
                    DateAndTime = "23-09-2024 12:00 PM",
                    EventDescription = "A leading conference on emerging technologies including AI, Blockchain, and Quantum Computing.",
                    TicketPrice = 2000
                },
                new Events()
                {
                    Id = Guid.Parse("dbe68caf-5b9f-4af9-971a-93f65f15b221"),
                    EventName = "Dilluminati",
                    EventVenue = "JLN Stadium, Delhi",
                    DateAndTime = "26-10-2024 7:00 PM",
                    EventDescription = "A night of live music performances by Diljit Dosanjh",
                    TicketPrice = 13000
                },
                new Events()
                {
                    Id = Guid.Parse("362676cd-ff9e-4274-b275-32cce4258ef8"),
                    EventName = "Food Mela",
                    EventVenue = "Noida Expo Centre, Noida",
                    DateAndTime = "30-09-2024 12:00 PM",
                    EventDescription = "Explore gourmet food and exquisite wines from around the world.",
                    TicketPrice = 500
                },
                new Events()
                {
                    Id = Guid.Parse("f5f64c28-64d0-4775-b244-a708ea53b728"),
                    EventName = "Comedy Night with Zakir Khan",
                    EventVenue = "Comedy Club, Gurgaon",
                    DateAndTime = "25-09-2024 6:00 PM",
                    EventDescription = "Laughout Loud with Zakir Khan's crazy anecdotes",
                    TicketPrice = 1500
                },
                new Events()
                {
                    Id = Guid.Parse("6434842b-01c6-482e-aefd-937205a787b3"),
                    EventName = "TechCrunch Disrupt",
                    EventVenue = "Manekshaw Centre, Delhi",
                    DateAndTime = "01-10-2024 12:00 PM",
                    EventDescription = "Celebrate the innovative tech startups at TechCrunch Disrupt",
                    TicketPrice = 2000
                }
            };

            modelBuilder.Entity<Events>().HasData(events);
        }
    }
}
