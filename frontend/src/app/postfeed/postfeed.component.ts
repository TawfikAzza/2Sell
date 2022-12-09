import { Component, OnInit } from '@angular/core';
import {HttpService} from "../../services/http.service";
import {postDTO} from "../../entities/entities";

@Component({
  selector: 'app-postfeed',
  templateUrl: './postfeed.component.html',
  styleUrls: ['./postfeed.component.css']
})
export class PostfeedComponent implements OnInit {

  constructor(private http:HttpService) { }
  result:postDTO[]=[]
  ngOnInit(): void {
    this.result = this.http.result;
    console.log("test",this.result);
  }

  log() {
    for (let i = 0; i < this.result.length; i++){
      console.log("for loop postfeed: "+this.result[i].title);
    }
  }
}
