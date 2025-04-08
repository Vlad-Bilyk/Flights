import { Component, OnInit } from '@angular/core';
import { PassengerService } from '../api/services';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
      email: [
        '',
        Validators.compose([
          Validators.required,
          Validators.email,
          Validators.minLength(3),
          Validators.maxLength(100),
        ]),
      ],
      firstName: [
        '',
        Validators.compose([
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(35),
        ]),
      ],
      lastName: [
        '',
        Validators.compose([
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(35),
        ]),
      ],
      isFemale: [true, Validators.required],
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
    if (this.form.invalid) return;

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
