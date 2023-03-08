import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IResponse } from 'src/app/models/IResponse';
import { IReviewConfigurationRequest, IReviewConfigurationUpdate } from 'src/app/models/review-management/review-management';
import { ConvertObjectToQueryparamService } from 'src/app/utility/common/coonvert-object-to-queryparam.service';
import { EnvironmentService } from 'src/services/environment-service.service';

@Injectable({
  providedIn: 'root'
})
export class ReviewManagementService {

  private _apiUrl: any = {
    saveReviewConfiguration: 'reviewConfiguration/saveReviewConfiguration',
    updateReviewConfiguration: 'reviewConfiguration/updateReviewConfiguration',
    deleteReviewConfigurationById: 'reviewConfiguration/deleteReviewConfigurationById',
    getAllReviewConfiguration :'reviewConfiguration/getAllReviewConfiguration',
    getReviewConfig :'reviewConfiguration/getReviewConfig'
  };
  
  constructor(private _httpClient: HttpClient, private _envService: EnvironmentService, private _objToQueryParam: ConvertObjectToQueryparamService) { }
  
  public saveReviewConfiguration = (reviewConfigurationRequest: IReviewConfigurationRequest): Observable<IResponse> => {
    return this._httpClient.post<IResponse>(this.getRoute(`${this._apiUrl.saveReviewConfiguration}`,this._envService.smartClearApiUrl),reviewConfigurationRequest);
  }

  public updateReviewConfiguration = (reviewConfigurationRequest: IReviewConfigurationUpdate): Observable<IResponse> => {
    return this._httpClient.post<IResponse>(this.getRoute(`${this._apiUrl.updateReviewConfiguration}`,this._envService.smartClearApiUrl),reviewConfigurationRequest);
  }

  public getAllReviewConfiguration = (): Observable<IResponse> => {
    return this._httpClient.get<IResponse>(this.getRoute(`${this._apiUrl.getAllReviewConfiguration}`, this._envService.smartClearApiUrl));
  }

  public getReviewConfig = (filter:any): Observable<IResponse> => {
    let queryStringData = this._objToQueryParam.setQueryParamString(filter);
    return this._httpClient.get<IResponse>(this.getRoute(`${this._apiUrl.getReviewConfig}?${queryStringData}`, this._envService.smartClearApiUrl));
  }

  public deleteReviewConfigurationById = (reviewConfigId: number): Observable<IResponse> => {
    return this._httpClient.post<IResponse>(this.getRoute(`${this._apiUrl.deleteReviewConfigurationById}`, this._envService.smartClearApiUrl),reviewConfigId);
  }

  private getRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
  
}
