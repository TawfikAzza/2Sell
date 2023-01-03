import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from "@angular/router";
import {Observable} from "rxjs";
import jwtDecode from "jwt-decode";
import {sessionToken} from "../entities/entities";

@Injectable({
  providedIn: 'root'
})
export class AuthguardService implements CanActivate {

  constructor(private router: Router) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
    Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    let token = localStorage.getItem('sessionToken');
    if (token) {
      let decodedToken = jwtDecode(token) as sessionToken;
      let currentDate = new Date();
      console.log("exp date: ",decodedToken.exp)
      if (decodedToken.exp) {
        let expDate = new Date(decodedToken.exp * 1000);
        if (currentDate < expDate && decodedToken.role == 1) {
          return true;
        }
        if (currentDate < expDate && decodedToken.role == 0) {
          return true;
        }
      }
    }
    return true;
  }
}
