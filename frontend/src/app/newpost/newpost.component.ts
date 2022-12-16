import {Component, OnInit} from '@angular/core';
import * as filestack from "filestack-js";
import {PickerResponse} from "filestack-js";
import {HttpService} from "../../services/http.service";
import {createPostDTO, registerDTO, UserProperties} from "../../entities/entities";
import {FormBuilder, FormControl, Validators} from "@angular/forms";
import {Router} from "@angular/router";

@Component({
  selector: 'app-newpost',
  templateUrl: './newpost.component.html',
  styleUrls: ['./newpost.component.css']
})
export class NewpostComponent implements OnInit {

  titleModel: string = "";
  descriptionModel: string = "";
  priceModel: number = 0;
  categoryModel: any;
  img: string = "";

  userProperties: UserProperties = {
    email: "",
    userName: "",
    roleId: 1
  }
  currentUser: registerDTO = {
    email: "",
    password: "",
    userName: "",
    address: "",
    firstName: "",
    lastName: "",
    phoneNumber: "",
    postalCode: "",
    img: "",
    roleId: 1
  };

  constructor(private http: HttpService,
              public formBuilder: FormBuilder,
              private router: Router) {

  }

  async ngOnInit(): Promise<void> {
    this.userProperties = this.http.getUserProperties(localStorage.getItem('sessionToken'));
    this.currentUser = await this.http.getUserByEmail(this.userProperties.email);
  }

  titleControl = new FormControl('', [
    Validators.required,
    Validators.pattern('^(.|\\s)*[a-zA-Z]+(.|\\s)*$')
  ]);

  descriptionControl = new FormControl('', [
    Validators.required,
    Validators.pattern('^(.|\\s)*[a-zA-Z]+(.|\\s)*$')
  ]);

  priceControl = new FormControl('', [
    Validators.required,
    Validators.pattern('[0-9]+[.[0-9]+]?')
  ]);

  newPostForm = this.formBuilder.group({
    title: this.titleControl,
    description: this.descriptionControl,
    price: this.priceControl
  })


  getTitleControlErrorMessage() {
    if (this.titleControl.hasError('required')) {
      return 'Please enter a value'
    }
    return "Please introduce a valid title for the post"
  }

  getDescriptionControlErrorMessage() {
    if (this.descriptionControl.hasError('required')) {
      return 'Please enter a value'
    }
    return "Please introduce a valid description for the post"
  }

  getPriceControlErrorMessage() {
    if (this.descriptionControl.hasError('required')) {
      return 'Please enter a value'
    }
    return "Please introduce a valid price for the post"
  }

  uploadImage() {
    const client = filestack.init('AzwS9T9PFRpW1fLDaalWgz');
    const options = {
      transformations: {
        crop: false,
        circle: true,
        rotate: true
      },
      maxFiles: 1,
      onUploadDone: (res: PickerResponse) => {
        let stringTmp = "";
        for (let i = 0; i < res.filesUploaded.length; i++) {
          stringTmp += res.filesUploaded[i].url + "#";
        }
        stringTmp = stringTmp.substring(0, stringTmp.length - 1);
        this.img = stringTmp;
      }
    }
    client.picker(options).open();
  }


  CreatePost() {
    let createdPost: createPostDTO = {
      Email: this.userProperties.email,
      Title: this.titleModel.trim(),
      Price: this.priceModel,
      Category: this.categoryModel,
      Description: this.descriptionModel.trim(),
      Img: this.img.trim()
    }
    this.http.CreatePost(createdPost)
      .then(()=>this.router.navigate(['mainPage']))
  }
}
