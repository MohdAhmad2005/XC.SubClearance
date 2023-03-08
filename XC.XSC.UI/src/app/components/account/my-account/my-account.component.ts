import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ToasterService } from 'src/app/common/toaster/toaster.service';
import { UnknownMobileNumber, UserManagerRequired } from 'src/app/enum/user-manager-status';
import { LastSessionReport, LastSessionResponse } from 'src/app/models/account/last-session/last-session';
import { UpdateOrganizationUserRequest } from 'src/app/models/organization-user-management/Users/update-user';
import { UserBusinessDetails } from 'src/app/models/user/business-details';
import { OrganizationUserManagementService } from 'src/app/Services/organization-user-management/organization-user-management.service';
import { UserAccountDetailResponse, UserAccountItem } from '../../../models/account/user-account';
import { AccountService } from '../../../Services/account/account.service';

@Component({
  selector: 'app-my-account',
  templateUrl: './my-account.component.html',
  styleUrls: ['./my-account.component.css']
})
export class MyAccountComponent implements OnInit {

  public timestamp: number;

  public myAccountForm: FormGroup;
  public userAccountDetails: UserAccountItem;
  public updateUserDetail: UpdateOrganizationUserRequest;
  public lastLogginIn: LastSessionReport;
  public updatedUserDetails:any;
  public holidayListArray: string[] = [];
  public managerArray: string[] = [];
  public isTeamManagerArray: string[] = [];
  public regionArray: string[] = [];
  public teamArray: string[] = [];
  public lobArray: string[] = [];
  public roleArray: string[] = [];
  public userBusinessDetails :UserBusinessDetails;

  constructor(private _accountService: AccountService,
    private _toasterService: ToasterService,
    private _organizationUserManagementService: OrganizationUserManagementService) { }

  ngOnInit(): void {

    this.myAccountForm = new FormGroup({
      accountDetails: new FormGroup({
        userId: new FormControl({ value: '', disabled: true }),
        firstName: new FormControl({ value: '', disabled: true }),
        lastName: new FormControl({ value: '', disabled: true }),
        emailId: new FormControl({ value: '', disabled: true }),
        contactNumber: new FormControl(''),
      }),
      organisationDetails: new FormGroup({
        role: new FormControl({ value: '', disabled: true }),
        team: new FormControl({ value: '', disabled: true }),
        reportingManager: new FormControl({ value: '', disabled: true }),
      })
    })

    this.getUserAccountDetails();
    this.getLastLoginDetailsApi();
  }

  public getUserAccountDetails(): void {
    this._toasterService.clear();
    this._accountService.getUserAccountDetails().subscribe(
      (response: UserAccountDetailResponse) => {
        if (response.isSuccess) {
          this.userAccountDetails = response.result[0];
          this.updatedUserDetails=response.result[0];
          this.bindUserAccountForm(this.userAccountDetails);
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

  public updateAccountDetails(myAccountFormValue: any): void {
    this._toasterService.clear();
    const userAccountDetail = { ...myAccountFormValue };
    if(userAccountDetail.accountDetails.contactNumber.length>0){
    if (userAccountDetail.accountDetails.contactNumber.length < 10) {
      this._toasterService.errorMessage("Please provide a valid mobile number.");
      return;
    }

    var plusCount= (userAccountDetail.accountDetails.contactNumber.match(/[+]/g));
    var plusposition= userAccountDetail.accountDetails.contactNumber.indexOf('+');
    
    if(plusCount!=null){
    if (plusCount.length  > 1) {
      this._toasterService.errorMessage("Please provide a valid mobile number."); 
      return;
    }
  }

    if (plusposition  >0) {
      this._toasterService.errorMessage("Please provide a valid mobile number."); 
      return;
    }

    if (userAccountDetail.accountDetails.contactNumber.indexOf('+')  > 0) {
      this._toasterService.errorMessage("Please provide a valid mobile number.");
      return;
    }
  }

    this.holidayListArray = [];
    this.managerArray = [];
    this.isTeamManagerArray = [];
    this.teamArray = [];
    this.roleArray = [];
    this.regionArray = [];
    this.lobArray = [];
    let region = [];
    let lob = [];

    this.userBusinessDetails.mobile=(userAccountDetail.accountDetails.contactNumber.length<=0?UnknownMobileNumber.NA:userAccountDetail.accountDetails.contactNumber);
    if (this.userAccountDetails.region && this.userAccountDetails.region.length) {
      this.userAccountDetails.region.forEach(element => {
        region.push(parseInt(element.regionId));
      });
    }
    else {
      region = [];
    }

    if (this.userAccountDetails.lob && this.userAccountDetails.lob.length) {
      this.userAccountDetails.lob.forEach(element => {
        lob.push(parseInt(element.lobId));
      });
    }
    else {
      lob = [];
    }
    var timestamp = new Date().getTime();
    this.isTeamManagerArray.push(String(this.userAccountDetails.isTeamManager == UserManagerRequired.Yes ? 'false' : 'true'));
    if (this.userAccountDetails.manager == null)
      this.managerArray.push('');
    else
      this.managerArray.push(String(this.userAccountDetails.manager[0].managerId));
    if (region && region.length) {
      this.regionArray = region.map(num => {
        return String(num);
      });
    }
    else {
      this.regionArray = [];
    }
    if (lob && lob.length) {
      this.lobArray = lob.map(num => {
        return String(num);
      });
    }
    else {
      this.lobArray = [];
    }

    if (this.userAccountDetails.holidayList && this.userAccountDetails.holidayList.length) {
      this.holidayListArray = this.userAccountDetails.holidayList.map(num => {
        return String(num.holidayListId);
      });
    }
    else {
      this.holidayListArray = [];
    }

    if (this.userAccountDetails.manager && this.userAccountDetails.manager.length) {
      this.managerArray = this.userAccountDetails.manager.map(num => {
        return String(num.managerId);
      });
    }
    else {
      this.managerArray = [];
    }


    if (this.userAccountDetails.team && this.userAccountDetails.team.length) {
      this.teamArray = this.userAccountDetails.team.map(num => {
        return String(num.teamId);
      });
    }
    else {
      this.teamArray = [];
    }


    if (this.userAccountDetails.role && this.userAccountDetails.role.length) {
      this.roleArray = this.userAccountDetails.role.map(num => {
        return String(num);
      });
    }
    else {
      this.roleArray = [];
    }

    const userMobile = {
      mobile:(userAccountDetail.accountDetails.contactNumber.length<=0?UnknownMobileNumber.NA:userAccountDetail.accountDetails.contactNumber)
    };
    this.updateUserDetail = {
      id: this.userAccountDetails.id,
      createdTimestamp: timestamp,
      userName: this.userAccountDetails.userName,
      enabled: this.userAccountDetails.enabled,
      totp: false,
      emailVerified: true,
      firstName: this.userAccountDetails.firstName,
      lastName: this.userAccountDetails.lastName,
      email: this.userAccountDetails.email,
      attributes: {
        holidayListId: this.holidayListArray,
        managerId: this.managerArray,
        isTeamManager: this.isTeamManagerArray,
        regionId: this.regionArray,
        teamId: this.teamArray,
        businessDetails:[JSON.stringify(this.userBusinessDetails)],
        lobId: this.lobArray,
        role: this.roleArray
      },
      disableableCredentialTypes: [],
      requiredActions: [],
      notBefore: 0,
      access: {
        manageGroupMembership: true,
        view: true,
        mapRoles: true,
        impersonate: true,
        manage: true
      },
      realmRoles: this.roleArray
    };

    this._organizationUserManagementService.updateUser(this.updateUserDetail).subscribe(response => {
      if (response.isSuccess) {
         this.updatedUserDetails=response.result;
        this._toasterService.successMessage('Successfully Completed.');
      } else {
        this._toasterService.errorMessage(response.message);
      }
    },
      error => {
        this._toasterService.errorMessage('An error has occurred.')
      }
    );
  }

  public bindUserAccountForm(data: UserAccountItem): void {
    var roleName: string;
    var teamName: string;
    var managerName: string;
    var businessDetails: string;
    data.role.forEach(x => { roleName = x.toString(); })
    data.team.forEach(x => { teamName = x.teamName; })
    data.manager.forEach(x => { managerName = x.managerName; })
    data.businessDetails.forEach(x => { businessDetails = x; })
    this.userBusinessDetails =JSON.parse(businessDetails);
    this.myAccountForm.patchValue({
      accountDetails: ({
        userId: data.userName,
        firstName: data.firstName,
        lastName: data.lastName,
        emailId: data.email,
        contactNumber: (!!this.userBusinessDetails.mobile ? (this.userBusinessDetails.mobile==UnknownMobileNumber.NA?UnknownMobileNumber.BLANK:this.userBusinessDetails.mobile) : UnknownMobileNumber.BLANK),
      }),
      organisationDetails: ({
        role: (!!roleName ? roleName : ''),
        team: (!!teamName ? teamName : ''),
        reportingManager: (!!managerName ? managerName : ''),
      })
    })
  }

  public getLastLoginDetailsApi(): void {
    this._toasterService.clear();
    this._accountService.getLastLoginDetailsApi().subscribe(
      (response: LastSessionResponse) => {
        if (response.isSuccess) {
          this.lastLogginIn = response.result;
          if (this.lastLogginIn) {
            this.timestamp = this.lastLogginIn.time;
          }
          else {
            this._toasterService.errorMessage(response.message);
          }
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

  public discardChanges(): void {
    var businessDetails: string;
    if (!!this.updatedUserDetails.attributes) {
      this.updatedUserDetails.attributes.businessDetails.forEach(x => { businessDetails = x; });
    }
    else {
      this.updatedUserDetails.businessDetails.forEach(x => { businessDetails = x; });
    }
    this.myAccountForm.patchValue({
      accountDetails: ({
        userId: this.updatedUserDetails.userName,
        firstName: this.updatedUserDetails.firstName,
        lastName: this.updatedUserDetails.lastName,
        emailId: this.updatedUserDetails.email,
        contactNumber: (!!this.userBusinessDetails.mobile ? (this.userBusinessDetails.mobile==UnknownMobileNumber.NA?UnknownMobileNumber.BLANK:this.userBusinessDetails.mobile) : UnknownMobileNumber.BLANK),
      }) 
    });
  }
}
