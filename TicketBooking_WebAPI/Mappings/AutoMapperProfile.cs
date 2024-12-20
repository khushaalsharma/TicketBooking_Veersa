﻿using AutoMapper;
using TicketBooking_WebAPI.Models.Domain;
using TicketBooking_WebAPI.Models.DTO;

namespace TicketBooking_WebAPI.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, RegisterDTO>().ReverseMap();
            CreateMap<User, LoginRequestDTO>().ReverseMap();
            CreateMap<Event, AddEventDTO>().ReverseMap();
            CreateMap<Ticket, UserTicketDTO>().ReverseMap();
            CreateMap<Ticket, EventTicketsDTO>().ReverseMap();
            CreateMap<Event, EventDTO>().ReverseMap();
            CreateMap<Ticket, NewTicketDTO>().ReverseMap();
            CreateMap<Ticket, TicketResponseDTO>().ReverseMap();
            CreateMap<User, UserData>().ReverseMap();
            CreateMap<Event, EventResponseDTO>().ReverseMap();
            CreateMap<TicketType, TicketTypeRespDTO>().ReverseMap();
            CreateMap<TicketType, AddTicketTypeDTO>().ReverseMap();
            CreateMap<EventImage, ImageUrlDTO>().ReverseMap();
            CreateMap<CartItem, CartItemRespDTO>().ReverseMap();
            CreateMap<Cart, CartDTO>().ReverseMap();
        }
    }
}
