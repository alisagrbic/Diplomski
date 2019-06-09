import { Component, OnInit } from '@angular/core';
import { DemoServiceService } from '../demoService/demo-service.service';
import { ActiveUser } from '../models/ActiveUser.model';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})

export class ProfileComponent implements OnInit {

  user: ActiveUser = {} as ActiveUser;
  activeId: number = -1;
  selectedFile: any;

  constructor(private service: DemoServiceService) {
    debugger
    this.service.getMethodDemo("http://localhost:51111/api/GetActiveUserId").subscribe(
      data => {
        this.activeId = data;
        debugger
        this.service.getMethodDemo("http://localhost:51111/api/AppUsers/" + this.activeId).subscribe(
          data => {
            this.user = data;
            debugger
          });
      });
   }

  ngOnInit() {
  
  }

  onFileChange(event)
  {
    this.selectedFile = event.target.files[0];
  }

  OnSubmit(user: ActiveUser, form: NgForm)
  {     
    debugger
      this.service.putUserPhoto(user, this.selectedFile)
      .subscribe(
        data => {
          console.log(this.user);
        },
        error => {
          console.log(error);
        }        
      );
      form.reset();
  }
 
}
