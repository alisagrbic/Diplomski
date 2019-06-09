import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';

@Injectable()
export class IsClient implements CanActivate {

  constructor() {}

  canActivate() {
    return localStorage.role == 'AppUser' ?  true : false;
  }
}