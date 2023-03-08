import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserInfoService {

  constructor() {
  }
  userDetails: any;
  token: string;

  setRefreshToken(token) {
    this.token = token;
  }

  setUserInfo(userDetails, token) {
    this.token = token;
    this.userDetails = JSON.parse(userDetails);
  }

  getUserDetails() {
    return this.userDetails ? this.userDetails : { LoginName: "", UserId: -1 };
  }

  getUserFullName() {
    return this.userDetails.FirstName + " " + this.userDetails.LastName;
  }

  getToken() {
    return this.token;
  }

  clearUserInfo() {
    this.token = null;
    this.userDetails = null;
  }

  setTimeZone(timezone) {
    if (this.userDetails.Timezone)
      this.userDetails.Timezone = timezone;
  }
}
