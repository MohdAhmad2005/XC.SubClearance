import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { EnvironmentService } from '../../../services/environment-service.service';
import { IResponse } from '../../models/IResponse';

@Injectable({
  providedIn: 'root'
})
export class NotificationServiceService {

  private notificationSource = new BehaviorSubject<string>("");
  public currentNotification = this.notificationSource.asObservable();

  public _apiUrl: any = {
    getUserNotificationApi: 'Notification/getUserNotification',
    updateNotificationsStatusApi: 'Notification/updateNotificationsStatus',
  };

  constructor(
    private _httpClient: HttpClient
    , private _envService: EnvironmentService
  ) {}

  public getUserNotification = (): Observable<IResponse> => {
    return this._httpClient.get<IResponse>(this.getRoute(this._apiUrl.getUserNotificationApi, this._envService.smartClearApiUrl));
  }

  public updateNotificationsStatus = (id: number): Observable<IResponse> => {
    return this._httpClient.post<IResponse>(this.getRoute(this._apiUrl.updateNotificationsStatusApi, this._envService.smartClearApiUrl), id);
  }

  private getRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }

  public refereshNotification(): void {
    this.notificationSource.next("");
  }

}
