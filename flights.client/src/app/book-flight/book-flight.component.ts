import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FlightService } from './../api/services/flight.service';
import { FlightRm } from '../api/models';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-book-flight',
  standalone: false,
  templateUrl: './book-flight.component.html',
  styleUrl: './book-flight.component.css',
})
export class BookFlightComponent implements OnInit {
  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly flightService: FlightService,
    private readonly authService: AuthService
  ) {}

  flightId: string = 'not loaded';
  flight: FlightRm = {};

  ngOnInit(): void {
    if (!this.authService.currentUser)
      this.router.navigate(['/register-passenger']);

    this.route.paramMap.subscribe((p) => this.findFlight(p.get('flightId')));
  }

  private readonly findFlight = (flightId: string | null) => {
    this.flightId = flightId ?? 'not passed';

    this.flightService.findFlight({ id: this.flightId }).subscribe({
      next: (flight) => (this.flight = flight),
      error: this.handleError,
    });
  };

  private readonly handleError = (err: any) => {
    if (err.status == 404) {
      alert('Flight not found');
      this.router.navigate(['/search-flights']);
    }

    console.log('Response Error. Status: ', err.status);
    console.log('Response error. Status Text: ', err.statusText);
    console.log(err);
  };
}
