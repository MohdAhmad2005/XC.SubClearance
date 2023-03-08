import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GetUserNotificationResponse } from '../../models/notification/notification';
import { NotificationServiceService } from './notification-service.service';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit {

  private id: number;
  public getUserNotificationResponse: GetUserNotificationResponse[];

  constructor(private _notificationService: NotificationServiceService, public _router: Router) { }

  ngOnInit(): void {
    this._notificationService.currentNotification.subscribe(
      () => (this.getUserNotification())
    );
  }

  public UpdateNotificationsStatus(id: number) { 
    this._notificationService.updateNotificationsStatus(id).subscribe(response => {
      if (response.isSuccess) {
        this.getUserNotification();
      }
    },
      error => {
      });
  }

  private getUserNotification(): void {
      this._notificationService.getUserNotification().subscribe(response => {
        if (response.isSuccess) {
          this.getUserNotificationResponse = response.result;
        }
        else {
          this.getUserNotificationResponse = [];
        }
      },
        error => {
          this.getUserNotificationResponse = [];
        });
  }

}
