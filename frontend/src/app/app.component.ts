import {Component} from '@angular/core';
import {NavigationEnd, Router, Event} from "@angular/router";
import {HttpService} from "../services/http.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import jwtDecode from "jwt-decode";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  currentRoute: string = "";

  constructor(private router: Router,
              public http: HttpService,
              private snackBar: MatSnackBar) {
    this.router.events.subscribe((event: Event) => {
      if (event instanceof NavigationEnd) {
        this.currentRoute = event.url;
      }
    });
    let t = localStorage.getItem('sessionToken')
    if (t) {
      let decoded = jwtDecode(t) as any;
      this.http.currentUserEmail = decoded.email;
    }


  }
}
