import { Component, OnInit } from '@angular/core';
import { DemoServiceService } from '../demoService/demo-service.service';
import { LoginModel } from '../models/login-model';
import { NgForm } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  constructor(private demoService: DemoServiceService) { }

  ngOnInit() {

  }

logIn(user: LoginModel, form: NgForm){
  this.demoService.getTheToken(user);
}


}
