import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { LastSessionResponse } from 'src/app/models/account/last-session/last-session';
import { EnvironmentService } from '../../../services/environment-service.service';
import {UserAccountDetailResponse } from '../../models/account/user-account';
import { HttpErrorHandler } from '../handler/HttpErrorHandler';

@Injectable({
  providedIn: 'root'
})
export class AccountService {


  private _apiUrl: any = {
    getCurrentUserDetailsApi: 'User/getCurrentUserDetails',
    updateUserAccountDetailsApi:'UserProfile/UpdateUserProfile',
    getLastLoginDetailsApi: 'User/getLastLoginDetails'
  };

  constructor(private _httpClient: HttpClient, private _envService: EnvironmentService, private _errorHandler: HttpErrorHandler) { }

  public getUserAccountDetails(){
    return this._httpClient.get<UserAccountDetailResponse>(this.getRoute(`${this._apiUrl.getCurrentUserDetailsApi}`, this._envService.smartClearApiUrl))
    .pipe(catchError(this._errorHandler.handleError));
  }

  public updateUserAccountDetails = (updateRequest:any) => {
    return this._httpClient.post(this.getRoute(this._apiUrl.updateUserAccountDetailsApi, this._envService.smartClearApiUrl), updateRequest);
  }

  public getLastLoginDetailsApi(){
    return this._httpClient.get<LastSessionResponse>(this.getRoute(`${this._apiUrl.getLastLoginDetailsApi}`, this._envService.smartClearApiUrl))
    .pipe(catchError(this._errorHandler.handleError));
  }

  private getRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }

}
