import { Component, OnInit } from '@angular/core';
import { PassengerService } from '../api/services';
import { FormBuilder, FormGroup } from '@angular/forms'

@Component({
  selector: 'app-register-passenger',
  standalone: false,
  templateUrl: './register-passenger.component.html',
  styleUrl: './register-passenger.component.css'
})
export class RegisterPassengerComponent implements OnInit {
  form!: FormGroup

  constructor(private passengerService: PassengerService,
    private fb: FormBuilder) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      email: [''],
      firstName: [''],
      lastName: [''],
      isFemale: [true]
    })
  }

  register() {
    console.log("Form Values:", this.form.value)
    this.passengerService.registerPassenger({body: this.form.value})
      .subscribe(_ => console.log("form posted to server"))
  }
}
