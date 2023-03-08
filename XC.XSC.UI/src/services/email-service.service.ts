import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RecoverEmailDto } from '../app/models/user/login-details-dto';
import { EnvironmentService } from './environment-service.service';

@Injectable({
  providedIn: 'root'
})
export class EmailServiceService {
  private _apiUrl: any = {
    SendResetPasswordLink: 'EmailService/SendResetPasswordLink',
  }; 

  constructor(private httpClient: HttpClient, private envService: EnvironmentService) {
  }

  SendResetPasswordLink = (body: RecoverEmailDto) => {
    return this.httpClient.post(this.getRoute(this._apiUrl.SendResetPasswordLink, this.envService.smartClearApiUrl), body);
  }
  private getRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}
