import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {Router} from "@angular/router";
import {HttpService} from "../../services/http.service";
import {MailDTO} from "../../entities/entities";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-send-mail',
  templateUrl: './send-mail.component.html',
  styleUrls: ['./send-mail.component.css']
})
export class SendMailComponent implements OnInit {

  receiver: string="";
  subject: string="";
  sender: string="";
  receiverName: string="";
  senderName: string="";
  mail_content: string="";

  mail:MailDTO= {
    receiver:"",
    sender:"",
    subject:"",
    mail_content:"",
    receiverName:"",
    senderName:""
  }

  constructor(@Inject(MAT_DIALOG_DATA) public data:any,
              private router:Router,
              private dialogRef: MatDialogRef<SendMailComponent>,
              private http:HttpService,
              private matSnackbar:MatSnackBar) {

    this.receiver = data.receiver;
    this.sender = data.sender;
    this.subject = data.subject;
    this.receiverName = data.receiverName;
    this.senderName = data.senderName;
  }

  ngOnInit(): void {
  }

  sendMail() {
    console.log("Receiver : ",this.receiver)
    this.mail.receiver= this.receiver;
    this.mail.sender = this.sender;
    this.mail.subject = "2Sell: Your Post ["+this.subject+"] Received a message";
    this.mail.mail_content = this.mail_content;
    this.mail.receiverName = this.receiverName;
    this.mail.senderName = this.senderName;
    this.http.sendMail(this.mail);
    this.matSnackbar.open('Your email has been sent', undefined,{duration:3000})
    this.dialogRef.close();
  }

  onCancel() {
    this.dialogRef.close();
  }
}
