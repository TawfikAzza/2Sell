import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import PasswordValidation from "./passwordValidation";


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

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
    email: this.emailControl,
    password: this.passwordControl,
    repeatPassword: this.repeatPasswordControl
  },{
    validators: [PasswordValidation.match('password', 'repeatPassword')]
  })

  print() {
    if(this.registerFormGroup.valid)
    console.log(this.registerFormGroup.value)
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
