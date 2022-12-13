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
      if (decodedToken.expDate) {
        let expDate = new Date(decodedToken.expDate * 1000);
        if (currentDate < expDate && decodedToken.role == 1) {
          return true;
        }
      }
    } else this.router.navigate(['login']);
    return false;
  }
}
