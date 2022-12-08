import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import {MatCardModule} from "@angular/material/card";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatButtonModule} from "@angular/material/button";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {RouterModule, Routes} from "@angular/router";
import {AuthguardService} from "../services/authguard.service";
import { MainpageComponent } from './mainpage/mainpage.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ProfileComponent } from './profile/profile.component';
import { CategoriesbarComponent } from './categoriesbar/categoriesbar.component';
import { PostfeedComponent } from './postfeed/postfeed.component';
import {MatCheckboxModule} from "@angular/material/checkbox";
import {MatSliderModule} from "@angular/material/slider";
import {MatExpansionModule} from "@angular/material/expansion";
import { PostComponent } from './post/post.component';

const routes: Routes = [
  {
    path: 'register', component: RegisterComponent
  },
  {
    path: 'login', component: LoginComponent
  },
  {
    path: 'mainPage', component: MainpageComponent
  },
  {
    path: 'navbartest', component: NavbarComponent
  },
  {
    path: 'profile', component: ProfileComponent
  },
  {
    path: '**', redirectTo: 'mainPage'
  }
]

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    MainpageComponent,
    NavbarComponent,
    ProfileComponent,
    CategoriesbarComponent,
    PostfeedComponent,
    PostComponent
  ],
  imports: [
    RouterModule.forRoot(routes),
    BrowserModule,
    BrowserAnimationsModule,
    MatCardModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSnackBarModule,
    MatCheckboxModule,
    MatSliderModule,
    FormsModule,
    MatExpansionModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
