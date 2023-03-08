import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { AddOrganizationUserRequest, AddUserResponse } from 'src/app/models/organization-user-management/Users/add-user';
import { UpdateOrganizationUserRequest, UpdateUserResponse } from 'src/app/models/organization-user-management/Users/update-user';
import { UserResponse, UserRoleResponse } from 'src/app/models/organization-user-management/Users/users';
import { EnvironmentService } from 'src/services/environment-service.service';
import { HttpErrorHandler } from '../handler/HttpErrorHandler';

@Injectable({
  providedIn: 'root'
})
export class OrganizationUserManagementService {

  public updateStatus;
  public editListData: any;
  public editId: string;
  public userEvent: any;

  private _apiUrl: any = {
    updateUserApi: 'User/updateUser'
  }

  public updateUserData(updateListData, id){
    this.editListData= updateListData;
    this.editId= id
  }

  public userEventReference(data){
    this.userEvent = data
  }

  constructor(private _httpClient: HttpClient, private _envService: EnvironmentService, private _errorHandler: HttpErrorHandler) { }
 
  public updateUser = (updateUserDetail: UpdateOrganizationUserRequest): Observable<UpdateUserResponse> => { 
    return this._httpClient.post<UpdateUserResponse>(this.getRoute(`${this._apiUrl.updateUserApi}`, this._envService.smartClearApiUrl), updateUserDetail)
      .pipe(catchError(this._errorHandler.handleError));
  }
 
  private getRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}
