import {Component, OnInit} from '@angular/core';
import {CommentDTO, postDTO} from "../../entities/entities";
import {HttpService} from "../../services/http.service";
import {FormBuilder} from "@angular/forms";
import {Router, UrlTree} from "@angular/router";
import {MatDialog} from "@angular/material/dialog";
import {NewCommentComponent} from "../new-comment/new-comment.component";

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

  async ngOnInit() {
    let urlParsed: UrlTree = this.router.parseUrl(this.router.url);
    console.log("params: ", urlParsed.queryParams['id']);
    this.getPost(urlParsed.queryParams['id']);
    this.getAllCommentFormPost(urlParsed.queryParams['id']);
  }


    newComment(){
      this.dialogRef.open(NewCommentComponent,{
        data: {
          id:this.currentPost.id,
          title:this.currentPost.title
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
