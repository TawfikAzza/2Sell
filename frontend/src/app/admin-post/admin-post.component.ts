import { Component, OnInit } from '@angular/core';
import {postDTO} from "../../entities/entities";
import {HttpService} from "../../services/http.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-admin-post',
  templateUrl: './admin-post.component.html',
  styleUrls: ['./admin-post.component.css']
})
export class AdminPostComponent implements OnInit {

  constructor(private http:HttpService,
              private router:Router) { }
  listPost:postDTO[]=[];

  ngOnInit(): void {
    this.getAllPost();
  }
  async getAllPost() {
    this.listPost = await this.http.getAllPost();
  }

  async deletePost(id: number) {
    console.log("post:",id);
    await this.http.deletePost(id).then(()=> this.router.navigateByUrl('/', { skipLocationChange: true })
      .then(() => {
        this.router.navigate(['admin-post']);
      }));;
  }
}
