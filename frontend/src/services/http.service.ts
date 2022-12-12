import {Injectable} from '@angular/core';
import axios from "axios";
import {environment} from "../environments/environment";
import {catchError, Observable} from "rxjs";
import {Router} from "@angular/router";
import {MatSnackBar} from "@angular/material/snack-bar";

import {filterSearchDTO, loginDTO, registerDTO,postDTO} from "../entities/entities";
import {PostfeedComponent} from "../app/postfeed/postfeed.component";




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


  post1:postDTO = {
    id:1,
    email: 'email',
    userName:'username',
    price: 22.15,
    title: 'titleofpost',
    description: 'description of the post that is pret of the post that is pretended to be ',
    authority: 1,
    address: 'some address 23',
    category: 2
  };

  post2:postDTO = {
    id:1,
    email: 'email2',
    userName:'username2',
    price: 33.15,
    title: 'titleofpost2',
    description: 'description of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescriptiscription of the post that is pretended 22222 to be so long that it doesnt even fitdescription of the post that is pretended 22222 to be so long that it doesnt even fitdescription of the post that is pretended 22222 to be so long that it doesnt even fitdescription of the post that is pretended 22222 to be so long that it doesnt even fitdescription of the post that is pretended 22222 to be so long that it doesnt even fitdescription of the post that is pretended 22222 to be so long that it doesnt even fit',
    authority: 3,
    address: 'some 222 address 23',
    category: 3
  };

  post3:postDTO = {
    id:1,
    email: 'email2',
    userName:'username2',
    price: 33.15,
    title: 'titleofpost2',
    description: 'description of the post that is pretended 22222 to be so long that it doesnt even fitdescription of the post that is pretended 22222 to be so long that it doesnt even fitdescription of the post that is pretended 22222 to be so long that it doesnt even fitdescription of the post that is pretended 22222 to be so long that it doesnt even fitdescription of the post that is pretended 22222 to be so long that it doesnt even fitdescription of the post that is pretended 22222 to be so long that it doesnt even fit',
    authority: 3,
    address: 'some 222 address 23',
    category: 3
  };

  post4:postDTO = {
    id:1,
    email: 'email2',
    userName:'username2',
    price: 33.15,
    title: 'titleofpost2',
    description: 'description of the post that is pretended 22222 to be so long that it doesnt even fitdescription of the post that is pretended 22222 to be so long that it doesnt even fitdescription of the post that is pretended 22222 to be so long that it doesnt even fitdescription of the post that is pretended 22222 to be so long that it doesnt even fitdescription of the post that is pretended 22222 to be so long that it doesnt even fitdescription of the post that is pretended 22222 to be so long that it doesnt even fit',
    authority: 3,
    address: 'some 222 address 23',
    category: 3
  };

  allPost: postDTO[] = [this.post1,
  this.post2, this.post3, this.post4];


  constructor(public matSnackbar: MatSnackBar,
              private router: Router,

  ){

  }
  async getUserByEmail(email:string) : Promise<registerDTO>{
    console.log("baseUrl",environment.baseUrl);
    let petition = await customAxios.get('WebShop/GetUserByEmail/'+email);
    console.log("petition",petition.data)
    return petition.data;
  }
  async register(param: registerDTO) {
    let petition = await customAxios.post('auth/register', param);
    if(petition.status == 200){
      localStorage.setItem('sessionToken', petition.data);
      this.matSnackbar.open("You have been registered", undefined, {duration: 3000})
    }
    else this.matSnackbar.open(petition.data, undefined, {duration:3000})
  }

  async login(param: loginDTO) {
    let petition = await customAxios.post('auth/login', param);
    if(petition.status == 200){
      localStorage.setItem('sessionToken', petition.data);
      this.matSnackbar.open('Welcome to 2Sell', undefined,{duration:3000})
    }
  }

  async updateProfile(dto: registerDTO) {
    let petition = await customAxios.post('WebShop/UpdateProfile',dto);
    console.log("petition",petition);
  }


  async filterSearch(dto: filterSearchDTO ):Promise<postDTO[]> {
    let dtoStringified: string="";
    dtoStringified = JSON.stringify(dto);

    let petition = await customAxios.get('WebShop/SearchCategories/'+dtoStringified)
      .then(function(response){
        let array:postDTO[]=[];
        for (let i = 0; i < response.data.length; i++) {
          console.log("For loop",response.data[i]);
          array.push(response.data[i])
        }
        return array;
      });
    this.result = petition;
    return petition;
  }

  async getAllPost():Promise<postDTO>{
    let petition = await customAxios.get('WebShop/GetAllPosts');
    this.allPost = petition.data;
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

}
