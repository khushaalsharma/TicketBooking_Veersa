using AutoMapper;
using TicketBookingWeb_API.Data.Models;
using TicketBookingWeb_API.Data.DTOs;

namespace TicketBookingWeb_API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Events, EventDataDTO>().ReverseMap();
            CreateMap<Tickets, TicketDTO>().ReverseMap();
            CreateMap<Events, AddEventDTO>().ReverseMap();
        }
    }
}
