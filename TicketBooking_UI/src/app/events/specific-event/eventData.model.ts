export interface SpecificEventModel{
    id: string;
    eventName: string;
    eventDescription: string;
    eventVenue: string;
    minTicketPrice: number;
    category: string;
    date: string;
    time: string;
    bannerImg: {url: string}[]
    user: OrgData;
    ticketTypes: TicketTypes[]
}

export interface OrgData{
    email: string;
    phoneNumber: string;
    name: string;
}

export interface TicketTypes{
    id: string;
    ticketCategory: string;
    totalTickets: number;
    availableTickets: number;
    price: number;
}