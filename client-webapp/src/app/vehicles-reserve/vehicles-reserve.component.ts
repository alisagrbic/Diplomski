import { Component, OnInit, Input } from '@angular/core';
import { DemoServiceService } from '../demoService/demo-service.service';
import { BranchOffice } from '../models/branchoffice';
import { NgForm } from '@angular/forms';
import { Reservation } from '../models/reservation'
import { Vehicle } from '../models/vehicle';
import { Item } from '../models/item';


@Component({
  selector: 'app-vehicles-reserve',
  templateUrl: './vehicles-reserve.component.html',
  styleUrls: ['./vehicles-reserve.component.css']
})
export class VehiclesReserveComponent implements OnInit {

  BranchOffices: BranchOffice[];
  Branchoffice: number;
  Branchoffice1: number;
  UserId: number;
  GetDate: string;
  item: Item = {} as Item;
  itemPrice: number;

  @Input() vehicleId: number;

  constructor(private service: DemoServiceService) {
    this.BranchOffices = [];
  }


  ngOnInit() {

    this.allBranchOffices('http://localhost:51111/api/BranchOffice');
    this.service.getItemForVehicle(this.vehicleId).subscribe(
      data => {
        this.item = data;
        debugger
      },
      error => {
        alert("nije uspelooooo")
      })
  }

 

  allBranchOffices(path: string) {
    this.service.getMethodDemo(path).subscribe(
      data => {
        this.BranchOffices = data;
      },
      error => {
        alert("nije uspelo")
      })
  }

  ItemForVehicle(num: number){
    
    this.service.postMethodDemoItem(num).subscribe(
      data => {
        this.item = data;
        debugger
      },
      error => {
        alert("nije uspelooooo")
      })

    this.service.getItemForVehicle(num).subscribe(
      data => {
        this.item = data;
        debugger
      },
      error => {
        alert("nije uspelooooo")
      })
  }

  ReservationData(dataForm: Reservation, form: NgForm) {
    this.service.getMethodDemo("http://localhost:51111/api/GetActiveUserId").subscribe(
      data => {       
        this.UserId = data;
        debugger
        dataForm.ClientID = this.UserId;
        dataForm.VehicleID = this.vehicleId; 
        dataForm.GetBranchId = this.Branchoffice;       
        dataForm.ReturnBranchId = this.Branchoffice1;
        
        this.service.postMethodDemo("http://localhost:51111/api/Reservation", dataForm).subscribe(
          data => {
            debugger
            this.BranchOffices = data;
          },
          error => {
            alert("nije uspelo")
          })
        })
      }
    }