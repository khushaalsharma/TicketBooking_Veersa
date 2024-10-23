using Microsoft.AspNetCore.Mvc;
using TicketBooking_WebAPI.Models.Domain;
using TicketBooking_WebAPI.Models.DTO;
using TicketBooking_WebAPI.Repositories;

namespace TicketBooking_WebAPI.Controllers
{
    [ApiController]
    [Route("EventImage")]
    public class EventImageController : Controller
    {
        private readonly IEventImageRepository eventImageRepository;

        public EventImageController(IEventImageRepository eventImageRepository)
        {
            this.eventImageRepository = eventImageRepository;
        }

        [HttpPost]
        [Route("UploadEventBanner")]
        public async Task<IActionResult> Upload([FromForm] EventImageUploadDTO eventImageUploadDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bannerImageData = new EventImage
            {
                Name = eventImageUploadDTO.Name,
                File = eventImageUploadDTO.File,
                SizeInBytes = eventImageUploadDTO.File.Length,
                FileExtension = Path.GetExtension(eventImageUploadDTO.File.FileName),
                EventId = eventImageUploadDTO.EventId
            };

            bannerImageData = await eventImageRepository.AddEventImage(bannerImageData);

            return Ok(bannerImageData);
        }
    }
}
