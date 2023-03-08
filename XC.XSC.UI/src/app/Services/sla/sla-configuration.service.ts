import { HttpClient } from '@angular/common/http';

import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { IResponse } from 'src/app/models/IResponse';
import { IslaConfigurationRequest } from 'src/app/models/Sla/sla';

import { EnvironmentService } from 'src/services/environment-service.service';



@Injectable({

  providedIn: 'root'

})

export class SlaManagementService {
  private _apiUrl: any = {
    saveSlaConfiguration: 'slaConfiguration/saveSlaConfiguration',
    updateSlaConfiguration: 'slaConfiguration/updateSlaConfiguration',
    getSlaConfigurationbyId: 'slaConfiguration/getSlaConfigurationbyId',
    getSlaConfigurationbyDetail: 'slaConfiguration/getAllSlaDetail',
  };

  constructor(private _httpClient: HttpClient, private _envService: EnvironmentService) { }

  public saveSlaConfiguration = (slaConfigurationRequest: IslaConfigurationRequest): Observable<IResponse> => {
    return this._httpClient.post<IResponse>(this.getRoute(`${this._apiUrl.saveSlaConfiguration}`,this._envService.smartClearApiUrl),slaConfigurationRequest);

  }
  public updateSlaConfiguration = (slaConfigurationRequest: IslaConfigurationRequest): Observable<IResponse> => {

    return this._httpClient.post<IResponse>(this.getRoute(`${this._apiUrl.updateSlaConfiguration}`,this._envService.smartClearApiUrl),slaConfigurationRequest);

  }

  public getSlaConfigurationById = (slaConfigId: number): Observable<IResponse> => {
    return this._httpClient.get<IResponse>(this.getRoute(`${this._apiUrl.getSlaConfigurationbyId}/${slaConfigId}`, this._envService.smartClearApiUrl));
  }
  public getSlaConfigurationByDetails = (): Observable<IResponse> => {

    return this._httpClient.get<IResponse>(this.getRoute(`${this._apiUrl.getSlaConfigurationbyDetail}`, this._envService.smartClearApiUrl));

  }

  private getRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }

 

}