import { Component, OnInit } from '@angular/core';
import { Vehicle } from '../models/vehicle';
import { DemoServiceService } from '../demoService/demo-service.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { VehicleType } from '../models/vehicle-type';

@Component({
  selector: 'app-edit-vehicle',
  templateUrl: './edit-vehicle.component.html',
  styleUrls: ['./edit-vehicle.component.css']
})

export class EditVehicleComponent implements OnInit {

  vehicleId: number = -1;
  activeVehicle: Vehicle;
  types: VehicleType[];
  yesNo: string[];
  years: number[];
  available: string;

  constructor(private service: DemoServiceService, private activatedRoute: ActivatedRoute, private router: Router) {
    this.activatedRoute.params.subscribe(params => {this.vehicleId = params["Id"]});
    this.types = [];
    this.years = [];
    this.yesNo = ["Yes", "No"];
    for(var i=1995; i<2018; i++){
      this.years.push(i);
    }
   }
   
  allVehicleTypes(path: string){
    this.service.getMethodDemo(path).subscribe(
      data => {
        this.types = data;
      },
      error => {
        alert("nije uspelo")
      })
  }

  ngOnInit() {
    this.service.getMethodDemo("http://localhost:51111/api/Vehicle/" + this.vehicleId).subscribe(
      data => {
        this.activeVehicle = data;
        this.activeVehicle.Available ? this.available = "Yes" : "No";
        this.allVehicleTypes('http://localhost:51111/api/VehicleType');
      },
      error => {
        alert("nije uspelo")
      });
  }


  onSubmit(form: NgForm){
    this.service.updateVehicle(this.vehicleId, this.activeVehicle).subscribe(
      data => {
        alert("Uspesno izmenjeno auto!");
        this.router.navigate(['services/' + this.activeVehicle.ServiceId]);
      },
      error => {
        alert("Nije uspela izmena auta")
      }); 
  }

  
  isManager(){
    return localStorage.role == 'Manager' ?  true : false;
  }
  
}
