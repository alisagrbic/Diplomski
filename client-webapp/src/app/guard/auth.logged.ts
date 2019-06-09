import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { DemoServiceService } from '../demoService/demo-service.service';

@Injectable()
export class IsSomeLogged implements CanActivate {

  constructor() {}

  canActivate() {
    return localStorage.jwt ? true : false;
  }
}