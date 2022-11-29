import { Component, OnInit } from '@angular/core';
import PasswordValidation from "../register/util/passwordValidation";
import {FormBuilder, FormControl, Validators} from "@angular/forms";
import {HttpService} from "../../services/http.service";
import {loginDTO} from "../../entities/entities";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  emailModel: any;
  passwordModel: any;

  constructor(public http: HttpService,
              public formBuilder:FormBuilder) { }

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

  loginForm = this.formBuilder.group({
    email: this.emailControl,
    password: this.passwordControl
  });

  getEmailErrorMessage() {
    if (this.emailControl.hasError('required')) {
      return 'Please enter a value'
    }
    return 'Enter a valid email'
  }

  getPasswordErrorMessage() {
    return 'Please enter a value'
  }

  async login() {
    let dto: loginDTO = {
      email: this.emailModel,
      password: this.passwordModel
    }

    await this.http.login(dto);
  }
}
