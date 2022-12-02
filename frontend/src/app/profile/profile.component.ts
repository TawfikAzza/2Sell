import { Component, OnInit } from '@angular/core';
import {registerDTO} from "../../entities/entities";
import {HttpService} from "../../services/http.service";
import {FormBuilder} from "@angular/forms";
import {Router} from "@angular/router";
import {C} from "@angular/cdk/keycodes";
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  email: any;
  userName: any;
  firstName: any;
  lastName: any;
  currentUser?:registerDTO;
  constructor(public http: HttpService,
              public formBuilder:FormBuilder,
              private router:Router) { }

  async ngOnInit(): Promise<void> {
    this.currentUser = await this.getUserFromEmail("test");
    this.email = this.currentUser.email;

    // this.email = this.currentUser.then(cu=> cu.email);
  }
 async getUserFromEmail(email:string): Promise<registerDTO> {
    const result = await this.http.getUserByEmail(email);
   console.log("result:",result);
    return result;
 }
}
