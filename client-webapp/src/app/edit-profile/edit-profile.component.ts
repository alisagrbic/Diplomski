import { Component, OnInit } from '@angular/core';
import { ActiveUser } from '../models/ActiveUser.model';
import { NgForm } from '@angular/forms';
import { DemoServiceService } from '../demoService/demo-service.service';


@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})

export class EditProfileComponent implements OnInit {


  activeUser: ActiveUser;
  activeUserId: number = -1;
  url: string;
  selectedFile: string;
 // public uploader: FileUploader = new FileUploader({url: URL, itemAlias: 'photo'});

  constructor(private service: DemoServiceService) { }
  

  ngOnInit() {
    this.service.getMethodDemo("http://localhost:51111/api/GetActiveUserId").subscribe(
      data => {
        this.activeUserId = data;
        this.service.getMethodDemo("http://localhost:51111/api/AppUsers/" + this.activeUserId).subscribe(
          data => {
            this.activeUser = data;
          })
      })
  }

  onSubmit(form: NgForm) {
      this.service.updateProfile(this.activeUserId, this.activeUser).subscribe(
        data => {
          alert("Uspelo")
        },
        error => {
          alert("nije uspelo")
        });
    
  }

  onSelectFile(event) {
    debugger
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]); // read file as data url

      reader.onload = (event) => { // called once readAsDataURL is completed
        this.url = `${(event.target as any).result}`;
      this.activeUser.Image = this.url
      }
      this.selectedFile = event.target.files[0];

    }
  }

  handleUpload(data): void{
    if(data && data.response){
      data = JSON.parse(data.response);
    }
  }
}