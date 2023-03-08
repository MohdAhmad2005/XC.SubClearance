// import { HttpClient } from '@angular/common/http';
// import { Injectable } from '@angular/core';
// import { BehaviorSubject, Observable } from 'rxjs';
// import { AuthResponseDto } from 'src/app/models/user/auth-response-dto';
// import { ForgotPasswordDto, LoginDetailsDto, LogoutDto, ResetPasswordDto } from 'src/app/models/user/login-details-dto';
// import { AesEncryption } from './aes-encryption-service.service';
// import { EnvironmentService } from './environment-service.service';
// import { TokenStorageService } from './token-storage.service';

// @Injectable({
//   providedIn: 'root'
// })

// export class AuthService {

//   private currentUserSubject: BehaviorSubject<any>;
//   private _apiUrl: any = {
//     loginApi: 'auth/getToken',
//     logoutApi: 'auth/logout',
//     sendResetPasswordApi: 'CustomerPortal/SendVerificationCode',
//     resetPwdApi: 'CustomerPortal/ResetPwd',
//     forgotPasswordApi: 'auth/forgotPassword',
//     resetPasswordApi: 'auth/resetPassword',
//   };

//   public currentUser: Observable<any>;

//   constructor(private httpClient: HttpClient, private tokenStorage: TokenStorageService, private envService: EnvironmentService, private _cryptoService: AesEncryption,) {
//     this.currentUserSubject = new BehaviorSubject<any>(JSON.parse(this._cryptoService.decrypt(localStorage.getItem('customerUserInfo'))));
//     this.currentUser = this.currentUserSubject.asObservable();
//   }

//   public get currentUserValue() {
//     return this.currentUserSubject.value;
//   }

//   loginUser = (body: LoginDetailsDto): Observable<AuthResponseDto> => {
//     return this.httpClient.post<AuthResponseDto>(this.getRoute(this._apiUrl.loginApi, this.envService.smartClearApiUrl), body);
//   }

//   logoutUser = (body: LogoutDto): Observable<boolean> => {
//     return this.httpClient.post<boolean>(this.getRoute(this._apiUrl.logoutApi, this.envService.smartClearApiUrl), body);
//   }

//   logout = (): void => {
//     this.tokenStorage.removeToken()
//     this.tokenStorage.removeRefreshToken();
//     this.tokenStorage.removeCustomerUserInfo();
//     this.sendAuthStateChangeNotification(false);
//   }
//   ForgotPassword = (body: ForgotPasswordDto): Observable<AuthResponseDto> => {
//     return this.httpClient.post<AuthResponseDto>(this.getRoute(this._apiUrl.forgotPasswordApi, this.envService.smartClearApiUrl), body);
//   }

//   resetPassword = (body: ResetPasswordDto): Observable<AuthResponseDto> => {
//     return this.httpClient.post<AuthResponseDto>(this.getRoute(this._apiUrl.resetPasswordApi, this.envService.smartClearApiUrl), body);
//   }

//   reloadPage = (): void => {
//     window.location.reload();
//   }

//   sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
//     this.currentUserSubject.next(isAuthenticated);
//   }

//   private getRoute = (route: string, envAddress: string) => {
//     return `${envAddress}/${route}`;
//   }

// }
