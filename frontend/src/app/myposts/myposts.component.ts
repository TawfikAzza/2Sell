import { Component, OnInit } from '@angular/core';
import {HttpService} from "../../services/http.service";
import {UserProperties} from "../../entities/entities";

@Component({
  selector: 'app-myposts',
  templateUrl: './myposts.component.html',
  styleUrls: ['./myposts.component.css']
})
export class MypostsComponent implements OnInit {

  constructor(private http:HttpService) { }
  userProperties:UserProperties={
    email:"",
    userName:"",
    roleId:1
  }
  ngOnInit(): void {
    this.userProperties = this.http.getUserProperties(localStorage.getItem('sessionToken'));
    console.log("userName: ",this.userProperties.userName);
    this.http.getMyPost(this.userProperties.userName);
  }


}
