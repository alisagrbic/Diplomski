import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { Injectable } from "@angular/core";
import { NotifierService } from "angular-notifier";
import { DemoServiceService } from "../demoService/demo-service.service";
declare var $: any;

@Injectable({
    providedIn: 'root'
})

export class NotificationService {
    hubConnection: any;

    // https://localhost:44300/api/Account/TestSignalR
    constructor(private httpClient: HttpClient, private router: Router, private notifierService: NotifierService ) { 
    }

    init() {
        try {
            this.startConnection();
            this.addTransferChartDataListener();
        } catch(e) {
            console.error(e);
        }
    }

    displayNotification(text, error?) {
        this.notifierService.notify(error ? 'error' : 'success', text);
    }

    private startConnection = () => {

        this.hubConnection  = $.hubConnection('https://localhost:44300'    );
        this.hubConnection.start();
              
    }

    public addTransferChartDataListener = () => {

        var notifications = this.hubConnection.createHubProxy('notifications');
        notifications.on('notification', (data) => {
            this.displayNotification(data);
            console.log(data);
   
  
          });
      }
}