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
    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventsRepository eventsRepository;
        private readonly IMapper mapper;

        public EventController(IEventsRepository eventsRepository, IMapper mapper)
        {
            this.eventsRepository = eventsRepository;
            this.mapper = mapper;
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
                DateAndTime = eventDto.DateAndTime,
                TicketPrice = eventDto.TicketPrice,
                TotalTickets = eventDto.TotalTickets,
                AvailableTickets = eventDto.AvailableTickets,
                UserId = Encoding.UTF8.GetString(managerId)
            };

            eventData = await eventsRepository.AddEvent(eventData);

            return Ok(mapper.Map<AddEventDTO>(eventData));
        }

        [HttpGet]
        [Route("getAllEvents")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllEvents()
        {
            var eventsData = await eventsRepository.GetAllEvents();

            return Ok(mapper.Map<List<EventDTO>>(eventsData));
        }

        //User ID is extracted from the cookie - BookerId
        [HttpGet]
        [Route("getEventByUser")]
        public async Task<IActionResult> GetEventsByUser()
        {
            var decodedVal = Request.Cookies["BookerId"];
            var managerId = Convert.FromBase64String(decodedVal);

            var eventsByManager = await eventsRepository.GetEventsByUserId(Encoding.UTF8.GetString(managerId));

            return Ok(mapper.Map<List<EventDTO>>(eventsByManager));
        }
    }
}
