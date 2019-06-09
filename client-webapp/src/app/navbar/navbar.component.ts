import { Component, OnInit } from '@angular/core';
import { IsSomeLogged } from '../guard/auth.logged';
import { DemoServiceService } from '../demoService/demo-service.service';
import { NotificationService } from '../notification/notification.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})

export class NavbarComponent implements OnInit {

  loggedIn: boolean;
  
  constructor(private logged: IsSomeLogged, private service: DemoServiceService, 
    private notificationService: NotificationService) {
    this.service.currentLoginState.subscribe(loggedIn => this.loggedIn = loggedIn);
   }

  ngOnInit() {
    this.loggedIn = this.logged.canActivate();
  }

logOut(){
  localStorage.removeItem('jwt');
  localStorage.removeItem('role');
  this.loggedIn = this.logged.canActivate();

}

notification() {
  this.notificationService.displayNotification('Some text');
}

}
