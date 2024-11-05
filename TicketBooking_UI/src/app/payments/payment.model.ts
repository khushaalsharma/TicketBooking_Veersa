export interface PrePaymentModel{
    "amount": number;
    "tickets": TicketForSale[]
};

interface TicketForSale{
    "eventName": string;
    "eventId": string;
    "ticketTypeName": string;
    "ticketTypeId": string;
    "qty": number;
    "amount": number;
}

export interface PaymentModel{
    "paymentMethod": string;
    "methodDetail": string;
    "boughtAt": string;
    "amount": number;
    "couponCode": string | null;
    "newTickets": PaymentTicket[]
}

export interface PaymentTicket{
    "ticketQty": number;
    "amount": number;
    "eventId": string;
    "ticketTypeId": string;
}