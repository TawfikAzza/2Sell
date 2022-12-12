import { Component, OnInit } from '@angular/core';
import {HttpService} from "../../services/http.service";
import {postDTO} from "../../entities/entities";

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
  userName: any;
  authority: any;


  constructor(public http:HttpService) {

  }



  ngOnInit(): void {

  }

}
