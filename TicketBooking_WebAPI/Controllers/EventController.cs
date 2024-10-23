using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TicketBooking_WebAPI.Models.Domain;
using TicketBooking_WebAPI.Models.DTO;
using TicketBooking_WebAPI.Repositories;

namespace TicketBooking_WebAPI.Controllers
{
    [Route("api/events")]
    [ApiController]
    //[Authorize]
    public class EventController : Controller
    {
        private readonly IEventsRepository eventsRepository;
        private readonly IMapper mapper;
        private readonly ITicketTypeRepository ticketTypeRepository;
        private readonly IEventImageRepository eventImageRepository;

        public EventController(IEventsRepository eventsRepository, IMapper mapper, ITicketTypeRepository ticketTypeRepository, IEventImageRepository eventImageRepository)
        {
            this.eventsRepository = eventsRepository;
            this.mapper = mapper;
            this.ticketTypeRepository = ticketTypeRepository;
            this.eventImageRepository = eventImageRepository;
        }

        [HttpPost]
        [Route("newEvent")]
        public async Task<IActionResult> AddEvent([FromBody] AddEventDTO eventDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cookieVal = Request.Cookies["BookerId"];
            var managerId = Convert.FromBase64String(cookieVal);

            var eventData = new Event
            {
                EventName = eventDto.EventName,
                EventVenue = eventDto.EventVenue,
                EventDescription = eventDto.EventDescription,
                Date = DateOnly.Parse(eventDto.Date),
                Time = TimeOnly.Parse(eventDto.Time),
                Category = eventDto.Category,
                UserId = Encoding.UTF8.GetString(managerId),
                MinTicketPrice = eventDto.MinTicketPrice,
            };

            eventData = await eventsRepository.AddEvent(eventData);

            foreach(var ticketTypeData in eventDto.TicketTypesArr)
            {
                var TicketTypeDetails = new TicketType
                {
                    TicketCategory = ticketTypeData.TicketCategory,
                    TotalTickets = ticketTypeData.TotalTickets,
                    AvailableTickets = ticketTypeData.AvailableTickets,
                    Price = ticketTypeData.Price,
                    EventId = eventData.Id
                };

                TicketTypeDetails = await ticketTypeRepository.addTicketType(TicketTypeDetails);
            }

            return Ok(mapper.Map<AddEventDTO>(eventData));
        }

        [HttpGet]
        [Route("getAllEvents")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllEvents(
            [FromQuery] string? name,
            [FromQuery] string? venue,
            [FromQuery] string? category,
            [FromQuery] string? From,
            [FromQuery] string? To,
            [FromQuery] string? sortBy,
            [FromQuery] bool isAsc = true)
        {
            // Parse the From and To dates only if they are not null
            DateOnly? fromDate = null;
            DateOnly? toDate = null;

            if (!string.IsNullOrEmpty(From))
            {
                fromDate = DateOnly.Parse(From);
            }

            if (!string.IsNullOrEmpty(To))
            {
                toDate = DateOnly.Parse(To);
            }

            // Call the repository method and pass nullable DateOnly values
            var eventsData = await eventsRepository.GetAllEvents(name, venue, category, fromDate, toDate, sortBy, isAsc);

            var allEvents = new List<EventDTO> { };

            foreach(var availableEvent in eventsData)
            {
                var eventDto = mapper.Map<EventDTO>(availableEvent);
                var eventImages = await eventImageRepository.GetAllEventImages(eventDto.Id);
                var images = new List<ImageUrlDTO> { };
                foreach(var image in eventImages)
                {
                    images.Add(mapper.Map<ImageUrlDTO>(image));
                }

                eventDto.bannerImg = images;
                allEvents.Add(eventDto);
            }

            return Ok(allEvents);
        }


        [HttpGet]
        [Route("getEventById/{Id:Guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventById([FromRoute] Guid Id)
        {
            var eventDetail = await eventsRepository.GetEventById(Id);
            var ticketTypeDetails = await ticketTypeRepository.GetTicketTypesForEvents(Id);
            var bannerImgDetails = await eventImageRepository.GetAllEventImages(Id);

            var ticketTypes = new List<TicketTypeRespDTO> { };

            foreach(var ticketType in ticketTypeDetails)
            {   
                ticketTypes.Add(mapper.Map<TicketTypeRespDTO>(ticketType));
            }

            var imageUrls = new List<ImageUrlDTO> { };

            foreach(var bannerImg in bannerImgDetails)
            {
                imageUrls.Add(mapper.Map<ImageUrlDTO>(bannerImg));
            }

            var eventData = new EventResponseDTO
            {
                EventName = eventDetail.EventName,
                EventVenue = eventDetail.EventVenue,
                EventDescription = eventDetail.EventDescription,
                Category = eventDetail.Category,
                Time = eventDetail.Time,
                Date = eventDetail.Date,
                UserId = eventDetail.UserId,
                User = mapper.Map<UserData>(eventDetail.User),
                MinTicketPrice = eventDetail.MinTicketPrice,
                TicketTypes = ticketTypes,
                bannerImg = imageUrls,
            };

            return Ok(eventData);
        }

        //User ID is extracted from the cookie - BookerId
        //[HttpGet]
        //[Route("getEventByUser")]
        //public async Task<IActionResult> GetEventsByUser()
        //{
        //    var decodedVal = Request.Cookies["BookerId"];
        //    var managerId = Convert.FromBase64String(decodedVal);

        //    var eventsByManager = await eventsRepository.GetEventsByUserId(Encoding.UTF8.GetString(managerId));

        //    return Ok(mapper.Map<List<EventDTO>>(eventsByManager));
        //}
    }
}
