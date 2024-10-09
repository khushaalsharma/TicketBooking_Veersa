export interface TicketModel{
    "id": string;
    "ticketQty": number,
    "amount": number,
    "dateAndTime": string,
    "eventId": string,
    "event": {
        "eventName": string,
        "eventDescription": string,
        "dateAndTime": string,
        "eventVenue": string,
    }
}