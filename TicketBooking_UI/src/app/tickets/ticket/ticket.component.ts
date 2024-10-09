import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TicketModel } from './ticket.model';
import { TicketService } from '../tickets.service';

@Component({
  selector: 'app-ticket',
  standalone: true,
  imports: [],
  providers: [TicketService],
  templateUrl: './ticket.component.html',
  styleUrl: './ticket.component.css'
})
export class TicketComponent {
  cancelTicketPopUp : boolean = false;
  tickets : TicketModel[] = [];

  @Input({required: true}) data !: TicketModel;
  @Output() ticketDel = new EventEmitter<void>();
  constructor(private ticketservice : TicketService){}

  cancelTicket() {
  // Retrieve tickets from local storage
  const data = localStorage.getItem("tickets");

  // Parse the tickets if data is found in local storage
  if (data) {
    this.tickets = JSON.parse(data);
  } else {
    alert("No tickets found to cancel.");
    return;
  }

  // Call the ticket service to cancel the ticket
  this.ticketservice.cancelTicket(this.data.id)
    .subscribe({
      next: response => {
        // Filter out the canceled ticket
        this.tickets = this.tickets.filter(t => t.id !== this.data.id);
        localStorage.setItem("tickets", JSON.stringify(this.tickets));
        this.ticketDel.emit();
        alert("Booking deleted");
        this.cancelTicketPopUp = false;
      },
      error: err => {
        console.error("Error occurred while canceling the ticket:", err);
        alert("Failed to cancel the ticket. Please try again.");
      }
    });
}


  cancelCancel(){
    this.cancelTicketPopUp = !this.cancelTicketPopUp;
  }
}
