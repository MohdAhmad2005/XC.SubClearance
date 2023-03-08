import { Component, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { KeycloakService } from 'keycloak-angular';

@Component({
  selector: 'app-xsc-logout',
  templateUrl: './xsc-logout.component.html',
  styleUrls: ['./xsc-logout.component.css']
})
export class XscLogoutComponent implements OnInit {

  private $counter: Observable<number>;
  mySubscription: Subscription;

  constructor(
    private _keyCloakService: KeycloakService) { 
      this._keyCloakService.logout();
    }

  ngOnInit(): void {

  }

}
