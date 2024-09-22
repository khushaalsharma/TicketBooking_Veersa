using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketBookingWeb_API.Data.DTOs;
using TicketBookingWeb_API.Data.Models;
using TicketBookingWeb_API.DatabaseContext;
using TicketBookingWeb_API.Repositories;

namespace TicketBookingWeb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : Controller
    {
        private readonly TicketBookingDbContext dbContext;
        private readonly IEventsRepository eventsRepository;
        private readonly IMapper mapper;

        //constructir for controller
        public EventsController(TicketBookingDbContext dbContext, IEventsRepository eventsRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.eventsRepository = eventsRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await eventsRepository.GetAllAsync(); //receiving the list of all the events from the database

            return Ok(mapper.Map<List<EventDataDTO>>(events));
        }

        //Add event API
        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody] AddEventDTO eventDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventModel = mapper.Map<Events>(eventDto);
            eventModel = await eventsRepository.AddEventAsync(eventModel);

            return Ok(mapper.Map<EventDataDTO>(eventModel));
        }
    }
}
