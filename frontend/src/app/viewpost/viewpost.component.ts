import {Component, OnInit} from '@angular/core';
import {CommentDTO, postDTO, registerDTO, sessionToken, UserProperties} from "../../entities/entities";
import {HttpService} from "../../services/http.service";
import {FormBuilder} from "@angular/forms";
import {Router, UrlTree} from "@angular/router";
import {MatDialog} from "@angular/material/dialog";
import {NewCommentComponent} from "../new-comment/new-comment.component";
import jwtDecode from "jwt-decode";

@Component({
  selector: 'app-viewpost',
  templateUrl: './viewpost.component.html',
  styleUrls: ['./viewpost.component.css']
})
export class ViewpostComponent implements OnInit {
  currentPost: postDTO = {
    id: 0,
    description: "",
    title: "",
    price: 0,
    authority: 0,
    category: 1,
    email: "",
    userName: "",
    address: "",
    img: ""
  };
  listComments:CommentDTO[]=[];
  constructor(public http: HttpService,
              public formBuilder: FormBuilder,
              private router: Router,
              private dialogRef:MatDialog) {
  }
  userProperties:UserProperties={
    email:"",
    userName:"",
    roleId:1
  }
  async ngOnInit() {
    let urlParsed: UrlTree = this.router.parseUrl(this.router.url);
    console.log("params: ", urlParsed.queryParams['id']);
    this.getPost(urlParsed.queryParams['id']);
    this.getAllCommentFormPost(urlParsed.queryParams['id']);
    this.userProperties = this.http.getUserProperties(localStorage.getItem('sessionToken'));
  }


    async newComment(){
      //let user:registerDTO = await this.http.getUserByEmail(this.userProperties.email);
      this.dialogRef.open(NewCommentComponent,{
        data: {
          id:this.currentPost.id,
          title:this.currentPost.title,
          author:this.userProperties.userName,
          date:"test"
          //avatar:user.img
        }
      });
    }
    async getAllCommentFormPost(id:number) {
       this.listComments = await this.http.GetAllCommentFromPost(id);
      for (let i = 0; i<this.listComments.length;i++) {
        console.log("Data :",this.listComments[i].postId);
      }
       //console.log("length :",this.listComments[0].Content);
    }

    async getPost(id:number):Promise<postDTO>{
        let tmp = await this.http.getPost(id);
        this.currentPost= tmp;

       // this.listImg = this.currentPost.img.split("#");


        return this.currentPost;
    }




}
