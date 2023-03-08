import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserFilterModel } from 'src/app/models/uam/filter/user-filter';
import { ConvertObjectToQueryparamService } from 'src/app/utility/common/coonvert-object-to-queryparam.service';
import { EnvironmentService } from '../../../../services/environment-service.service';
import { IResponse } from '../../../models/IResponse';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private _apiUrl: any = {
    getUsersByFilters: 'User/getUsersByFilters'

  };

  constructor(private _httpClient: HttpClient, private _envService: EnvironmentService) { }

  public getUsersByFilters(filter:UserFilterModel){
    return this._httpClient.post<IResponse>(this.getRoute(`${this._apiUrl.getUsersByFilters}`, this._envService.smartClearApiUrl), filter); 
  }


  private getRoute = (route: string, baseUrl: string) => {
    return `${baseUrl}/${route}`;
  }
}
