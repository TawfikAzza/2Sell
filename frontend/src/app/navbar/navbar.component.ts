import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {registerDTO, UserProperties} from "../../entities/entities";
import {HttpService} from "../../services/http.service";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private router:Router,
              private http:HttpService) { }
  currentUser:registerDTO = {
    email:"",
    password:"",
    userName:"",
    address:"",
    firstName:"",
    lastName:"",
    phoneNumber:"",
    postalCode:"",
    img:"",
    roleId:1
  };
  userProperties:UserProperties={
    email:"",
    userName:"",
    roleId:1
  }
  async ngOnInit(): Promise<void> {
    this.userProperties = this.http.getUserProperties(localStorage.getItem('sessionToken'));
    this.userProperties = this.http.getUserProperties(localStorage.getItem('sessionToken'));
    this.currentUser = await this.http.getUserByEmail(this.userProperties.email);
  }

  async navigateMainPage() {
    await this.router.navigate(['mainPage'])
  }

  async navigateUserProfile() {
    await this.router.navigate(['myProfile'])
  }

  async navigateUserPosts() {
    await this.router.navigate(['myPosts'])
  }

  logout() {
    localStorage.setItem('sessionToken',"");
  }
}
