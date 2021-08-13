import { RoutesService, eLayoutType } from '@abp/ng.core';
import { CurrentUserComponent } from '@abp/ng.theme.basic';

import { APP_INITIALIZER } from '@angular/core';
import { OAuthService} from 'angular-oauth2-oidc';



export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService, OAuthService], multi: true },
];

function configureRoutes(routesService: RoutesService, oauthservice: OAuthService) {

  var invis = false;
 
  

    
    var role = localStorage.getItem('role');
  if (role == 'user') {
    localStorage.setItem('invis', 'true');
      invis = true;
      console.log("user invis", invis);
      
    }
  else {
    localStorage.setItem('invis', 'false');
      invis = false;
      console.log("invis", invis);
    }
    





 

  return () => {
    routesService.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
        
      },
      {
        path: '/',
        name: 'ToDo Setting',
        iconClass: 'fa fa-cog ng-star-inserted',
        order: 2,
        layout: eLayoutType.application,
        invisible: invis
      },
      {
        path: '/categories',
        name: 'Category ',
        parentName: 'ToDo Setting',
        iconClass: 'fa fa-wrench ng-star-inserted',
        layout: eLayoutType.application,
      },
      {
        path: '/tasks',
        name: 'Task ',
        parentName: 'ToDo Setting',
        iconClass: 'fa fa-wrench ng-star-inserted',
        layout: eLayoutType.application,
      },
      {
        path: '/todos',
        name: 'Status ',
        parentName: 'ToDo Setting',
        iconClass: 'fa fa-wrench ng-star-inserted',
        layout: eLayoutType.application,
      },
      
      {
        path: '/priorities',
        name: 'Priority',
        parentName: 'ToDo Setting',
        iconClass: 'fa fa-wrench ng-star-inserted',
        layout: eLayoutType.application,
      },
    ]);
    
  };
}
