import { Component } from '@angular/core';
import { EventsComponent } from "../events/events.component";

import { NavbarComponent } from "../navbar/navbar.component";

@Component({
  selector: 'app-homepage',
  standalone: true,
  imports: [EventsComponent, NavbarComponent],
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.css'
})
export class HomepageComponent {

}
