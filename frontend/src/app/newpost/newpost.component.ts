import { Component, OnInit } from '@angular/core';
import * as filestack from "filestack-js";
import {PickerResponse} from "filestack-js";
import {HttpService} from "../../services/http.service";
import {createPostDTO} from "../../entities/entities";

@Component({
  selector: 'app-newpost',
  templateUrl: './newpost.component.html',
  styleUrls: ['./newpost.component.css']
})
export class NewpostComponent implements OnInit {
  title: string="";
  description: string="";
  price: number=0;
  category: number=0;
  img:string="";
  constructor(private http:HttpService) { }

  ngOnInit(): void {
  }

  uploadImages() {
   const client = filestack.init('AzwS9T9PFRpW1fLDaalWgz');
    const options = {
      transformations: {
        crop: false,
        circle: true,
        rotate: true
      },
      maxFiles:1,
      onUploadDone: (res:PickerResponse)=> {
        let stringTmp="";
        for(let i=0; i<res.filesUploaded.length;i++) {
          stringTmp+=res.filesUploaded[i].url+"#";
        }
        stringTmp = stringTmp.substring(0,stringTmp.length-1);
        this.img = stringTmp;
      }
    }
    client.picker(options).open();
   // this.img="https://cdn.filestackcontent.com/n9OIUGXmSfvxSHsy2znG#https://cdn.filestackcontent.com/RcsePpDTeiwJ84g3gXzA#https://cdn.filestackcontent.com/ZlfajHvsRqu5G53JAZfO";
  }


  CreatePost() {
    let createdPost:createPostDTO={
      Email:"user",
      Title:this.title.trim(),
      Price:this.price,
      Category:this.category,
      Description:this.description.trim(),
      Img:this.img.trim()
    }
    this.http.CreatePost(createdPost);
  }
}
