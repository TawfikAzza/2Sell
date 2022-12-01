import {Injectable} from '@angular/core';
import axios from "axios";
import {environment} from "../environments/environment";
import {catchError} from "rxjs";
import {Router} from "@angular/router";
import {MatSnackBar} from "@angular/material/snack-bar";
import {loginDTO, registerDTO} from "../entities/entities";

export const customAxios = axios.create({
  baseURL: environment.baseUrl
})

//To ask: Why in the sample project given the axios is initialized like this
//and what are the advantages and disadvantages of doing it like this

@Injectable({
  providedIn: 'root'
})

export class HttpService {
  currentUserEmail: any;

  constructor(public matSnackbar: MatSnackBar,
              private router: Router
  ){

  }

  async register(param: registerDTO) {
    let petition = await customAxios.post('auth/register', param);
    if(petition.status == 201){
      localStorage.setItem('sessionToken', petition.data);
      this.matSnackbar.open("You have been registered", undefined, {duration: 3000})
    }
    else this.matSnackbar.open(petition.data, undefined, {duration:3000})
  }

  async login(param: loginDTO) {
    let petition = await customAxios.post('auth/login', param);
    if(petition.status == 201){
      localStorage.setItem('sessionToken', petition.data);
      this.matSnackbar.open('Welcome to 2Sell', undefined,{duration:3000})
    }
  }
}
