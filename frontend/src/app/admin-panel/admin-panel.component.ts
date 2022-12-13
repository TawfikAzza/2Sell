import { Component, OnInit } from '@angular/core';
import {HttpService} from "../../services/http.service";
import {registerDTO} from "../../entities/entities";
import {Router} from "@angular/router";

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {

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
          this.router.navigate(['admin-panel']);
        }));

  }

  deleteUser(email: string) {
    console.log("Delete user not implemented yet, is it necessary ?");
  }
}
