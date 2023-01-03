import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {MatDialogModule} from "@angular/material/dialog";
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


import { ViewpostComponent } from './viewpost/viewpost.component';
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MatIconModule} from "@angular/material/icon";
import {MatProgressBarModule} from "@angular/material/progress-bar";
import { NewpostComponent } from './newpost/newpost.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import {MatListModule} from "@angular/material/list";
import { AdminUserComponent } from './admin-user/admin-user.component';
import { AdminPostComponent } from './admin-post/admin-post.component';
import { NewCommentComponent } from './new-comment/new-comment.component';
import { SendMailComponent } from './send-mail/send-mail.component';
import { MypostsComponent } from './myposts/myposts.component';


const routes: Routes = [
  {
    path: 'admin-panel', component: AdminPanelComponent, canActivate: [AuthguardService]
  },
  {
    path: 'admin-user', component: AdminUserComponent, canActivate: [AuthguardService]
  },
  {
    path: 'admin-post', component: AdminPostComponent, canActivate: [AuthguardService]
  },
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
    path: 'profile', component: ProfileComponent , canActivate: [AuthguardService]
  },
  {
    path: 'myposts', component: MypostsComponent
  },
  {
    path: 'viewpost', component: ViewpostComponent
  },
  {
    path: 'newpost', component: NewpostComponent, canActivate: [AuthguardService]
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
    ViewpostComponent,
    NewpostComponent,
    AdminPanelComponent,
    AdminUserComponent,
    AdminPostComponent,
    NewCommentComponent,
    SendMailComponent,
    MypostsComponent
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
        MatExpansionModule,
        HttpClientModule,
        MatIconModule,
        MatProgressBarModule,
        MatListModule,
        MatDialogModule
    ],
  providers: [PostfeedComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
