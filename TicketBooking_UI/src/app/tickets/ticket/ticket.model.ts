export interface TicketModel{
    "id": string;
    "ticketQty": number;
    "amount": number;
    "dateAndTime": string;
    "eventId": string;
    "event": {
        "eventName": string;
        "eventDescription": string;
        "date": string;
        "time": string;
        "eventVenue": string;
    };
    "ticketTypeId": string;
    "ticketType": {
        "ticketCategory": string;
    };
    "paymentsId": string;
}