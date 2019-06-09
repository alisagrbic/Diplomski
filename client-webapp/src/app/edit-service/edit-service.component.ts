import { Component, OnInit } from '@angular/core';
import { DemoServiceService } from '../demoService/demo-service.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Service } from '../models/service';
import { debug } from 'util';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-edit-service',
  templateUrl: './edit-service.component.html',
  styleUrls: ['./edit-service.component.css']
})

export class EditServiceComponent implements OnInit {

  serviceId: number = -1;
  activeService: Service;
  
  constructor(private service: DemoServiceService, private activatedRoute: ActivatedRoute, private router: Router) {
    this.activatedRoute.params.subscribe(params => {this.serviceId = params["Id"]});
   }


  ngOnInit() {
    this.service.getMethodDemo("http://localhost:51111/api/Services/" + this.serviceId).subscribe(
      data => {
        this.activeService = data;
      },
      error => {
        alert("nije uspelo")
      });
  }

  onSubmit(form: NgForm){
  
  this.service.updateService(this.serviceId, this.activeService).subscribe(
    data => {
      this.router.navigate(['services/']);
      alert("Uspesno izmenjen servis!")
    },
    error => {
      alert("nije uspelo")
    }); 
}
}
