import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class JwtService {
  
  constructor(
    private _jwtHelper :JwtHelperService) { }

  public GetTokenDecoded(token: string): any {
    return this._jwtHelper.decodeToken(token);
  }

  public getTokenExpirationDate(token: string):Date {
    return this._jwtHelper.getTokenExpirationDate(token);
  }

  public isAuthenticated(token: string): boolean {
    return !this._jwtHelper.isTokenExpired(token);
  }

}
