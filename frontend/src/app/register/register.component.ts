import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import PasswordValidation from "./util/passwordValidation";


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

  firstNameControl = new FormControl('',[
    Validators.required,
    Validators.pattern(/^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/u)
  ])

  lastNameControl = new FormControl('',[
    Validators.required,
    Validators.pattern(/^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/u)
  ])


  usernameControl = new FormControl('',[
    Validators.required,
    Validators.pattern("^(?=.{4,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")
  ])

  addressControl = new FormControl('',[
    Validators.required,
    Validators.pattern("^([A-zæøåÆØÅ]{3,40}\\.?\\s)+([0-9]){1,5}\\w?(\\s.*)?$")
  ])

  postalCodeControl = new FormControl('',[
    Validators.required,
    Validators.pattern("/^[1-9][0-9]{3} ?(?!sa|sd|ss)[a-z]{2}$/i")
  ])

  registerFormGroup = this.formBuilder.group({
    email: this.emailControl,
    password: this.passwordControl,
    repeatPassword: this.repeatPasswordControl,
    firstName: this.firstNameControl,
    lastName: this.lastNameControl,
    username: this.usernameControl,
    address: this.addressControl,
    postalCode: this.postalCodeControl
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

  getFistNameErrorMessage() {
    if (this.firstNameControl.hasError('required')) {
      return 'Please enter a value'
    }
    return 'Please introduce a valid name'
  }

  getLastNameErrorMessage() {
    if (this.lastNameControl.hasError('required')) {
      return 'Please enter a value'
    }
    return 'Please introduce a valid last name'
  }

  getUsernameErrorMessage() {
    if (this.usernameControl.hasError('required')) {
      return 'Please enter a value'
    }
    return 'Please introduce a valid username'
  }

  getAddressErrorMessage() {
    if (this.addressControl.hasError('required')) {
      return 'Please enter a value'
    }
    return 'Please introduce a valid address'
  }

  getPostalCodeErrorMessage() {
    if (this.postalCodeControl.hasError('required')) {
      return 'Please enter a value'
    }
    return 'Please introduce a valid postal code'
  }
}
