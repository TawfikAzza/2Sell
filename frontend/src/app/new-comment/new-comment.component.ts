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
  comment: string="";
  commentDto:CommentDTO={
    postId:0,
    content:""
  }
  constructor(@Inject(MAT_DIALOG_DATA) public data:any,
              private router:Router,
              private dialogRef: MatDialogRef<NewCommentComponent>,
              private http:HttpService) {
      this.postId= data.id;
      this.title=data.title;
  }

  ngOnInit(): void {
  }

  onsubmit() {
    console.log("postId ",this.postId);
    console.log("comment ",this.comment);
    this.commentDto.postId=this.postId;
    this.commentDto.content=this.comment;
    this.http.AddComment(this.commentDto);
    this.dialogRef.close();
  }
}
