import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {Router} from "@angular/router";
import {CommentDTO} from "../../entities/entities";
import {HttpService} from "../../services/http.service";


@Component({
  selector: 'app-new-comment',
  templateUrl: './new-comment.component.html',
  styleUrls: ['./new-comment.component.css']
})
export class NewCommentComponent implements OnInit {
  postId:number=0;
  title:string="";
  author:string="";
  dateComment:string="";
  avatar:string="";
  comment: string="";
  commentDto:CommentDTO={
    postId:0,
    content:"",
    author:"",
    date:"",
    avatar:""
  }
  constructor(@Inject(MAT_DIALOG_DATA) public data:any,
              private router:Router,
              private dialogRef: MatDialogRef<NewCommentComponent>,
              private http:HttpService) {
      this.postId= data.id;
      this.title=data.title;
      this.author=data.author;
      this.dateComment=data.date;
      this.avatar=data.avatar;
  }

  ngOnInit(): void {
  }

  onsubmit() {
    console.log("postId ",this.postId);
    console.log("comment ",this.comment);
    this.commentDto.postId=this.postId;
    this.commentDto.content=this.comment;
    this.commentDto.date=this.dateComment;
    this.commentDto.author=this.author;
    this.commentDto.avatar="";
    this.http.AddComment(this.commentDto);
    this.dialogRef.close();
  }

  onCancel() {
    this.dialogRef.close();
  }
}
