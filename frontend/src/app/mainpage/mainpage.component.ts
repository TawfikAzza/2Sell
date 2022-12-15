import { Component, OnInit } from '@angular/core';
import {Category} from "../../entities/entities";
import {HttpService} from "../../services/http.service";

@Component({
  selector: 'app-mainpage',
  templateUrl: './mainpage.component.html',
  styleUrls: ['./mainpage.component.css']
})
export class MainpageComponent implements OnInit {

  constructor(private http: HttpService) { }

  ngOnInit(): void {
    console.log("Data ",this.http.result);
    this.http.getAllPost();
  }
}
