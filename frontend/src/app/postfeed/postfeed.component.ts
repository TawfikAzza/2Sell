import {Component, OnInit} from '@angular/core';
import {HttpService} from "../../services/http.service";
import {postDTO} from "../../entities/entities";
import {Router} from "@angular/router";


@Component({
  selector: 'app-postfeed',
  templateUrl: './postfeed.component.html',
  styleUrls: ['./postfeed.component.css']
})
export class PostfeedComponent implements OnInit {

  constructor(public http: HttpService,
              private router: Router) {
    this.result = this.http.result;
  }

  empty: postDTO = {
    id:0,
    email:'string',
    userName:'string',
    price:1,
    title:'loading',
    description:'loading',
    authority:0,
    address:'loading',
    category:0,
    img:'loading'
}
  result: postDTO[] = []

  ngOnInit(): void {
    this.result.push(this.empty);
    this.result = this.http.result;
  }


  deletePost(id: number) {
    this.http.deletePost(id)
      .then(() => this.router.navigateByUrl('/', {skipLocationChange: true})
        .then(() => {
          this.router.navigate(['myposts']);
        }));
  }
}
