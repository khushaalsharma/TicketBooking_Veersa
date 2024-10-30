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

        [HttpGet]
        [Route("getUserTickets")]
        public async Task<IActionResult> GetTicketByUser()
        {
            var cookieVal = Request.Cookies["BookerId"];
            var userId = Convert.FromBase64String(cookieVal);

            var ticketsByUser = await ticketRepository.GetTicketsByUserId(Encoding.UTF8.GetString(userId));

            return Ok(mapper.Map<List<TicketResponseDTO>>(ticketsByUser));
            //return Ok(ticketsByUser);
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
            var cookieVal = Request.Cookies["BookerId"];
            var userId = Convert.FromBase64String(cookieVal);

            var deleteOk = await ticketRepository.DeleteTicket(id, Encoding.UTF8.GetString(userId));

            if (deleteOk)
            {
                return Ok(new { message = "Ticket Deleted" });
            }

            return BadRequest(new { message = "Only Ticket user can Delete the ticket" });
        }
    }
}
