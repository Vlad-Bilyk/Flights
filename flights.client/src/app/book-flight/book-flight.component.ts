import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FlightService } from './../api/services/flight.service';
import { BookDto, FlightRm } from '../api/models';
import { AuthService } from '../auth/auth.service';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-book-flight',
  standalone: false,
  templateUrl: './book-flight.component.html',
  styleUrl: './book-flight.component.css',
})
export class BookFlightComponent implements OnInit {
  form!: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private flightService: FlightService,
    private authService: AuthService,
    private fb: FormBuilder
  ) {}

  flightId: string = 'not loaded';
  flight: FlightRm = {};

  ngOnInit(): void {
    if (!this.authService.currentUser)
      this.router.navigate(['/register-passenger']);

    this.route.paramMap.subscribe((p) => this.findFlight(p.get('flightId')));

    this.form = this.fb.group({
      number: [
        1,
        Validators.compose([
          Validators.required,
          Validators.min(1),
          Validators.max(254),
        ]),
      ],
    });
  }

  private readonly findFlight = (flightId: string | null) => {
    this.flightId = flightId ?? 'not passed';

    this.flightService.findFlight({ id: this.flightId }).subscribe({
      next: (flight) => (this.flight = flight),
      error: this.handleError,
    });
  };

  book() {
    if (this.form.invalid) {
      return;
    }

    console.log(
      `Booking ${this.form.get('number')?.value} passengers for the flight: ${
        this.flight.id
      }`
    );

    const booking: BookDto = {
      flightId: this.flightId,
      passengerEmail: this.authService.currentUser?.email,
      numberOfSeats: this.form.get('number')?.value,
    };

    this.flightService.bookFlight({ body: booking }).subscribe({
      next: (_) => this.router.navigate(['/my-booking']),
      error: this.handleError,
    });
  }

  private readonly handleError = (err: any) => {
    if (err.status == 404) {
      alert('Flight not found');
      this.router.navigate(['/search-flights']);
    }

    if (err.status == 409) {
      console.log('err: ' + err);
      alert(JSON.parse(err.error).message);
    }

    console.log('Response Error. Status: ', err.status);
    console.log('Response error. Status Text: ', err.statusText);
    console.log(err);
  };

  get number() {
    return this.form.get('number') as FormControl;
  }
}
