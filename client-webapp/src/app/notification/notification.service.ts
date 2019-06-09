import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { Injectable } from "@angular/core";
import * as $ from 'jquery';
import * as signalR from "@aspnet/signalr";
import { NotifierService } from "angular-notifier";

@Injectable({
    providedIn: 'root'
})

export class NotificationService {



    constructor(private httpClient: HttpClient, private router: Router, private notifierService: NotifierService ) { }

    displayNotification(text) {
        this.notifierService.notify( 'success', text);
    }

}