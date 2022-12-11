import { Component, OnInit } from '@angular/core';
<<<<<<< HEAD
=======
import {Category} from "../../entities/entities";
import {HttpService} from "../../services/http.service";
>>>>>>> Develop

@Component({
  selector: 'app-mainpage',
  templateUrl: './mainpage.component.html',
  styleUrls: ['./mainpage.component.css']
})
export class MainpageComponent implements OnInit {

  constructor(private http: HttpService) { }

  ngOnInit(): void {
<<<<<<< HEAD
=======
    console.log("Data ",this.http.result)
    //console.log(categories[0]);
>>>>>>> Develop
  }
}
