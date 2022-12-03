import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private router:Router) { }

  ngOnInit(): void {
  }

  async navigateMainPage() {
    await this.router.navigate(['mainPage'])
  }

  async navigateUserProfile() {
    await this.router.navigate(['myProfile'])
  }

  async navigateUserPosts() {
    await this.router.navigate(['myPosts'])
  }
}
