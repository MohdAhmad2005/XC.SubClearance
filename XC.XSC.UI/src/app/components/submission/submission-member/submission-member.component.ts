import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalDialogComponent } from 'src/app/common/modal-dialog/modal-dialog.component';
import { Submissions } from 'src/app/models/submission/submissionlist';
import { SubmissionService } from 'src/app/Services/submission/submission.service';
import { ToasterService } from '../../../common/toaster/toaster.service';

@Component({
  selector: 'app-submission-member',
  templateUrl: './submission-member.component.html',
  styleUrls: ['./submission-member.component.css']
})
export class SubmissionMemberComponent implements OnInit {

  public submissionId:number;
  public submissionDetails: Submissions;
  public caseNumber: string;

  constructor(private _routeParams: ActivatedRoute,
    private _submissionService: SubmissionService,
    private _modalService: NgbModal,
    private _toasterService: ToasterService) { }

  public enableDataValidation=true;
  public enableClearanceSanction=false;
  public enableGeneralInfo = false;

  onDataValidationClick() {
    this.enableDataValidation=true;
    this.enableClearanceSanction=false;
    this.enableGeneralInfo = false;
  }

  onSanctionClearanceClick() {
    this.enableDataValidation=false;
    this.enableClearanceSanction=true;
    this.enableGeneralInfo=false;
  }

  onGeneralInfoClick() {
    this.enableDataValidation=false;
    this.enableClearanceSanction=false;
    this.enableGeneralInfo = true;
    $('#home-tab').removeClass('nav-link nav-link active').addClass('nav-link');
    $('#profile-tab').removeClass('nav-link nav-link active').addClass('nav-link');
    $('#contact-tab').removeClass('nav-link nav-link active').addClass('nav-link active');
  }

  ngOnInit(): void {
    this._routeParams.queryParams.subscribe(params => {
      this.submissionId = params['submissionId']
    });
    this.getInScopeSubmissionById();
  }
  
  private getInScopeSubmissionById() {
      this._submissionService.getInScopeSubmissionById(this.submissionId).subscribe(response => {
        if (response.isSuccess) {
          this.submissionDetails = response.result as Submissions;
          this.caseNumber = response.result.caseNumber;
        }
        else {
          this._toasterService.errorMessage(response.message);
          this.submissionDetails = {} as Submissions;
        }
      },
        error => {
          this._toasterService.warningMessage(error.message);
          this.submissionDetails = {} as Submissions;
        });
  }
}
