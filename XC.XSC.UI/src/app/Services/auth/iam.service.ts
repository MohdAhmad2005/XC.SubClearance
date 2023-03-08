import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { TenantConfig, TenantResponse } from 'src/app/models/iam/iam-tenant';
import { environment } from 'src/environments/environment';
import { EnvironmentService } from 'src/services/environment-service.service';

@Injectable({
  providedIn: 'root'
})
export class IAMService {

  @Output() keycloakConfigLoaded: EventEmitter<{isSuccess: boolean, config:TenantConfig}>;

  private messageSource = new BehaviorSubject({isSuccess: false, config:undefined});
  
  currentMessage = this.messageSource.asObservable();
  
  private _apiUrl: any = {
    getTenantOptionsApi: 'api/ccmp/uam/iam/getTenantConfig'   
  };
  
  private _config: TenantConfig;

  constructor(private _httpClient: HttpClient, 
    private _envService: EnvironmentService) {
  }

  private getRoute = ( apiRootUrl: string, apiUrl: string) => {
    return `${apiRootUrl}/${apiUrl}`;
  }
  
  public getTenantConfiguration() {    
      return this.getTenantOptions(encodeURIComponent(window.location.origin));    
  }

  public getTenantOptions = (rootUrl: string): Observable<TenantResponse> => {
    return this._httpClient.get<TenantResponse>(this.getRoute(`${environment.uamApiUrl}`, `${this._apiUrl.getTenantOptionsApi}/${rootUrl}`));
  }
  
}
