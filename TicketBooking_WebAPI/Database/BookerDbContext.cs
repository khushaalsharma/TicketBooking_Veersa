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
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<EventImage> EventImages { get; set; }
        public DbSet<Payments> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Ticket to Event relationship with Restrict
            builder.Entity<Ticket>()
                   .HasOne(t => t.Event)
                   .WithMany()
                   .HasForeignKey(t => t.EventId)
                   .OnDelete(DeleteBehavior.Restrict);  // Restrict deletion of related event

            // Ticket to User relationship with Restrict
            builder.Entity<Ticket>()
                   .HasOne(t => t.User)
                   .WithMany()
                   .HasForeignKey(t => t.UserId)
                   .OnDelete(DeleteBehavior.Restrict);  // Restrict deletion of related user

            // Ticket to TicketType relationship with Restrict
            builder.Entity<Ticket>()
                   .HasOne(t => t.TicketType)
                   .WithMany()
                   .HasForeignKey(t => t.TicketTypeId)
                   .OnDelete(DeleteBehavior.Restrict);  // Explicitly set Restrict to avoid NO ACTION issue

            // TicketType to Event relationship
            builder.Entity<TicketType>()
                   .HasOne(tt => tt.Event)
                   .WithMany()
                   .HasForeignKey(tt => tt.EventId)
                   .OnDelete(DeleteBehavior.Cascade);  // Optional: Cascade deletion of related event in TicketType

            // EventImage to Event relationship
            builder.Entity<EventImage>();  // Optional: Cascade deletion of related event in EventImage

            // Payments to User relationship
            builder.Entity<Payments>()
                   .HasOne(p => p.User)
                   .WithMany()
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Restrict);  // Restrict deletion of related user
        }
    }
}
