// import { Injectable } from '@angular/core';
// import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
// import { Observable } from 'rxjs';
// import { AuthService } from '../services/auth.service';
// import { TokenStorageService } from '../services/token-storage.service';

// @Injectable({
//   providedIn: 'root'
// })
// export class AuthguardGuard implements CanActivate {
//   constructor(private authService: AuthService, private tokenService: TokenStorageService, private router: Router) {

//   }
//   canActivate(
//     route: ActivatedRouteSnapshot,
//     state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
//     if (this.tokenService.getToken()) {
//       return true;
//     }
//     else {
//       return false;
//     }
//   }

// }
