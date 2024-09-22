using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TicketBookingWeb_API.Data.DTOs;
using TicketBookingWeb_API.Data.Models;
using TicketBookingWeb_API.DatabaseContext;
using TicketBookingWeb_API.Repositories;

namespace TicketBookingWeb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : Controller
    {
        private readonly TicketBookingDbContext dbContext;
        private readonly ITicketRepository ticketRepository;
        private readonly IMapper mapper;

        public TicketController(TicketBookingDbContext dbContext, ITicketRepository ticketRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.ticketRepository = ticketRepository;
            this.mapper = mapper;
        }

        //Booking a Ticket API
        [HttpPost]
        public async Task<IActionResult> BookTicket([FromBody] TicketDTO ticketDto)
        {
            if (!ModelState.IsValid) //checking if the user follows the data constraints
            {
                return BadRequest(ModelState);
            }

            var newTicket = mapper.Map<Tickets>(ticketDto);

            var ticket = await ticketRepository.BookTicketAsync(newTicket);

            return Ok(mapper.Map<TicketDTO>(ticket));
        }
    }
}
