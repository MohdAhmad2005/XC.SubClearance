import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IResponse } from 'src/app/models/IResponse';
import { ConvertObjectToQueryparamService } from 'src/app/utility/common/coonvert-object-to-queryparam.service';
import { EnvironmentService } from 'src/services/environment-service.service';

@Injectable({
  providedIn: 'root'
})
export class EmailInfoService {

  private _apiUrl: any = {
    getEmailInfoDetailById: 'emailinfo/getEmailInfoDetailById',
    getMailBoxDetail:'emailinfo/getMailBoxList'
  };

  constructor(private _httpClient: HttpClient, private _envService: EnvironmentService,
    private _objToQueryParam: ConvertObjectToQueryparamService) { }
  
  public getEmailInfoDetailById = (emailInfoId: number): Observable<IResponse> => {
    return this._httpClient.get<IResponse>(this.getRoute(`${this._apiUrl.getEmailInfoDetailById}/${emailInfoId}`, this._envService.smartClearApiUrl));
  }

  public getMailBoxDetail(filter:any){
    let queryStringData = this._objToQueryParam.setQueryParamString(filter);
    return this._httpClient.get<IResponse>(this.getRoute(`${this._apiUrl.getMailBoxDetail}?${queryStringData}`, this._envService.smartClearApiUrl)); 
  }
  
  private getRoute = (route: string, baseUrl: string) => {
    return `${baseUrl}/${route}`;
  }
}
