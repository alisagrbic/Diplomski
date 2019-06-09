import { Component, OnInit } from '@angular/core';
import { DemoServiceService } from '../demoService/demo-service.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BranchOffice } from '../models/branchoffice';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-edit-branch',
  templateUrl: './edit-branch.component.html',
  styleUrls: ['./edit-branch.component.css']
})

export class EditBranchComponent implements OnInit {

  branchId: number = -1;
  activeBranch: BranchOffice;
  
  constructor(private service: DemoServiceService, private activatedRoute: ActivatedRoute, private router: Router) {
    this.activatedRoute.params.subscribe(params => {this.branchId = params["Id"]});
   }

  ngOnInit() {
    this.service.getMethodDemo("http://localhost:51111/api/BranchOffice/" + this.branchId).subscribe(
      data => {
        this.activeBranch = data;
      },
      error => {
        alert("nije uspelo")
      });
  }
  
  onSubmit(form: NgForm){
    this.service.updateBranch(this.branchId, this.activeBranch).subscribe(
      data => {
       // this.router.navigate(['branches/' + this.serviceId]);
        alert("Uspesno izmenjena filijala!")
      },
      error => {
        alert("nije uspelo")
      }); 
  }
  
}
