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
              public http:HttpService) { }
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
    //this.userProperties = this.http.getUserProperties(localStorage.getItem('sessionToken'));

    let tokenString:string|null = localStorage.getItem('sessionToken');

    if(tokenString!="" && tokenString!=null) {
      console.log("in if");
      this.userProperties = this.http.getUserProperties(localStorage.getItem('sessionToken'));
      this.currentUser = await this.http.getUserByEmail(this.userProperties.email);
    }
    console.log("UserName: ",this.currentUser.userName);
    console.log("Role:",this.currentUser.roleId);
    this.http.logged=false;

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
    localStorage.clear();
    this.http.logged=false;
    //localStorage.setItem('sessionToken',"");
  }
}
