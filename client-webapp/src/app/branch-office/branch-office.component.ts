import { Component, OnInit } from '@angular/core';
import { BranchOffice } from '../models/branchoffice';
import { NgForm } from '@angular/forms';
import { DemoServiceService } from '../demoService/demo-service.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MapInfo } from '../models/map.model';
import { IsAdmin } from '../guard/auth.admin';

@Component({
  selector: 'app-branch-office',
  templateUrl: './branch-office.component.html',
  styleUrls: ['./branch-office.component.css'],
  styles: ['agm-map {height: 500px; width: 700px;}']
})

export class BranchOfficeComponent implements OnInit {

  serviceId: number;
  mapInfo: MapInfo;
  latNum: number;
  lngNum: number;

  constructor(private service: DemoServiceService, private activatedRoute: ActivatedRoute, private router: Router) { 
    debugger
    this.activatedRoute.params.subscribe(params => {this.serviceId = params["Id"]});    //Id je definisano u appmodule.ts kod path: "service/Id"
    this.allBranches(this.serviceId);
  }

  url: string = '';
  selectedFile: string;
  branches: BranchOffice[];
  activeUserId: number;

  ngOnInit() {
    this.service.getCurrentUser().subscribe(
      data =>
      {
        this.activeUserId = data;
      }
    );
    this.mapInfo = new MapInfo(45.242268, 19.842954, 
      "assets/ftn.png",
      "Jugodrvo" , "" , "http://ftn.uns.ac.rs/691618389/fakultet-tehnickih-nauka");
  }

  deleteBranch(id: number){

  for(var i=0; i<this.branches.length; i++){
   if(this.branches[i].BranchOfficeID == id){
     this.branches[i].Deleted = true;
     this.service.updateBranch(this.branches[i].BranchOfficeID, this.branches[i]).subscribe(
      data => {
        alert("Uspesno obrisana filijala!");
        this.router.navigate(['branches/' + this.serviceId]);
      },
      error => {
        alert("nije uspelo")
      }); 
   } 
  }
}

  allBranches(id){
    this.service.getAllBranchesForService(id).subscribe(
      data => {
        this.branches = data;
        debugger
      },
      error => {
        alert("nije uspelo")
      })
    }

    isManager(){
      return localStorage.role == 'Manager' ?  true : false;
    }

    isAdmin(){
      return localStorage.role == 'Admin' ?  true : false;
    }
  }  


