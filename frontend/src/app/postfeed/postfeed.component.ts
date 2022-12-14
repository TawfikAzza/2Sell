import { Component, OnInit } from '@angular/core';
import {HttpService} from "../../services/http.service";
import {postDTO} from "../../entities/entities";
import {MatCardModule} from '@angular/material/card';


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
    for(let i=0;i<this.http.result.length;i++) {
      console.log("Result :",this.result[i].img);
    }
  }



}
