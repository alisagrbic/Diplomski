import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { DemoServiceService } from '../demoService/demo-service.service';
import { Service } from '../models/service';
import { Vehicle } from '../models/vehicle';
import { BranchOffice } from '../models/branchoffice';
import { Comment } from '../models/comment';
import { Router } from '@angular/router';

@Component({
  selector: 'app-services',
  templateUrl: './services.component.html',
  styleUrls: ['./services.component.css']
})

export class ServicesComponent implements OnInit {

  services: Service[];
  vehicles: Vehicle[];
  branches: BranchOffice[];
  comments: Comment[];
  servicesForAdmin: Service[];
  allservices: Service[];
  ActiveService: Service;
  PopList: Service[];
  activeUser: number;

  constructor(private service: DemoServiceService, private router: Router) {
    this.services = [];
    this.vehicles = [];
    this.branches = [];
    this.comments = [];
    this.servicesForAdmin = [];
    this.allservices = [];
    }


  ngOnInit() {
    this.allServices();
debugger
    this.service.getCurrentUser().subscribe(
      data => {
        this.activeUser = data;
        debugger
      },
      error => {
        alert("nije uspelo")
      })
  }

  allServices() {
    debugger
    this.service.getAllServices().subscribe(
      data => {
        this.allservices = data;
        debugger
        this.allservices.forEach(el => {
          debugger
              if (el.Approved == true) {
                this.services.push(el);
              }
               else
              {
                this.servicesForAdmin.push(el);
              }
          
          });
        debugger
      error => {
        alert("nije uspelo")
      }
    }
    )
}

  deleteService(serviceId: number) {
    for (var i = 0; i < this.services.length; i++) {
      if (this.services[i].Id == serviceId) {
        this.services[i].Deleted = true;
        this.service.deleteService(this.services[i].Id, this.services[i]).subscribe(
          data => {
            this.services = [];
            this.allServices();
            this.router.navigate(['services']);
          },
          error => {
            alert("nije uspelo")
          });
     debugger

    this.services.forEach(data =>
      {
        if(data.Id == serviceId){
          data.Deleted=true;

          this.service.deleteService(data.Id, data).subscribe(
            data => {
              this.allServices();
              this.router.navigate(['services']);
            },
            error => {
              alert("nije uspelo")
            });
        }
      }
    )

  }
}
  }

  ApprovedService(id: number) {
    debugger
    this.service.getService(id).subscribe(
      data => {
        this.ActiveService = data;
        this.ActiveService.Approved = true;
        this.service.updateService(id, this.ActiveService).subscribe(
          data => {
            this.services=[];
            this.servicesForAdmin=[];
            this.allServices();
          this.router.navigate(['services']);
            debugger
            this.service.SendEmail(`${this.ActiveService.CreatorID}`, 1).subscribe(
              data =>{
                alert("uspjesno poslat mejl")
              }, 
              error => {
                alert("nije uspelo slanje mejla")
              }
            );
          },
          error => {
            alert("nije uspelo")
          });
      },
      error => {
        alert("nije uspelo")
      });
  }

  DenyService(id: number) {
    debugger
    this.service.getService(id).subscribe(
      data => {
        this.ActiveService = data;
        this.ActiveService.Deleted = true;
        this.service.deleteService(id, this.ActiveService).subscribe(
          data => {
            this.services = [];
            this.servicesForAdmin = [];
            this.allServices();
            this.service.SendEmail(`${this.ActiveService.CreatorID}`, 0).subscribe(
              data =>{
                alert("uspjesno poslat mejl")
              }, 
              error => {
                alert("nije uspelo slanje mejla")
              }
            );
          },
          error => {
            alert("nije uspelo")
          });

      })
  }

  isManager(){
    return localStorage.role == 'Manager' ?  true : false;
  }
  
  isAdmin(){
    return localStorage.role == 'Admin' ?  true : false;
  }
}
