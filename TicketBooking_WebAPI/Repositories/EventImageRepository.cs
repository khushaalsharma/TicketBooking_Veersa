using Microsoft.EntityFrameworkCore;
using TicketBooking_WebAPI.Database;
using TicketBooking_WebAPI.Models.Domain;

namespace TicketBooking_WebAPI.Repositories
{
    public class EventImageRepository : IEventImageRepository
    {
        private readonly BookerDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public EventImageRepository(BookerDbContext dbContext, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<EventImage> AddEventImage(EventImage image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images/Events", $"{image.Name}{image.FileExtension}");

            //Upload image to the local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //we need to add the image path according to this localhost for it we use HttpContextAccessor
            //this is similar to something like https://localhost:1234/Images/image.png
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/Events/{image.Name}{image.FileExtension}";

            image.Url = urlFilePath;

            //add to image table
            await dbContext.EventImages.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }

        public async Task<List<EventImage>> GetAllEventImages(Guid eventId)
        {
            var imageList = await dbContext.EventImages
                                           .Where(ei => ei.EventId == eventId)
                                           .ToListAsync();

            return imageList;
        }
    }
}
