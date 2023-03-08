import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { KeycloakService } from 'keycloak-angular';
import { UserAccountDetailResponse, UserAccountItem } from 'src/app/models/account/user-account';
import { UserInfoDto } from 'src/app/models/user/user-info-dto';
import { AccountService } from 'src/app/Services/account/account.service';
import { ToasterService } from '../toaster/toaster.service';

@Component({
  selector: 'app-xsc-header',
  templateUrl: './xsc-header.component.html',
  styleUrls: ['./xsc-header.component.css']
})
export class XscHeaderComponent implements OnInit {
  
  private _userProfile: UserInfoDto;
  public userAccountDetails: UserAccountItem;

  constructor(
    private _router: Router,
    private _keyCloakService: KeycloakService,    
    private _accountService: AccountService,
    private _toasterService: ToasterService) { }

  ngOnInit(): void {
    this.getCurrentUserProfile();
  }

  get userInfo(): UserInfoDto {
    return this._userProfile;
  }

  get userNameFirstLetter(): string {
    if(!(this.userInfo == null || this.userInfo == undefined )) {
      return this.userInfo.firstName.slice(0, 1);
    }      
    else {
        return '';
      }
    }
  
  get getFirstName(): string {
    if(!(this.userInfo == null || this.userInfo == undefined )) {
      return this.userInfo.firstName;
    }      
    else {
        return '';
      }    
    }
  
    get getEmail(): string {
      if(!(this.userInfo == null || this.userInfo == undefined )) {
        return this.userInfo.email;
      }      
      else {
          return '';
        }    
      }

  public openResetPassword():void {    
    this._router.navigate(["ResetPassword"]);
  }

  public logOut(): void {    
    this._keyCloakService.logout();
  }

  private getCurrentUserProfile():void{
    this._accountService.getUserAccountDetails().subscribe(
      (response: UserAccountDetailResponse) => {
        if (response.isSuccess) {
          this.userAccountDetails = response.result[0];
          this._userProfile = <UserInfoDto>{
            email: this.userAccountDetails.email,
            firstName: this.userAccountDetails.firstName,
            lastName: this.userAccountDetails.lastName
          };
        }
        else {
          this._toasterService.errorMessage(response.message);
        }
      },
      (error) => {
        this._toasterService.errorMessage(error);
      }
    );
  }
}
