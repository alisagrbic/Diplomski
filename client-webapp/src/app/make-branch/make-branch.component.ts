import { Component, OnInit, Input } from '@angular/core';
import { BranchOffice } from 'src/app/models/branchoffice';
import { NgForm } from '@angular/forms';
import { DemoServiceService } from '../demoService/demo-service.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MapInfo } from '../models/map.model';

@Component({
  selector: 'app-make-branch',
  templateUrl: './make-branch.component.html',
  styleUrls: ['./make-branch.component.css'],
  styles: ['agm-map {height: 500px; width: 700px;}']
})

export class MakeBranchComponent implements OnInit {

  branches: BranchOffice[];
  selectedFile: "";
  url: "";
  serviceId: number;
  activeUser: number;
  mapInfo: MapInfo;
  latNum: number;
  lngNum: number;
  
  constructor(private service: DemoServiceService, private activatedRoute: ActivatedRoute, private router: Router) { 
    this.activatedRoute.params.subscribe(params => {this.serviceId = params["Id"]});
  }
  

  ngOnInit() {
    this.mapInfo = new MapInfo(45.242268, 19.842954, 
      "assets/ftn.png",
      "Jugodrvo" , "" , "http://ftn.uns.ac.rs/691618389/fakultet-tehnickih-nauka");

      this.service.getCurrentUser().subscribe(
        data => {
          this.activeUser = data;
        },
        error => {
          alert("nije uspelo")
        })
  }


  onSelectFile(event) {
    if (event.target.files && event.target.files[0]) {
       var reader = new FileReader();

       reader.readAsDataURL(event.target.files[0]); // read file as data url

       reader.onload = (event) => { // called once readAsDataURL is completed
         this.url = (event.target as any).result;
       }

      this.selectedFile = event.target.files[0]
    }
  }
  
  onSubmit(newBranch: BranchOffice, form: NgForm){
    debugger
    newBranch.ServiceID = this.serviceId;
    newBranch.CreatorID = this.activeUser;

    newBranch.Latitude = this.latNum;
    newBranch.Longitude = this.lngNum;


    let body = new FormData();
    body.append('image', this.selectedFile)
    body.append('branch', JSON.stringify(newBranch))
    debugger
    this.service.postMethodDemo("http://localhost:51111/api/BranchOffice", body).subscribe(
        data => {
          alert("Uspesno ste se dodali novu filijalu!")
          this.router.navigate(['branches/' + this.serviceId]);
        },
        error => {
          alert("nije uspelo")
        });
        

    this.url = "";  
    form.reset();
  }

  
  placeMarker($event){
    console.log($event.coords.lat);
    console.log($event.coords.lng);
    this.latNum = $event.coords.lat;
    this.lngNum = $event.coords.lng;

  }


}


