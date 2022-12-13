import { Component, OnInit } from '@angular/core';
import {postDTO} from "../../entities/entities";
import {HttpService} from "../../services/http.service";
import {FormBuilder} from "@angular/forms";
import {Router} from "@angular/router";

@Component({
  selector: 'app-viewpost',
  templateUrl: './viewpost.component.html',
  styleUrls: ['./viewpost.component.css']
})
export class ViewpostComponent implements OnInit {
  currentPost: postDTO = {
    id:0,
    description:"",
    title:"",
    price:0,
    authority:0,
    category:0,
    email:"",
    userName:"",
    address:"",
    img:""
  };
  listImg:string[]=[];
  constructor(public http: HttpService,
              public formBuilder:FormBuilder,
              private router:Router) { }

  ngOnInit() {
    this.getPost(3);
  }

  async getPost(id:number):Promise<postDTO>{
      let tmp = await this.http.getPost(id);
      this.currentPost= tmp;
      this.listImg = this.currentPost.img.split("#");


      return this.currentPost;
  }
}
