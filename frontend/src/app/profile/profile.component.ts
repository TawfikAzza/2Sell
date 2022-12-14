import {Component, Input, OnInit} from '@angular/core';
import {registerDTO, UserProperties} from "../../entities/entities";
import {HttpService} from "../../services/http.service";
import {FormBuilder, FormControl, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import PasswordValidation from "../register/util/passwordValidation";
import {HttpClient, HttpEventType} from "@angular/common/http";
import {finalize, Subscription} from "rxjs";
import {customAxios} from "../../services/http.service";
import {environment} from "../../environments/environment";
import * as filestack from 'filestack-js';
import {PickerFileMetadata, PickerResponse} from "filestack-js";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

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

  emptyString = "";

  pageMode = 'view';
  emailModel: any;
  userNameModel: any;
  firstNameModel: any;
  lastNameModel: any;
  passwordModel: any;
  addressModel: any;
  postalCodeModel: any;
  phoneNumberModel: any;
  imgModel: any;


  constructor(public http: HttpService,
              public formBuilder: FormBuilder,
              private router: Router,
              private httpC: HttpClient) {
  }

  userProperties: UserProperties = {
    email: "",
    userName: "",
    roleId: 1
  }

  async ngOnInit() {
    let token = localStorage.getItem('sessionToken');
    this.userProperties = this.http.getUserProperties(localStorage.getItem('sessionToken'));
    this.userProperties = this.http.getUserProperties(localStorage.getItem('sessionToken'));
    console.log("email: ", this.userProperties.email);
    this.currentUser = await this.http.getUserByEmail(this.userProperties.email);//this.getUserFromEmail(this.userProperties.email);
    // this.email = this.currentUser.then(cu=> cu.email);
    console.log(this.currentUser.img)
  }

  async getUserFromEmail(email: string) {
    this.currentUser = await this.http.getUserByEmail(email);
    console.log("current user : " + this.currentUser.email);
  }


  usernameControl = new FormControl('', [
    Validators.required,
    Validators.pattern("^(?=.{4,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")
  ]);

  passwordControl = new FormControl('', [
    Validators.required,
    Validators.pattern('^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$')
    //At least 8 characters, one letter and one number
  ]);

  repeatPasswordControl = new FormControl('', [
    Validators.required,
  ]);

  firstNameControl = new FormControl('', [
    Validators.required,
    Validators.pattern(/^[a-zA-Z??????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????? ,.'-]+$/u)
  ])

  lastNameControl = new FormControl('', [
    Validators.required,
    Validators.pattern(/^[a-zA-Z??????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????? ,.'-]+$/u)
  ])


  addressControl = new FormControl('', [
    Validators.required,
    Validators.pattern("^([A-z????????????]{3,40}\\.?\\s)+([0-9]){1,5}\\w?(\\s.*)?$")
  ])

  postalCodeControl = new FormControl('', [
    Validators.required,
    //Validators.pattern("/^[1-9][0-9]{3} ?(?!sa|sd|ss)[a-z]{2}$/i")
  ])

  phoneNumberControl = new FormControl('', [
    Validators.required,
    //Validators.pattern("/^[1-9][0-9]{3} ?(?!sa|sd|ss)[a-z]{2}$/i")
  ])
  modifyFormGroup = this.formBuilder.group({
    password: this.passwordControl,
    repeatPassword: this.repeatPasswordControl,
    firstName: this.firstNameControl,
    lastName: this.lastNameControl,
    address: this.addressControl,
    postalCode: this.postalCodeControl,
    phoneNumber: this.phoneNumberControl
  }, {
    validators: [PasswordValidation.match('password', 'repeatPassword')]
  })

  @Input()
  requiredFileType: string = "";
  fileName = '';
  uploadProgress: number = 0;
  uploadSub: Subscription = new Subscription;

  onFileSelected(event: any) {
    const file: File = event.target.files[0];

    if (file) {
      this.fileName = file.name;
      const formData = new FormData();
      formData.append("thumbnail", file);
      formData.append("userEmail", this.currentUser.email);
      const upload$ = this.httpC.post(environment.baseUrl + "/WebShop/UploadFileProfile/", formData, {
        reportProgress: true,
        observe: 'events'
      })
        .pipe(
          finalize(() => this.reset())
        );

      this.uploadSub = upload$.subscribe(event => {
        if (event.type == HttpEventType.UploadProgress) {
          // @ts-ignore
          this.uploadProgress = Math.round(100 * (event.loaded / event.total));
          console.log("upload progress:" + this.uploadProgress);
        }
      });
    }
  }

  cancelUpload() {
    this.uploadSub.unsubscribe();
    this.reset();
  }

  reset() {
    this.uploadProgress = 0;
    this.uploadSub = new Subscription;
  }

  modeChange(mode: string) {
    this.pageMode = mode;
    if (mode == 'modify') {
      this.userNameModel = this.currentUser.userName;
      this.firstNameModel = this.currentUser.firstName;
      this.lastNameModel = this.currentUser.lastName;
      this.addressModel = this.currentUser.address;
      this.postalCodeModel = this.currentUser.postalCode;
      this.phoneNumberModel = this.currentUser.phoneNumber;
      this.imgModel = this.currentUser.img;
    }
    console.log("pageMode:", this.pageMode);
  }

  async modifyUser() {
    let dto: registerDTO = {
      email: this.currentUser.email,
      password: this.passwordModel.trim(),
      firstName: this.firstNameModel.trim(),
      lastName: this.lastNameModel.trim(),
      userName: this.userNameModel.trim(),
      address: this.addressModel.trim(),
      postalCode: this.postalCodeModel.trim(),
      phoneNumber: this.phoneNumberModel.trim(),
      img: this.currentUser.img,
      roleId: this.currentUser.roleId
    }
    console.log(dto);

    await this.http.updateProfile(dto)
      .then(() => this.pageMode = 'view')
      .then(() => this.router.navigate(['profile']));
    this.currentUser = await this.http.getUserByEmail(dto.email);

  }

  getFistNameErrorMessage() {
    if (this.firstNameControl.hasError('required')) {
      return 'Please enter a value';
    }
    return 'First name can not be empty';
  }

  getLastNameErrorMessage() {
    if (this.lastNameControl.hasError('required')) {
      return 'Please enter a value';
    }
    return 'Last name cannot be empty';
  }

  getPasswordErrorMessage() {
    if (this.passwordControl.hasError('required')) {
      return 'Please enter a value';
    }
    return 'Enter a valid password';
  }

  getRepeatPasswordErrorMessage() {
    if (this.repeatPasswordControl.hasError('required')) {
      return 'Please enter a value';
    }
    return "Passwords doesn't match";
  }


  getAddressErrorMessage() {
    if (this.addressControl.hasError('required')) {
      return 'Please enter a value';
    }
    return 'Enter a valid address';
  }

  getPostalCodeErrorMessage() {
    if (this.postalCodeControl.hasError('required')) {
      return 'Please enter a value';
    }
    return 'Enter a valid postal code';
  }

  getPhoneNumberErrorMessage() {
    if (this.phoneNumberControl.hasError('required')) {
      return 'Please enter a value';
    }
    return 'Enter a valid phone number';
  }


  getUsernameErrorMessage() {
    if (this.usernameControl.hasError('required')) {
      return 'Please enter a value'
    }
    return 'Enter a valid user name';
  }

  url: string = "";

  async uploadImageProfile() {
    let test: PickerResponse;
    const client = filestack.init('AzwS9T9PFRpW1fLDaalWgz');
    const options = {
      transformations: {
        crop: false,
        circle: true,
        rotate: true
      },
      maxFiles: 1,
      onUploadDone: (res: PickerResponse) => {
        test = res;
        for (let i = 0; i < res.filesUploaded.length; i++) {
          console.log("res:", res.filesUploaded[i].handle);
          this.currentUser.img = res.filesUploaded[i].url.toString();
        }
      }
    }
    client.picker(options).open();
  }

  displayUser() {
    console.log(this.currentUser.img)
  }
}
