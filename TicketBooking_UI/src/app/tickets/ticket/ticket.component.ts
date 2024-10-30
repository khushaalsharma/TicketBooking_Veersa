import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TicketModel } from './ticket.model';
import { TicketService } from '../tickets.service';
import { CurrencyChange } from '../../utils/currency.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-ticket',
  standalone: true,
  imports: [CommonModule],
  providers: [TicketService, CurrencyChange],
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.css'] // Corrected property name
})
export class TicketComponent {
  cancelTicketPopUp: boolean = false; // State for the cancel confirmation dialog
  tickets: TicketModel[] = [];

  @Input({ required: true }) data!: TicketModel; // Input data from parent component
  @Output() ticketDel = new EventEmitter<void>(); // Output event for ticket deletion

  constructor(
    private ticketService: TicketService, // Service for ticket actions
    private currency: CurrencyChange // Service for currency formatting
  ) {}

  getAmount(amount: number | undefined) {
    const sessionToken = localStorage.getItem("Token");
    if (sessionToken) {
      const claims = atob(sessionToken.split('.')[1]);
      console.log(claims);
      var parsedToken = JSON.parse(claims);
      var currencyByUser = parsedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality"];

      if (currencyByUser === "INR") {
        return "₹" + amount;
      } else {
        return "$" + this.currency.inrToUsd(amount);
      }
    }
    return "₹" + amount;
  }

  cancelTicket() {
    // Retrieve tickets from local storage
    const data = localStorage.getItem("tickets");

    if (data) {
      // Parse stored ticket data if available
      this.tickets = JSON.parse(data);
    } else {
      alert("No tickets found to cancel.");
      return;
    }

    // Call the service to delete the ticket
    this.ticketService.cancelTicket(this.data.id).subscribe({
      next: response => {
        // Remove the canceled ticket from the tickets array
        this.tickets = this.tickets.filter(t => t.id !== this.data.id);
        localStorage.setItem("tickets", JSON.stringify(this.tickets));
        this.ticketDel.emit(); // Emit the deletion event
        alert("Booking deleted successfully.");
        this.cancelTicketPopUp = false;
      },
      error: err => {
        console.error("Error occurred while canceling the ticket:", err);
        alert("Failed to cancel the ticket. Please try again.");
      }
    });
  }

  // Toggle function to open or close the cancel dialog
  cancelCancel() {
    this.cancelTicketPopUp = !this.cancelTicketPopUp;
  }
}
