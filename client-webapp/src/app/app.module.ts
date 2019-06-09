import { BrowserModule, makeStateKey } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AgmCoreModule } from '@agm/core';

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './interceptors/demo.interceptor';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { NavbarComponent } from './navbar/navbar.component';
import { HomeComponent } from './home/home.component';
import { ServicesComponent } from './services/services.component';
import { DemoServiceService } from './demoService/demo-service.service';
import { VehiclesComponent } from './vehicles/vehicles.component';
import { VehiclesReserveComponent } from './vehicles-reserve/vehicles-reserve.component'
import { ProfileComponent } from './profile/profile.component';
import { IsSomeLogged } from './guard/auth.logged';
import { IsAdmin } from './guard/auth.admin';
import { IsManager } from './guard/auth.manager';
import { IsClient } from './guard/auth.client';
import { MakeServiceComponent } from './make-service/make-service.component';
import { BranchOfficeComponent } from './branch-office/branch-office.component';
import { MakeBranchComponent } from './make-branch/make-branch.component';
import { MakeVehicleComponent } from './make-vehicle/make-vehicle.component';
import { EditServiceComponent } from './edit-service/edit-service.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { EditVehicleComponent } from './edit-vehicle/edit-vehicle.component';
import { EditBranchComponent } from './edit-branch/edit-branch.component';
import { NotificationService } from './notification/notification.service';
import { NotifierModule, NotifierOptions } from 'angular-notifier';

const notifierDefaultOptions: NotifierOptions = {
  position: {
    horizontal: {
      position: 'right',
      distance: 12
    },
    vertical: {
      position: 'top',
      distance: 12,
      gap: 10
    }
  },
  theme: 'material',
  behaviour: {
    autoHide: 5000,
    onClick: false,
    onMouseover: 'pauseAutoHide',
    showDismissButton: true,
    stacking: 4
  },
  animations: {
    enabled: true,
    show: {
      preset: 'slide',
      speed: 300,
      easing: 'ease'
    },
    hide: {
      preset: 'fade',
      speed: 300,
      easing: 'ease',
      offset: 50
    },
    shift: {
      speed: 300,
      easing: 'ease'
    },
    overlap: 150
  }
};

const ChildRoutes =
  [
    {
      path: "vehicles-reserve",
      component: VehiclesReserveComponent
    },
  ]

const Routes= [
  {
    path: "login",
    component: LoginComponent,
  },

  {
    path: "register",
    component: RegisterComponent,
  },

  {
    path: "home",
    component: HomeComponent
  },

  {
    path: "services",
    component: ServicesComponent
  },

  {
    path: "services/:Id",
    component: VehiclesComponent,
    children: ChildRoutes
  },

  {
    path: "branches/:Id",
    component: BranchOfficeComponent
  },

  {
    path: "profile",
    component: ProfileComponent,
    canActivate: [IsSomeLogged]
  },

  {
    path: "make-service",
    component: MakeServiceComponent,
    canActivate: ['isAdminOrManager']
  },

  {
    path: "make-branch/:Id",
    component: MakeBranchComponent,
    canActivate: ['isAdminOrManager']
  },
  {
    path: "make-vehicle/:Id",
    component: MakeVehicleComponent,
    canActivate: ['isAdminOrManager']
  },
  {
    path: "edit-service/:Id",
    component: EditServiceComponent,
    canActivate: ['isAdminOrManager']
  },
  {
    path: "edit-profile",
    component: EditProfileComponent,
    canActivate: ['isAdminOrManager']
    },
    {
      path: "edit-profile",
    component: EditProfileComponent,
    canActivate: [IsSomeLogged]
  },
  {
    path: "edit-branch/:Id",
      component: EditBranchComponent,
      canActivate: ['isAdminOrManager']
  },
  {
    path: "edit-vehicle/:Id",
      component: EditVehicleComponent,
      canActivate: ['isAdminOrManager']
  }
  
]

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    NavbarComponent,
    HomeComponent,
    ServicesComponent,
    VehiclesComponent,
    ProfileComponent,
    VehiclesReserveComponent,
    MakeServiceComponent,
    BranchOfficeComponent,
    MakeBranchComponent,
    MakeVehicleComponent,
    EditServiceComponent,
    EditProfileComponent,
    EditVehicleComponent,
    EditBranchComponent
    ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(Routes),
    FormsModule,
        HttpClientModule,
    AgmCoreModule.forRoot({apiKey: 'AIzaSyDnihJyw_34z5S1KZXp90pfTGAqhFszNJk'}),
    NotifierModule.withConfig(notifierDefaultOptions)
  ],
  providers: [
    DemoServiceService,
    NotificationService,
    IsAdmin,
    IsManager,
    IsClient,
    IsSomeLogged,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    {
      provide: 'IsSomeLogged',
      useValue: () => {
        return true;
      } 
    },
    {
      provide: 'isAdminOrManager',
      useValue: () => {
        if(localStorage.role == 'Admin' || localStorage.role == "Manager"){
          return true;
        }
      } 
    },

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

