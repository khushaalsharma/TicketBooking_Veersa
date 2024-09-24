import { Component, inject, Input } from '@angular/core';
import { TicketModel } from './ticket.model';
import { EventsService } from '../events.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { Booking } from '../event/booking.model';


@Component({
  selector: 'app-book-ticket',
  standalone: true,
  imports: [HttpClientModule, FormsModule],
  providers: [EventsService],
  templateUrl: './book-ticket.component.html',
  styleUrl: './book-ticket.component.css'
})
export class BookTicketComponent {
  @Input({required: true}) eventData !: Booking;

  enteredName = "";
  enteredPhone = "" ;
  enteredEmail = "";
  enteredQty = 1;

  private eventService = inject(EventsService);

  onSubmit(){
    if(this.enteredPhone.length != 10){
      alert("Phone Number must be atleast 10 digit");
    }
    else{
      const ticketData : TicketModel = {
        userName: this.enteredName,
        userPhone: this.enteredPhone,
        userEmail: this.enteredEmail,
        ticketQty: this.enteredQty,
        eventsId: this.eventData.id
      };

      this.eventService.postData(ticketData)
          .subscribe(response => {
            if(response){
              alert("Booking Successful!");
            }

            console.log(response);
          });

      //console.log(ticketData);
    }
  }
}
