import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { EventModel } from './event/event.model';
import { TicketModel } from './book-ticket/ticket.model';

@Injectable({
  providedIn: 'root'
})
export class EventsService {

  private eventsApiUrl = "https://localhost:7254/api/events/getAllEvents"; //will set in environment variables
  private ticketApiUrl = "https://localhost:7254/api/tickets/newTicket";
  private sessionToken = localStorage.getItem("Token");

  constructor(private http: HttpClient) { } //constructor for service uses httpClient to use the API

  //fetching all the events
  getEvent(): Observable<EventModel[]>{
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    })
    return this.http.get<EventModel[]>(this.eventsApiUrl, {
      headers,
      withCredentials: true
    }).pipe(
      catchError((error: any): Observable<any> => {
        console.error("Error fetching events in service:", error);
        throw error;
      })
    );
  }

  postData(ticketData: TicketModel): Observable<any>{
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.sessionToken}`
    });

    return this.http.post(this.ticketApiUrl, ticketData, {
      headers,
      withCredentials: true
    });
  }
}
