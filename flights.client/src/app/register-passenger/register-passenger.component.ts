import { Component, OnInit } from '@angular/core';
import { PassengerService } from '../api/services';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-passenger',
  standalone: false,
  templateUrl: './register-passenger.component.html',
  styleUrl: './register-passenger.component.css',
})
export class RegisterPassengerComponent implements OnInit {
  form!: FormGroup;

  constructor(
    private passengerService: PassengerService,
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      email: [''],
      firstName: [''],
      lastName: [''],
      isFemale: [true],
    });
  }

  checkPassenger(): void {
    const params = { email: this.form.get('email')?.value };

    this.passengerService.findPassenger(params).subscribe({
      next: this.login,
      error: (e) => {
        if (e.status != 404) console.error(e);
      },
    });
  }

  register() {
    console.log('Form Values:', this.form.value);
    this.passengerService
      .registerPassenger({ body: this.form.value })
      .subscribe({
        next: this.login,
        error: console.error,
      });
  }

  private login = () => {
    this.authService.loginUser({ email: this.form.get('email')?.value });
    this.router.navigate(['/search-flights']);
  };
}
