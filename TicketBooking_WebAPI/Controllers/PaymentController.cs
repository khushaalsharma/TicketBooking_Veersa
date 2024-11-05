using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TicketBooking_WebAPI.Models.Domain;
using TicketBooking_WebAPI.Models.DTO;
using TicketBooking_WebAPI.Repositories;

namespace TicketBooking_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly ITicketRepository ticketRepository;
        private readonly IPaymentRepository paymentRepository;
        private readonly IMapper mapper;
        private readonly ICartRepository cartRepository;

        public PaymentController(ITicketRepository ticketRepository, IPaymentRepository paymentRepository, IMapper mapper, ICartRepository cartRepository)
        {
            this.ticketRepository = ticketRepository;
            this.paymentRepository = paymentRepository;
            this.mapper = mapper;
            this.cartRepository = cartRepository;
        }

        [HttpPost]
        [Route("NewTickets")]
        public async Task<IActionResult> buyTickets([FromBody] BuyTicketDTO ticketDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "error with model", ModelState });
            }

            var cookieVal = Request.Cookies["BookerId"];
            var decodeVal = Convert.FromBase64String(cookieVal);
            var userId = Encoding.UTF8.GetString(decodeVal);

            //update the coupon count
            await paymentRepository.UpdateCoupon(ticketDto.CouponCode);

            //initiate payment
            var paymentData = new Payments
            {
                Amount = ticketDto.Amount,
                PaymentMethod = ticketDto.PaymentMethod,
                MethodDetail = ticketDto.MethodDetail,
                BoughtAt = ticketDto.BoughtAt,
                UserId = userId,
            };

            paymentData = await paymentRepository.MakePayment(paymentData);

            //add each ticket in the database
            foreach(var ticket in ticketDto.NewTickets)
            {
                var ticketData = new Ticket
                {
                    TicketQty = ticket.TicketQty,
                    Amount = ticket.Amount,
                    DateAndTime = ticketDto.BoughtAt.ToString(),
                    EventId = ticket.EventId,
                    TicketTypeId = ticket.TicketTypeId,
                    UserId = userId,
                    PaymentsId = paymentData.Id,
                };

                var booked = await ticketRepository.CheckTicketQty(ticketData);
                if (booked)
                {
                    await ticketRepository.BuyTicket(ticketData);
                }
            }

            //empty user's cart
            await cartRepository.EmptyCart(userId);

            var resp = new PaymentRespDTOcs
            {
                Id = paymentData.Id,
                BoughtAt = paymentData.BoughtAt,
                Amount = paymentData.Amount
            };

            return Ok(resp);
        }

    }
}
