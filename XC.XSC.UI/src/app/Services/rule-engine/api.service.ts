import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { GlobalService } from './global.service';
import { UserInfoService } from './user-info.service';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private currentSubject: BehaviorSubject<any>;
  localToken: String = '';
  public IsWinAuth: boolean = false;
  headers: HttpHeaders;
  constructor(private http: HttpClient, private _route: Router,
    private globalService: GlobalService,
    private userInfoService: UserInfoService
  ) {
  }

  private formatErrors(error: any, route: Router, gService: GlobalService) {
    let isExpire = '';
    let isEmpty = '';
    let param = 0;
    if (error.status === 401) {
      param = 401;
      isExpire = error.headers.get('IsTokenExpire');
      isEmpty = error.headers.get('EmptyToken');
      if (isExpire) {
        param = 4011;
      }
      else if (isEmpty) {
        param = 4012;
      }
    }
    else if (error.status === 0 || error.status === 408) {
      param = 408;
    }
    else if (error.status === 500) {
      param = 500;
    }
    else
      param = error.status;
    if (isExpire !== undefined && isExpire === 'true') {
      if (environment.AuthenticationMode == "System") {
        route.navigate(['/systemlogin'], {
          queryParams: {
            return: route.routerState.snapshot.url
          }
        });
      } else {
        route.navigate(["/login"], { queryParams: { code: param, msg: 'Your session has expired. Please log in again!' }, replaceUrl: true });
      }
    }
    else {
      route.navigate(["/unauthorized", param], { replaceUrl: true });
    }
    return throwError(error.error);
  }

  get(path: string, params: HttpParams = new HttpParams()): Observable<any> {
    this.currentSubject = new BehaviorSubject('');
    let route = this._route;
    let formatErrors = this.formatErrors;
    let gService = this.globalService;
    return this.http.get(`${environment.ruleEngineApiUrl}${path}`, { params: params, headers: this.headers, observe: "response" }).pipe(map(data => {
      if (data) {
        let tokenHeader = data.headers.get('refreshtoken');
        if (tokenHeader !== undefined && tokenHeader !== null) {
          let token = JSON.parse(tokenHeader);
          if (token) {
            this.userInfoService.setRefreshToken(token.Token);
          }
        }
        if (data.status === 401) {
          this._route.navigate(["/unauthorized"]);
        }
        this.currentSubject.next(data.body);
      }
      return data.body;
    })).pipe(catchError(function (err) {
      return formatErrors(err, route, gService);
    }));
  }

  put(path: string, body: Object = {}): Observable<any> {
    this.currentSubject = new BehaviorSubject('');
    let route = this._route;
    let formatErrors = this.formatErrors;
    let gService = this.globalService;
    return this.http.put(`${environment.ruleEngineApiUrl}${path}`, body, { headers: this.headers, observe: "response" }).pipe(map(data => {
      if (data) {
        let tokenHeader = data.headers.get('refreshtoken');
        if (tokenHeader !== undefined && tokenHeader !== null) {
          let token = JSON.parse(tokenHeader);
          if (token) {
            this.userInfoService.setRefreshToken(token);
          }
        }
        this.currentSubject.next(data.body);
      }
      return data.body;
    })).pipe(catchError(function (err) {
      return formatErrors(err, route, gService);
    }));
  }

  post(path: string, body: Object = {}): Observable<any> {
    this.currentSubject = new BehaviorSubject('');
    let route = this._route;
    let formatErrors = this.formatErrors;
    let gService = this.globalService;
    return this.http.post(`${environment.ruleEngineApiUrl}${path}`, body, { headers: this.headers, observe: "response" }).pipe(map(data => {
      if (data) {
        let tokenHeader = data.headers.get('refreshtoken');
        if (tokenHeader !== undefined && tokenHeader !== null) {
          let token = JSON.parse(tokenHeader);
          if (token) {
            this.userInfoService.setRefreshToken(token.Token);
          }
        }
        this.currentSubject.next(data.body);
      }
      return data.body;
    })).pipe(catchError(function (err) {
      return formatErrors(err, route, gService);
    }));
  }

  delete(path): Observable<any> {
    this.currentSubject = new BehaviorSubject('');
    let route = this._route;
    let formatErrors = this.formatErrors;
    let gService = this.globalService;
    return this.http.delete(`${environment.ruleEngineApiUrl}${path}`, { headers: this.headers, observe: "response" }).pipe(map(data => {
      if (data) {
        let tokenHeader = data.headers.get('refreshtoken');
        if (tokenHeader !== undefined && tokenHeader !== null) {
          let token = JSON.parse(tokenHeader);
          if (token) {
            this.userInfoService.setRefreshToken(token.Token);
          }
        }
        this.currentSubject.next(data.body);
      }
      return data.body;
    })).pipe(catchError(function (err) {
      return formatErrors(err, route, gService);
    }));
  }

  filepost(uploadReqFile: any): Observable<any> {
    let route = this._route;
    let formatErrors = this.formatErrors;
    let gService = this.globalService;
    return this.http.request(uploadReqFile).pipe(catchError(function (err) {
      return formatErrors(err, route, gService);
    }));
  }

}
