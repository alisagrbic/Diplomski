import { Component, OnInit } from '@angular/core';
import { AppUser } from '../models/AppUser.model';
import { NgForm } from '@angular/forms';
import { DemoServiceService } from '../demoService/demo-service.service';
import { Router } from '@angular/router';
import { Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {

  roles: any = ["Client", "Manager"];
  selectedRole: string;

  constructor(private service: DemoServiceService, private router: Router) {
  
   }

  ngOnInit() {
  }

  onSubmit(user: AppUser, form: NgForm) {
    debugger
    user.role = this.selectedRole;
    if(this.selectedRole == "Manager"){
      user.role = "Manager";
    }
    else{
      user.role = "AppUser";
    }
    
    this.service.postMethodDemo("http://localhost:51111/api/Account/Register", user).subscribe(
      data => {
        alert("Uspesno ste se registrovali")
        this.router.navigate(['services']);
      },
      error => {
        debugger
        alert("nije uspelo")
      })

    form.reset();
  }

}