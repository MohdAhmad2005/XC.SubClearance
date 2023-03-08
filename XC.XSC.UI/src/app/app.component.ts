import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { KeycloakService } from 'keycloak-angular';
import { Subscription } from 'rxjs/internal/Subscription';
import { Constants } from './models/config/constants';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  title = Constants.TitleOfSite;
  subscription: Subscription;
  authentication: boolean;
  
  constructor(http: HttpClient,
    public _router: Router,
    private _keyCloakStorage: KeycloakService
    ) {
   
  }

  ngAfterViewInit() {
    this._keyCloakStorage.isLoggedIn().then((response) => {
      this.authentication = response;
    });
  }
  
}
