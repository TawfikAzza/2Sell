import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, Validators} from "@angular/forms";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  email: any;
  password: any;
  repeatPassword: any;
  passwordsMatch: any;

  constructor(public formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
  }

  emailControl = new FormControl('', [
    Validators.required,
    Validators.email
  ]);

  passwordControl = new FormControl('', [
    Validators.required,
    Validators.pattern('^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$')
    //At least 8 characters, one letter and one number
  ]);

  repeatPasswordControl = new FormControl('', [
    Validators.required,
  ]);

  registerFormGroup = this.formBuilder.group({
    email: this.emailControl.value,
    password: this.passwordControl.value,
    repeatPassword: this.repeatPasswordControl.value
  })

  print() {
    console.log(this.email, this.password, this.password)
  }

  getEmailErrorMessage() {
    if (this.emailControl.hasError('required')) {
      return 'Please enter a value'
    }
    return 'Enter a valid email'
  }

  getPasswordErrorMessage() {
    if (this.passwordControl.hasError('required')) {
      return 'Please enter a value'
    }
    return 'Password must contain at least 8 characters, one letter and one number'
  }

  getRepeatPasswordErrorMessage() {
    if (this.repeatPasswordControl.hasError('required')) {
      return 'Please enter a value'
    }
    return "Passwords doesn't match"
  }
}
