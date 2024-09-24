import { Component } from '@angular/core';
import { EventsComponent } from "../events/events.component";
import { BookTicketComponent } from '../events/book-ticket/book-ticket.component';

@Component({
  selector: 'app-homepage',
  standalone: true,
  imports: [EventsComponent, BookTicketComponent],
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.css'
})
export class HomepageComponent {

}
