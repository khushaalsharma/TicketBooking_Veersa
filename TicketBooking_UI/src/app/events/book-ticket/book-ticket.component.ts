import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { TicketModel } from './ticket.model';
import { EventsService } from '../events.service';
import { FormsModule } from '@angular/forms';
import { Booking } from '../event/booking.model';
import { AuthHelperService } from '../../auth/authHelper.service';


@Component({
  selector: 'app-book-ticket',
  standalone: true,
  imports: [ FormsModule],
  providers: [EventsService],
  templateUrl: './book-ticket.component.html',
  styleUrl: './book-ticket.component.css'
})
export class BookTicketComponent {
  @Input({required: true}) eventData !: Booking;
  @Output() close = new EventEmitter<void>();
  enteredQty = 1;

  private eventService = inject(EventsService);
  private autHelp = inject(AuthHelperService);

  formatDateTime() {
    const now = new Date();

    const day = String(now.getDate()).padStart(2, '0');
    const month = String(now.getMonth() + 1).padStart(2, '0'); // Months are zero-indexed
    const year = now.getFullYear();

    let hours = now.getHours();
    const minutes = String(now.getMinutes()).padStart(2, '0');
    const ampm = hours >= 12 ? 'PM' : 'AM';

    hours = hours % 12;
    hours = hours ? hours : 12; // The hour '0' should be '12'
    const formattedHours = String(hours).padStart(2, '0');

    return `${day}-${month}-${year} ${formattedHours}:${minutes} ${ampm}`;
  }

  onSubmit(){
    if(this.autHelp.isLoggedIn() === false){
      alert("Please Sign In to book ticket");
      window.location.href = "/login";
    }
    else{
      const ticketData : TicketModel = {
        ticketQty: this.enteredQty,
        amount: this.enteredQty * this.eventData.ticketPrice,
        dateAndTime: this.formatDateTime(),
        eventId: this.eventData.id,
      };

      this.eventService.postData(ticketData)
          .subscribe((response) => {
            if(response){
              alert("Booking Successful!");
              this.close.emit();
            }

            console.log(response);
          }, (error) => {
            alert("You are trying to book more than available tickets");
            this.close.emit();
          });

      console.log(ticketData);
    }
  }
}
