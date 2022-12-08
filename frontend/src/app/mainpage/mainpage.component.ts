import { Component, OnInit } from '@angular/core';
import {categories} from "../../entities/entities";

@Component({
  selector: 'app-mainpage',
  templateUrl: './mainpage.component.html',
  styleUrls: ['./mainpage.component.css']
})
export class MainpageComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    console.log(categories[0]);
  }

}
