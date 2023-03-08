import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EnvironmentService } from 'src/services/environment-service.service';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {

  private _apiUrl: any = {
    download: 'Document/download'
  };

  constructor(private _httpClient: HttpClient, private _envService: EnvironmentService) { }
  
  public download(documentId:string) {
    return this._httpClient.get(this.getRoute(`${this._apiUrl.download}/${documentId}`, this._envService.smartClearApiUrl),{ responseType: 'blob'});
  }

  private getRoute = (route: string, baseUrl: string) => {
    return `${baseUrl}/${route}`;
  }
}
