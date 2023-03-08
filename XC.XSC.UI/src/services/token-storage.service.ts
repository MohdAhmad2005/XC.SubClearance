// import { Injectable } from '@angular/core';
// import { Subject } from 'rxjs';
// import { UserInfoDto } from 'src/app/models/user/user-info-dto';
// import { AesEncryption } from './aes-encryption-service.service';

// const Access_Token = 'cp_access_token';
// const Refresh_Token = 'cp_refresh_token';
// const customerUserInfo = 'customerUserInfo';

// @Injectable({
//   providedIn: 'root'
// })
// export class TokenStorageService {

//   private authenticationChanged = new Subject<boolean>();
  
//   constructor(private _cryptoService: AesEncryption,) { }


//   getToken(): string | null {
//     return localStorage.getItem(Access_Token);
//   }
 
//   setToken(token: string): void {    
//     localStorage.setItem(Access_Token, token);
//     this.authenticationChanged.next(this.isAuthenticated());
//   }

//   saveToken(token: string): void {
    
//     localStorage.clear();
//     localStorage.removeItem(Access_Token);
//     localStorage.setItem(Access_Token, token);

//     this.authenticationChanged.next(this.isAuthenticated());
//   }

//   setRefreshToken(token: string): void {
//     localStorage.removeItem(Refresh_Token);
//     localStorage.setItem(Refresh_Token, token);
//   }
  
//   getRefreshToken(): string | null {
//     return localStorage.getItem(Refresh_Token);
//   }

//   removeToken(): void {
//     localStorage.removeItem(Access_Token);
//     localStorage.removeItem(Refresh_Token);
//     localStorage.removeItem(customerUserInfo);
//   }

//   removeRefreshToken(): void {
//     localStorage.removeItem(Refresh_Token);
//   }

//   getUser(): any {
//     const user = localStorage.getItem(Access_Token);
//     if (user) {
//       return JSON.parse(user);
//     }
//     return {};
//   }

//   setCustomerUserInfo(userInfo: UserInfoDto): void {
//     localStorage.removeItem(customerUserInfo);
//     localStorage.setItem(customerUserInfo, this._cryptoService.encrypt(JSON.stringify(userInfo)));    
//   }

//   getCustomerUserInfo(): UserInfoDto {
//     return JSON.parse(this._cryptoService.decrypt(localStorage.getItem('customerUserInfo')));
//   }

//   removeCustomerUserInfo(): void {
//     localStorage.removeItem(customerUserInfo);
//   }

//   public isAuthenticated():boolean {
//     return (!(localStorage.getItem(Access_Token) === undefined || 
//     localStorage.getItem(Access_Token) === null ||
//     localStorage.getItem(Access_Token) === 'null' ||
//     localStorage.getItem(Access_Token) === 'undefined' ||
//     localStorage.getItem(Access_Token) === ''));
//   }

//   public isAuthenticationChanged():any {
//     return this.authenticationChanged.asObservable();
//   }
// }
