import { Component, OnInit } from '@angular/core';
import {HttpService} from "../../services/http.service";
import {postDTO} from "../../entities/entities";
import {MatCardModule} from '@angular/material/card';
import {Router} from "@angular/router";


@Component({
  selector: 'app-postfeed',
  templateUrl: './postfeed.component.html',
  styleUrls: ['./postfeed.component.css']
})
export class PostfeedComponent implements OnInit {

  constructor(public http:HttpService,
              private router:Router) {
    this.result = this.http.result;
  }

  result:postDTO[]=[]

  ngOnInit(): void {
    this.result = this.http.result;
  }


  deletePost(id: number) {
    this.http.deletePost(id)
      .then(()=> this.router.navigateByUrl('/', { skipLocationChange: true })
        .then(() => {
          this.router.navigate(['myposts']);
        }));
  }
}
