// import { Injectable } from '@angular/core';
// import {
//   HttpRequest,
//   HttpHandler,
//   HttpEvent,
//   HttpInterceptor
// } from '@angular/common/http';
// import { catchError, Observable, of } from 'rxjs';
// import { environment } from 'src/environments/environment';

// import { Router } from '@angular/router';
// import { AuthService } from 'src/services/auth.service';
// import { TokenStorageService } from 'src/services/token-storage.service';

// @Injectable()
// export class AuthInterceptor implements HttpInterceptor {
//   private secureRoutes = [environment.domain];
//   private readonly authService: AuthService;
//   private readonly tokenService: TokenStorageService;
//   private readonly router: Router;
//   constructor(authService: AuthService, tokenService: TokenStorageService, router: Router) {
//     this.authService = authService;
//     this.tokenService = tokenService;
//     this.router = router;
//   }

//   intercept(request: HttpRequest<any>, next: HttpHandler) {
    
//     const token = this.tokenService.getToken();
    
//     request = request.clone({
//       setHeaders: { Authorization: `Bearer ${token}` }
//     });

//     return next.handle(request).pipe(catchError(
//       (err) => {
//         if (err.status === 401) {
//           this.authService.logout();
//           this.router.navigateByUrl('Logout');
//           return of(err);
//         }
//         throw err;
//       }
//     ));
//   }
// }
