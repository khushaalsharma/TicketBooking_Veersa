using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketBooking_WebAPI.Database;
using TicketBooking_WebAPI.Models.Domain;
using TicketBooking_WebAPI.Models.DTO;

namespace TicketBooking_WebAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CouponController : Controller
    {
        private readonly BookerDbContext dbContext;
        private readonly IMapper mapper;

        public CouponController(BookerDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("/newCoupon")]
        public async Task<IActionResult> newCoupon([FromBody] CouponDTO couponDTO) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var couponData = mapper.Map<Coupon>(couponDTO);

            await dbContext.Coupons.AddAsync(couponData);
            await dbContext.SaveChangesAsync();

            return Ok(couponData);
        }

        [HttpGet]
        [Route("/checkCoupon/{code}")]
        public async Task<IActionResult> checkCoupon([FromRoute] string code)
        {
            var couponData = await dbContext.Coupons.FirstOrDefaultAsync(c => c.Code == code);

            if (couponData != null)
            {
                return Ok(couponData);
            }

            return BadRequest(new { message = "Coupon not found" });
        }
    }
}
