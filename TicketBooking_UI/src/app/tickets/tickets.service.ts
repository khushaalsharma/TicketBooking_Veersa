import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { catchError, Observable } from "rxjs";
import { TicketModel } from "./ticket/ticket.model";

@Injectable({
    providedIn: "root"
})
export class TicketService{
    private getTicketApi = "https://localhost:7254/api/tickets/getUserTickets";
    private delTicketApi = "https://localhost:7254/api/tickets/DeleteTicket";

    private sessionToken = localStorage.getItem("Token");

    constructor(private http: HttpClient){}

    getTickets() : Observable<TicketModel>{
        const headers = new HttpHeaders({
            'Authorization': `Bearer ${this.sessionToken}`,
            'Content-Type': 'application/json'
        });
        return this.http.get<TicketModel>(this.getTicketApi, {
            headers,
            withCredentials: true
        }).pipe(
            catchError((error: any) : Observable<any> => {
                console.log("Error fetching tickets:", error);
                throw error;
            })
        )
    }

    cancelTicket(id: string): Observable<any>{
        const headers = new HttpHeaders({
            'Authorization': `Bearer ${this.sessionToken}`,
            'Content-Type': 'application/json'
        });

        return this.http.delete(this.delTicketApi + `/${id}`, {
            headers,
            withCredentials: true
        });
    }
}