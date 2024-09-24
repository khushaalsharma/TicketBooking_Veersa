import { Component, EventEmitter, Input, Output } from '@angular/core';
import type { EventModel } from './event.model';
import { Booking } from './booking.model';

@Component({
  selector: 'app-event',
  standalone: true,
  imports: [],
  templateUrl: './event.component.html',
  styleUrl: './event.component.css'
})
export class EventComponent {
  @Input({required: true}) eventData !: EventModel;
  @Output() bookEvent : EventEmitter<Booking> = new EventEmitter<Booking>();

  onBook(){
    const eventDetails : Booking = {
      id: this.eventData.id,
      eventName: this.eventData.eventName,
      ticketPrice: this.eventData.ticketPrice
    };

    this.bookEvent.emit(eventDetails);
  }
}
