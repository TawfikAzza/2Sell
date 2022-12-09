import { Component, OnInit } from '@angular/core';
import {HttpService} from "../../services/http.service";
import {postDTO} from "../../entities/entities";
import {CategoriesbarComponent} from "../categoriesbar/categoriesbar.component";
import {C} from "@angular/cdk/keycodes";
import {MainpageComponent} from "../mainpage/mainpage.component";

@Component({
  selector: 'app-postfeed',
  templateUrl: './postfeed.component.html',
  styleUrls: ['./postfeed.component.css']
})
export class PostfeedComponent implements OnInit {

  constructor(public http:HttpService) {
    this.result = this.http.result;
  }
  result:postDTO[]=[]

  ngOnInit(): void {

    console.log("test",this.result);
    this.result = this.http.result;

  }
  getInstance(category:CategoriesbarComponent){
    //this.categoryComponent=category;
    //return this;
  }
  log() {
    for (let i = 0; i < this.result.length; i++){
      console.log("for loop postfeed: "+this.result[i].title);
    }
  }
}
