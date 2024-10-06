using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Database
{
    public class BookerDbContext : IdentityDbContext<User>
    {
        public BookerDbContext(DbContextOptions<BookerDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Event to Ticket relationship, disabling cascading delete
            builder.Entity<Ticket>()
                   .HasOne(t => t.Event)
                   .WithMany()
                   .HasForeignKey(t => t.EventId)
                   .OnDelete(DeleteBehavior.Restrict);  // Change to No Action or Restrict

            // User to Ticket relationship, keeping cascading delete
            builder.Entity<Ticket>()
                   .HasOne(t => t.User)
                   .WithMany()
                   .HasForeignKey(t => t.UserId)
                   .OnDelete(DeleteBehavior.Cascade);  // Optional: can still be set to Cascade
        }

    }
}
