export interface CartModel{
    "id": string;
    "price": number;
    "quantity": number;
    "amount": number;
    "eventId": string;
    "ticketTypeId": string;
    "event": EventModel;
    "ticketType": TicketTypeModel
}

interface EventModel{
    "eventName": string;
    "eventVenue": string;
    "date": string;
    "time": string;
}

interface TicketTypeModel{
    "ticketCategory": string;
}