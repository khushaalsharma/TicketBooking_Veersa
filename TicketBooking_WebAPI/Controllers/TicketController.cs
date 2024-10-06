using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TicketBooking_WebAPI.Models.Domain;
using TicketBooking_WebAPI.Models.DTO;
using TicketBooking_WebAPI.Repositories;

namespace TicketBooking_WebAPI.Controllers
{
    [Route("api/tickets")]
    [ApiController]
    [Authorize]
    public class TicketController : Controller
    {
        private readonly IMapper mapper;
        private readonly ITicketRepository ticketRepository;

        public TicketController(IMapper mapper, ITicketRepository ticketRepository)
        {
            this.mapper = mapper;
            this.ticketRepository = ticketRepository;
        }

        [HttpPost]
        [Route("newTicket")]
        public async Task<IActionResult> AddTicket([FromBody] NewTicketDTO ticketDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cookieVal = Request.Cookies["BookerId"];
            var userId = Convert.FromBase64String(cookieVal);

            var ticketData = new Ticket
            {
                TicketQty = ticketDto.TicketQty,
                Amount = ticketDto.Amount,
                DateAndTime = ticketDto.DateAndTime,
                EventId = ticketDto.EventId,
                UserId = Encoding.UTF8.GetString(userId)
            };

            var check = await ticketRepository.CheckTicketQty(ticketData);
            if (check)
            {
                ticketData = await ticketRepository.BuyTicket(ticketData);
                return Ok(mapper.Map<TicketResponseDTO>(ticketData)); //Add a DTO
            }

            return BadRequest("Tickets not available");
        }

        [HttpGet]
        [Route("getUserTickets")]
        public async Task<IActionResult> GetTicketByUser()
        {
            var cookieVal = Request.Cookies["BookerId"];
            var userId = Convert.FromBase64String(cookieVal);

            var ticketsByUser = await ticketRepository.GetTicketsByUserId(Encoding.UTF8.GetString(userId));

            return Ok(mapper.Map<List<TicketResponseDTO>>(ticketsByUser));
        }

        [HttpGet]
        [Route("getTicketsByEvent/{id:Guid}")]
        public async Task<IActionResult> GetTicketsByEvent([FromRoute] Guid id)
        {
            var eventTickets = await ticketRepository.GetTicketsByEventId(id);

            return Ok(mapper.Map<List<EventTicketsDTO>>(eventTickets));
        }

        [HttpDelete]
        [Route("DeleteTicket/{id:Guid}")]
        public async Task<IActionResult> DeleteTicket([FromRoute] Guid id) //takes ticket ID
        {
            await ticketRepository.DeleteTicket(id);

            return Ok("Ticket Deleted");
        }
    }
}
