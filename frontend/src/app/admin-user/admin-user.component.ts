import { Component, OnInit } from '@angular/core';
import {HttpService} from "../../services/http.service";
import {Router} from "@angular/router";
import {registerDTO} from "../../entities/entities";

@Component({
  selector: 'app-admin-user',
  templateUrl: './admin-user.component.html',
  styleUrls: ['./admin-user.component.css']
})
export class AdminUserComponent implements OnInit {

  constructor(private http:HttpService,
              private router:Router) { }
  listUser:registerDTO[]=[];
  ngOnInit(): void {
    this.getAllUsers();
  }
  async getAllUsers(){
    this.listUser = await this.http.getAllUsers();
  }


  async changeBanStatus(email: string) {
    await this.http.changeBanStatus(email)
      .then(()=> this.router.navigateByUrl('/', { skipLocationChange: true })
        .then(() => {
          this.router.navigate(['admin-user']);
        }));

  }

  deleteUser(email: string) {
    console.log("Delete user not implemented yet, is it necessary ?");
  }

}
