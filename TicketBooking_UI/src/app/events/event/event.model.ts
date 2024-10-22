export interface EventModel{
    id: string;
    eventName: string;
    eventVenue: string;
    date: string,
    time: string,
    minTicketPrice: number;
    eventDescription: string;
    category: string;
    bannerImg: {url : string}[];
}