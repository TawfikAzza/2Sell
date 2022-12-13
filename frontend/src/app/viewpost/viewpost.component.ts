import {Component, OnInit} from '@angular/core';
import {postDTO} from "../../entities/entities";
import {HttpService} from "../../services/http.service";
import {FormBuilder} from "@angular/forms";
import {Router, UrlTree} from "@angular/router";

@Component({
  selector: 'app-viewpost',
  templateUrl: './viewpost.component.html',
  styleUrls: ['./viewpost.component.css']
})
export class ViewpostComponent implements OnInit {
  currentPost: postDTO = {
    id: 2,
    description: "this is a good description hehehehethis is a good description hehehehethis is a good description hehehehethis is a good description hehehehethis is a good description hehehehethis is a good description hehehehethis is a good description hehehehethis is a good description hehehehethis is a good description hehehehethis is a good description hehehehethis is a good description hehehehethis is a good description hehehehethis is a good description hehehehethis is a good description hehehehethis is a good description hehehehethis is a good description hehehehethis is a good description hehehehethis is a good description hehehehethis is a good description hehehehe",
    title: "Such a good bike this one",
    price: 22.1,
    authority: 3,
    category: 1,
    email: "emailoftheguy@gmail.com",
    userName: "username",
    address: "some adddresss 23",
    img: "https://cdn.filestackcontent.com/g9ZuxMVpQl2UjD4mYWPQ"
  };

  constructor(public http: HttpService,
              public formBuilder: FormBuilder,
              private router: Router) {
  }

  ngOnInit() {
      let urlParsed:UrlTree = this.router.parseUrl(this.router.url);
      console.log("params: ",urlParsed.queryParams['id']);
      this.getPost(urlParsed.queryParams['id']);
  }



    async getPost(id:number):Promise<postDTO>{
        let tmp = await this.http.getPost(id);
        this.currentPost= tmp;

       // this.listImg = this.currentPost.img.split("#");


        return this.currentPost;
    }




}
