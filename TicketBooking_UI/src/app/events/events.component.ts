import { Component, OnInit } from '@angular/core';
import { EventComponent } from "./event/event.component";
import { EventsService } from './events.service';
import { HttpClientModule } from '@angular/common/http';
import type { EventModel } from './event/event.model';
import { Booking } from './event/booking.model';
import { BookTicketComponent } from './book-ticket/book-ticket.component';

@Component({
  selector: 'app-events',
  standalone: true,
  imports: [EventComponent, HttpClientModule, BookTicketComponent],
  providers: [EventsService],
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent {
  public eventsData : EventModel[] = [];

  bookTicket : boolean = false;
  bookTicketData : Booking = {
    id: "",
    eventName: "",
    ticketPrice: 0
  };

  constructor(private eventService: EventsService){} //constructor to use the service object

  getEventsFromStorage(){
    const events = localStorage.getItem("events");
    if(events){
      this.eventsData = JSON.parse(events);
    }
  }

  ngOnInit(): void { //A lifecycle hook that is called after Angular has initialized all data-bound properties of a directive. Define an ngOnInit() method to handle any additional initialization tasks.
    this.eventService.getEvent().subscribe(
      (data: EventModel[]) => {
        console.log(data);
        //this.eventsData = data; 
        localStorage.setItem("events", JSON.stringify(data));
        this.getEventsFromStorage();
      },
      (error) => {
        console.error("Error fetching events", error);
      }
    );
  }

  bookingTicket(booking: Booking){
    if(booking){
      this.bookTicket = true;
      this.bookTicketData = booking;
    }
    else{
      this.bookTicket = false;
    }
  }

  closeBook(){
    this.bookTicket = false;
  }
}
