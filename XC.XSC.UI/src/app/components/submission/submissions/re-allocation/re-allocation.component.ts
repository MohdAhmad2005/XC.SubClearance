import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToasterService } from 'src/app/common/toaster/toaster.service';
import { ActionReferenceType, AssignSubmissionToUserRequest } from 'src/app/models/submission/submissionlist';
import { SubmissionService } from 'src/app/Services/submission/submission.service';

@Component({
  selector: 'app-re-allocation',
  templateUrl: './re-allocation.component.html',
  styleUrls: ['./re-allocation.component.css']
})
export class ReAllocationComponent implements OnInit {

  @Input() submissionId: number;
  public userInfo: any[] = [];
  public usersListByFilter: any;
  public region: number;
  public team: number;
  public lob: number;
  public confirmationMessage: string;
  public isConfirmAlertVisible: boolean;
  public userId: string;
  public userName: any[] = [];
  public assignSubmissionToUser: AssignSubmissionToUserRequest;
  public actionReference: ActionReferenceType;
  public buttonDisabled: boolean;


  @ViewChild('usersListValue') UsersListValue: ElementRef;

  constructor(private _activeModal: NgbActiveModal,
    private _submissionService: SubmissionService,
    private _toasterService: ToasterService,) { }

  ngOnInit(): void {
    this.userInfo = [this._submissionService.allUserInfo.caseNumber,
    this._submissionService.allUserInfo.insuredName,
    this._submissionService.allUserInfo.assignedTo];
    this.lob = this._submissionService.allUserInfo.lobId;
    this.team = this._submissionService.allUserInfo.teamId;
    this.region = this._submissionService.allUserInfo.regionId;
    this.getUsersByFilter();
  }

  public getUsersByFilter(): void {
    this.usersListByFilter = this._submissionService.assignToUserList;
    this.usersListByFilter = this.usersListByFilter.filter((element) => element.lob.find((x) => x.lobId == this.lob) && element.region.find((y)=> y.regionId== this.region) && element.team.find((z)=>z.teamId == this.team));
  }

  public userListChangeEvent(event: any): void {
    let selectedUserListValue = this.usersListByFilter.filter(x => x.id == event.target.value);
    if(selectedUserListValue.length<=0){
      this.buttonDisabled = false;
    }else{
    this.assignSubmissionToUser = {
      submissionId: this.submissionId,
      userId: event.target.value
    }
    this.buttonDisabled = true;
  }
}

  public submitClick(): void {
    this._toasterService.clear();
    this.userName = this.usersListByFilter.filter(x => x.id == this.assignSubmissionToUser.userId);
    this.isConfirmAlertVisible = true;
    this.actionReference = ActionReferenceType.ReAllocation;
    this.confirmationMessage = "Are you sure you want to assign " + this._submissionService.allUserInfo.caseNumber + " to " + this.userName[0].firstName + ' '+ this.userName[0].lastName;
  }

  public confirmationClick(event: any): void {
    if (!!event) {
      if (event.isConfirmed) {
        if (event.referenceType == ActionReferenceType.ReAllocation) {
          this._submissionService.assignSubmissionToUser(this.assignSubmissionToUser).subscribe(response => {
            if (response.isSuccess) {
              this.closeModal(ActionReferenceType.ReAllocation);
              this._toasterService.successMessage(this._submissionService.allUserInfo.caseNumber + " has been assigned to " + this.userName[0].firstName + ' '+ this.userName[0].lastName);
            }
            else {
              this._toasterService.errorMessage(response.message);
              this.isConfirmAlertVisible = false;
            }
          },
            error => {
              this.isConfirmAlertVisible = false;
            });
        }
      }
      else {
        this.closeModal(ActionReferenceType.ReAllocation);
        this.isConfirmAlertVisible = false;
      }
    }
  }

  public closeModal(event: ActionReferenceType): void {
    this._activeModal.close(event);
  }

  public resetList(): void {
    this.UsersListValue.nativeElement.value = '';
    this.buttonDisabled = false;
  }
}
