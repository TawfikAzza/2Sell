import {Injectable} from '@angular/core';
import axios from "axios";
import {environment} from "../environments/environment";
import {catchError, Observable} from "rxjs";
import {Router} from "@angular/router";
import {MatSnackBar} from "@angular/material/snack-bar";

import {
  filterSearchDTO,
  loginDTO,
  registerDTO,
  postDTO,
  createPostDTO,
  sessionToken,
  UserProperties
} from "../entities/entities";
import {PostfeedComponent} from "../app/postfeed/postfeed.component";
import jwtDecode from "jwt-decode";




export const customAxios = axios.create({
  baseURL: environment.baseUrl,
  headers:{'Content-Type': 'application/json; charset=utf-8'}
})

//To ask: Why in the sample project given the axios is initialized like this
//and what are the advantages and disadvantages of doing it like this

@Injectable({
  providedIn: 'root'
})

export class HttpService {
  currentUserEmail: any;
  result: postDTO[] = [];
  allPost: postDTO[] = [];
  logged:boolean=false;
  currentUser:registerDTO={
    email:"",
    password:"",
    userName:"",
    address:"",
    firstName:"",
    lastName:"",
    phoneNumber:"",
    postalCode:"",
    img:"",
    roleId:1
  };
  constructor(public matSnackbar: MatSnackBar,
              private router: Router,

  ){

    customAxios.interceptors.response.use(
      response => {

        return response;
      }, rejected => {

        catchError(rejected);
      }
    );

    customAxios.interceptors.request.use(
      async config => {
        if(localStorage.getItem('sessionToken')) {
          config.headers = {
            'Content-Type': 'application/json; charset=utf-8',
            'Authorization': `Bearer ${localStorage.getItem('sessionToken')}`
          }
        }
        return config;
      },
      error => {
        Promise.reject(error)
      });

  }
  ngOnInit() {
    let tokenString:string|null = sessionStorage.getItem('Email');
    console.log("Token String: ",tokenString);
    if(tokenString!="" && tokenString!=null) {
      this.registerUser(tokenString);
    }
  }
  async registerUser(email:string){
    this.currentUser =  await this.getUserByEmail(email);
    console.log("User: ",this.currentUser)
  }
  async getUserByEmail(email:string) : Promise<registerDTO>{
    console.log("baseUrl",environment.baseUrl);
    let petition = await customAxios.get('WebShop/GetUserByEmail/'+email);
    console.log("petition",petition.data);
    return petition.data;
  }
  async register(param: registerDTO) {
    let petition = await customAxios.post('auth/register', param);
    if(petition.status == 200){
      localStorage.setItem('sessionToken', petition.data);
      sessionStorage.setItem('Email',param.email);
      this.matSnackbar.open("You have been registered", undefined, {duration: 3000})
    }
    else this.matSnackbar.open(petition.data, undefined, {duration:3000})
  }

  async login(param: loginDTO) {
    let petition = await customAxios.post('Auth/login', param);
    if(petition.status == 200){
      localStorage.setItem('sessionToken', petition.data);
      sessionStorage.setItem('Email',param.Email);
      this.logged=true;
      this.matSnackbar.open('Welcome to 2Sell', undefined,{duration:3000});
    }
  }

  async updateProfile(dto: registerDTO) {
    let petition = await customAxios.post('WebShop/UpdateProfile',dto);
    console.log("petition",petition);
  }



  async filterSearch(dto: filterSearchDTO ):Promise<postDTO[]> {
    let dtoStringified: string="";
    dtoStringified = JSON.stringify(dto);
    this.result=[];
    let petition = await customAxios.get('WebShop/SearchCategories/'+dtoStringified)
      .then(function(response){
        let array:postDTO[]=[];
        if(response.data.length===0) {
          console.log("List Empty");
        }
        for (let i = 0; i < response.data.length; i++) {
          console.log("For loop",response.data[i]);
          array.push(response.data[i])
        }
        return array;
      }).catch(err => {
        let emptyArray:postDTO[]=[];
        return emptyArray;
      });

    this.result = petition;
    console.log("result size:",this.result.length);
    return petition;
  }

  emptypost: postDTO[] = [{
    id: 0,
    email: 'no emaoil',
    userName:'string',
    price: 22.1,
    title:'string',
    description:'string',
    authority:1,
    address:'string',
    category:1,
    img:'string'
}]

  async getAllPost():Promise<postDTO[]>{
    let petition = await customAxios.get('WebShop/GetAllPosts');
    if(petition.data == []){
      return this.emptypost;
    }
    this.allPost = petition.data;
    this.result = petition.data;
    return petition.data;
  }



  async getPost(id: number):Promise<postDTO> {
    let petition = await customAxios.get('WebShop/ViewPost/'+id);
    return petition.data;
  }

  async uploadFile(file: FormData) {

      const config = {
        headers: {
         'contentType':'Content-type: multipart/form-data'

        }
      };
      console.log("file",file.get('data') );
      let petition = await customAxios.post('WebShop/UploadFile/', file,config);
      return petition.data;
  }

  async CreatePost(createdPost: createPostDTO) {
      let stringData = JSON.stringify(createdPost);
    console.log("string :",stringData);
      await customAxios.post("WebShop/CreatePost",createdPost);
  }

  getUserProperties(item: string | null):UserProperties {
    let userProperties:UserProperties={
      userName:"",
      email:"",
      roleId:1
    };
    if(item==null){
      this.router.navigate(['login']);
    }
    if (item != null) {
      let decodedToken = jwtDecode(item) as sessionToken;
      if (decodedToken.userName != null) {
        userProperties.userName = decodedToken.userName;
      }
      if (decodedToken.email != null) {
        userProperties.email = decodedToken.email;
      }
      if (decodedToken.role != null) {
        userProperties.roleId = decodedToken.role;
      }
    }
    return userProperties;
  }

  async getAllUsers() {
    let petition = await customAxios.get("WebShop/GetAllUsers");
    return petition.data;
  }

  async changeBanStatus(email: string) {
      await customAxios.post("WebShop/ChangeBanStatus/", email);
  }

  async deletePost(id: number) {
    await customAxios.get("WebShop/DeletePost/"+id);
  }
}
