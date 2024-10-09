import { Component } from '@angular/core';
import { EventsComponent } from "../events/events.component";
import { BookTicketComponent } from '../events/book-ticket/book-ticket.component';
import { NavbarComponent } from "../navbar/navbar.component";

@Component({
  selector: 'app-homepage',
  standalone: true,
  imports: [EventsComponent, BookTicketComponent, NavbarComponent],
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.css'
})
export class HomepageComponent {

}
