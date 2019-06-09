import { Component, OnInit } from '@angular/core';
import { IsSomeLogged } from '../guard/auth.logged';
import { DemoServiceService } from '../demoService/demo-service.service';
import { NotificationService } from '../notification/notification.service';
import { IPayPalConfig, ICreateOrderRequest } from 'ngx-paypal';

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


public payPalConfig: IPayPalConfig = {
  currency: 'EUR',
  clientId: 'ASpJflCcuFrtD6QJBnuc1JKEcDvjXUlzu0RKH-IkGrJDrxAx8rGyOX_Pgq7t4eRPBcvZ-WswmLfA0pnJ',
  createOrderOnClient: (data) => <ICreateOrderRequest>{
    intent: 'CAPTURE',
    purchase_units: [
      {
        amount: {
          currency_code: 'EUR',
          value: '9.99',
          breakdown: {
            item_total: {
              currency_code: 'EUR',
              value: '9.99'
            }
          }
        },
        items: [
          {
            name: 'Naziv vozila',
            quantity: '1',
            category: 'DIGITAL_GOODS',
            unit_amount: {
              currency_code: 'EUR',
              value: '9.99',
            },
          }
        ]
      }
    ]
  },
  advanced: {
      commit: 'true'
  },
  style: {
      label: 'paypal',
      layout: 'vertical'
  },
  onApprove: (data, actions) => {
      console.log('onApprove - transaction was approved, but not authorized', data, actions);
      actions.order.get().then(details => {
          console.log('onApprove - you can get full order details inside onApprove: ', details);
      });

  },
  onClientAuthorization: (data) => {
      console.log('onClientAuthorization - you should probably inform your server about completed transaction at this point', data);
      this.notification();
  },
  onCancel: (data, actions) => {
      console.log('OnCancel', data, actions);
      this.notificationService.displayNotification('Some onCancel', true);

  },
  onError: err => {
      console.log('OnError', err);
      this.notificationService.displayNotification('Some onError', true);
  },
  onClick: (data, actions) => {
      console.log('onClick', data, actions);
      this.notificationService.displayNotification('Some onClick');
  }
};

}
