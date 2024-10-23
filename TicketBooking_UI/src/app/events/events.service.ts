import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { from, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { EventModel } from './event/event.model';
import { TicketModel } from './book-ticket/ticket.model';

@Injectable({
  providedIn: 'root'
})
export class EventsService {

  private eventsApiUrl = "https://localhost:7254/api/events/getAllEvents"; //will set in environment variables
  private ticketApiUrl = "https://localhost:7254/api/tickets/newTicket";
  private eventByIdUrl = "https://localhost:7254/api/events/getEventById";
  private sessionToken = localStorage.getItem("Token");

  constructor(private http: HttpClient) { } //constructor for service uses httpClient to use the API

  //fetching all the events
  getEvent(): Observable<EventModel[]>{
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    return this.http.get<EventModel[]>(this.eventsApiUrl, {
      headers,
      withCredentials: true,
    }).pipe(
      catchError((error: any): Observable<any> => {
        console.error("Error fetching events in service:", error);
        throw error;
      })
    );
  }

  //get events woth filtering and searching
  getEventByFilter(name: string, venue: string, category: string, From: string, To: string, sortBy: string, isAsc: boolean): Observable<EventModel[]>{
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    const queryParams = new HttpParams()
                    .set("name", name)
                    .set("venue", venue)
                    .set("category", category)
                    .set("From", From)
                    .set("To", To)
                    .set("sortBy", sortBy)
                    .set("isAsc", isAsc);

    return this.http.get<EventModel[]>(this.eventsApiUrl, {
      headers,
      withCredentials: true,
      params: queryParams
    }).pipe(
      catchError((error: any): Observable<any> => {
        console.error("Error fetching events in service:", error);
        throw error;
      })
    );
  }

  getEventById(id: string): Observable<any>{
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

   

    return this.http.get(this.eventByIdUrl + `/${id}`, {
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
