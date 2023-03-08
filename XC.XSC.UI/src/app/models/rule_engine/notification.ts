export class NotificationModel {
    notification: string;
    read:boolean;
    notificationData: UserNotification[]
}

export class UserNotification {
    userName: string;
    role: string;    
}