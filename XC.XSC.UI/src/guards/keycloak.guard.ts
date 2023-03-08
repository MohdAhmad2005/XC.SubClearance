import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { KeycloakAuthGuard, KeycloakService } from 'keycloak-angular';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class KeycloakGuard extends KeycloakAuthGuard {
  
  constructor(protected override readonly router: Router, protected readonly _keycloakService: KeycloakService){
    super(router, _keycloakService);
  }

  override canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {

    if (!this.authenticated) {      
        return Promise.resolve(true);
    } else {
        return super.canActivate(route, state) as Promise<boolean>;
    }
  }

  public async isAccessAllowed( route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
  
    if (!this.authenticated) {
      await this._keycloakService.login({ redirectUri: window.location.origin + state.url });
    }

    const requiredRoles = route.data['roles'];
 
    if (!(requiredRoles instanceof Array) || requiredRoles.length === 0) {
      return true;
    }
 
    return requiredRoles.every((role) => this.roles.includes(role));
  }
}
