import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';

import { DemoServiceService } from '../demoService/demo-service.service';
import { Service } from '../models/service';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-make-service',
  templateUrl: './make-service.component.html',
  styleUrls: ['./make-service.component.css']
})

export class MakeServiceComponent implements OnInit {

  constructor(private service: DemoServiceService, private router: Router) { debugger}

  ngOnInit() {debugger
  }

  selectedFile: File;
  url: string;
  creator: number;

  onSelectFile(event) {
    if (event.target.files && event.target.files[0]) {
       var reader = new FileReader();

       reader.readAsDataURL(event.target.files[0]); 

       reader.onload = (event) => { 
         this.url = (event.target as any).result;
       }

      this.selectedFile = event.target.files[0]
    }
  }

  onSubmit(newService: Service, form: NgForm){

    this.service.getCurrentUser().subscribe(
      data => {
          this.creator = data;
          newService.CreatorID = this.creator;
          let body = new FormData();
          body.append('image', this.selectedFile)
          body.append('service', JSON.stringify(newService))
    
          this.service.postMethodDemo("http://localhost:51111/api/Services", body).subscribe(
            data => {
              this.router.navigate(['services']);
            },
            error => {
              alert("nije uspelo")
            })
      });

    this.url = "";  
    form.reset();
  }

}
