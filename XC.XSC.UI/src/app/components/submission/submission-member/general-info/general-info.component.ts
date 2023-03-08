import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal} from '@ng-bootstrap/ng-bootstrap';
import { ReviewType } from 'src/app/enum/review-type';
import { CommentType } from 'src/app/enum/submission/commentType';
import { UserRoleStatus } from 'src/app/enum/submission/submission-edit-general-info';
import { SubmissionStatus } from 'src/app/enum/submission/submission-status';
import { UserAccountDetailResponse, UserAccountItem } from 'src/app/models/account/user-account';
import { IReviewConfigurationResponse } from 'src/app/models/review-management/review-management';
import { Comments } from 'src/app/models/submission/Comments';
import { ReviewReply, ReviewSubmit } from 'src/app/models/submission/submission-member/review-submit';
import { ActionReferenceType, Submissions } from 'src/app/models/submission/submissionlist';
import { AccountService } from 'src/app/Services/account/account.service';
import { ReviewManagementService } from 'src/app/Services/review-management/review-management.service';
import { RegularExpressionServiceService } from 'src/services/regular-expression-service.service';
import { Formconstant } from '../../../../common/constant/constant';
import { QuestionControlService } from '../../../../common/dynamic-form/question-control.service';
import { ToasterService } from '../../../../common/toaster/toaster.service';
import { SubmissionService } from '../../../../Services/submission/submission.service';

@Component({
  selector: 'app-general-info',
  templateUrl: './general-info.component.html',
  styleUrls: ['./general-info.component.css']
})
export class GeneralInfoComponent implements OnInit {

  public reviewForm: FormGroup;
  public pasForm: FormGroup;
  public formData: any;
  public generalInfoData: any;
  public pasControls: any[];
  public pasHeader: string;
  public reviewControls: any[];
  public reviewHeader: string;
  public generalInfoTableHeader: string;
  public columnDefination = [];
  public tableContent: any;
  public isConfrimAlertVisible: boolean = false;
  public actionReference: ActionReferenceType;
  private submissionId: number;
  private urlActionType: string;
  private reviewDetails: IReviewConfigurationResponse;
  public confirmationMessage: string;
  public userAccountDetails: UserAccountItem;
  public submissionDetails: Submissions;
  public isPasButtonVisible:boolean=false;;
  public isReviewButtonDisable:boolean=true;;
  public isPasButtonDisable:boolean=true;;
  
  public loggedInUserRole: string;
  public isReviewButtonVisible: boolean;
  public isProcessorButtonVisible: boolean;
  public isSaveAndExitButtonDisable: boolean = true;  
  public isProcessorButtonDisable: boolean;
  public caseNumber: string;

  @Input() set CaseNumber(value: any) {
    if (!!value) {
      this.caseNumber = value;
    }
  }

  constructor(public _modalService: NgbModal,
    private _controlService: QuestionControlService,
    private _submissionService: SubmissionService,
    private _accountService: AccountService,
    private _toasterService: ToasterService,
    private _routeParams: ActivatedRoute,
    private _router: Router,
    private _reviewService: ReviewManagementService,
    private _regularExpressionService: RegularExpressionServiceService) { }

  ngOnInit(): void {    
    this._routeParams.queryParams.subscribe(params => {
      this.submissionId = params['submissionId'];
      this.urlActionType = params['action'];
    });
    
    this.getColumnDefinition();
    this.formData = Formconstant.generalInformationForm;
    this.getFormData();

    this.getUserAccountDetails();
  }

  public getFormData(): void {
    this._controlService.getFormData(this.formData).then(item => {
      this.pasControls = item.response[0].pasResponse;
      this.pasHeader = item.response[0].pasHeader;
      this.reviewControls = item.response[0].reviewResponse;
      this.reviewHeader = item.response[0].reviewHeader;
      this.generalInfoTableHeader = item.response[0].generalInfoTableHeader;

      this.pasForm = this._controlService.toFormGroup(this.pasControls);
      this.reviewForm = this._controlService.toFormGroup(this.reviewControls);
      this.formChange();
    });
  }

  public bindPasReviewFormValues(data: any): void {
    let reviewFormatData = this._controlService.setDateFormate(data.reviewInformation, this.reviewControls);
    this.reviewForm.patchValue(reviewFormatData);

    const commentBoxReadOnly = this.reviewControls.find(x => x.key == "comment");   
    const reviewStatusDropdownReadOnly = this.reviewControls.find(x => x.key == "reviewStatus"); 
    if(this.urlActionType == ActionReferenceType.EditSubmission){
      commentBoxReadOnly.readOnly = false;
    }
    else{
      commentBoxReadOnly.readOnly = true;
      reviewStatusDropdownReadOnly.readOnly = true;
      this.isPasButtonDisable = true;
      this.isReviewButtonDisable = true;
      this.isProcessorButtonDisable = true;
      this.isSaveAndExitButtonDisable = true;
    }
  }

  public getColumnDefinition(): void {
    this.columnDefination.push({ header: 'Date', field: 'commentDate', type: 'date', sortKey: 'commentDate' },);
    this.columnDefination.push({ header: 'Description', field: 'description', sortKey: 'description' },);
    this.columnDefination.push({ header: 'Comment By', field: 'commentBy', sortKey: 'commentBy' },);
  }

  public getTableContent(tableList: any): void {
    if (!!tableList) {
      this.tableContent = tableList;
    }
    else {
      this.tableContent = null;
    }
  }

  public saveAndExit(): void {
    this._toasterService.clear();
    const comments: Comments = {
      CommentType: CommentType.Review,
      CommentText: this.reviewForm.value.comment,
      SubmissionId: Number(this.submissionId),
      JsonData: ''
    };    
    const commentBoxValue = this.reviewForm.value.comment;

    if(this._regularExpressionService.specialCharactersAndNumeric.test(commentBoxValue)){
      this._toasterService.errorMessage("Please enter valid input and not just special characters");
      return;
    }
    else{
      this._submissionService.saveSubmissionComment(comments).subscribe(response => {
        if (response.isSuccess) {
          this._toasterService.successMessage("Successfully added comment.");
          this._router.navigate(['/Submissions']);
        }
        else {
          this._toasterService.errorMessage(response.message);
        }
      },
        error => {
        });
    }
  }

  public submitForReview(): void {
    this._toasterService.clear();    
    const commentBoxValue = this.reviewForm.value.comment;

    if(this._regularExpressionService.specialCharactersAndNumeric.test(commentBoxValue)){
      this._toasterService.errorMessage("Please enter valid input and not just special characters");
      return;
    }
    else{
      this.isConfrimAlertVisible = true;
      this.actionReference = ActionReferenceType.SubmitReview;
      this.confirmationMessage = `Are you sure you want to submit the Case Number ${this.caseNumber} for Review`;
    }

  }

  public submitForPAS(): void {
    this._toasterService.clear();    
    const commentBoxValue = this.reviewForm.value.comment;

    if(this._regularExpressionService.specialCharactersAndNumeric.test(commentBoxValue)){
      this._toasterService.errorMessage("Please enter valid input and not just special characters");
      return;
    }
    else{
      this.isConfrimAlertVisible = true;
      this.actionReference = ActionReferenceType.SubmitforPAS;
      this.confirmationMessage = `Are you sure you want to submit the Case Number ${this.caseNumber} to PAS`;
    }
  }

  public sendBackToProcessor(): void {
    this._toasterService.clear();    
    const commentBoxValue = this.reviewForm.value.comment;
    const reviewStatusValue = this.reviewForm.get('reviewStatus').value;

    if(this._regularExpressionService.specialCharactersAndNumeric.test(commentBoxValue)){
      this._toasterService.errorMessage("Please enter valid input and not just special characters");
      return;
    }
    else{
      if(reviewStatusValue == SubmissionStatus.ReviewPass){
        this.isConfrimAlertVisible = true;
        this.actionReference = ActionReferenceType.SendBackToProcessor;
        this.confirmationMessage = `Are you sure this case has passed the review?`;
      }
      else{
        this.isConfrimAlertVisible = true;
        this.actionReference = ActionReferenceType.SendBackToProcessor;
        this.confirmationMessage = `Are you sure this case has failed the review?`;
      }
    }

  }

  public getSubmissionGeneralInformation(submissionId: number): void {    
    this._submissionService.getSubmissionGeneralInformation(submissionId).subscribe(response => {
      if (response.isSuccess) {        
        this.generalInfoData = response.result;

        this.bindPasReviewFormValues(this.generalInfoData);
        this.getTableContent(response.result.reviewInformation.comments);
        
        if (!(this.reviewForm.get('reviewStatus').value == SubmissionStatus.ReviewPass
        || this.reviewForm.get('reviewStatus').value == SubmissionStatus.ReviewFail)) {

        this.reviewForm.get('reviewStatus').setValue(null);        
      }

      }
      else {
        this._toasterService.errorMessage(response.message);
      }
    },
      error => {

      });
  };

  public getUserAccountDetails(): void {
    this._toasterService.clear();
    this._accountService.getUserAccountDetails().subscribe(
      (response: UserAccountDetailResponse) => {
        if (response.isSuccess) {
          this.userAccountDetails = response.result[0];
          this.loggedInUserRole = this.userAccountDetails.role.toString();
          this.getSubmissionGeneralInformation(this.submissionId);
          this.getInScopeSubmissionById();
          this.hideOrShowProcessorButton();

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

  private getReviewDetails(): void {
    const filter = { "userId": true }
    this._reviewService.getReviewConfig(filter).subscribe(response => {
      if (response.isSuccess) {
        this.reviewDetails = response.result;
        this.hideOrShowPasButton();
        this.hideOrShowReviewButton();
        this.hideOrShowProcessorButton();
        this.enableOrDisableReviewButton();
        this.enableOrDisablePasButton();
        this.enableOrDisableProcessorButton();
      }
      else {
        this._toasterService.errorMessage(response.message);
      }
    },
      error => {
      });
  }

  private hideOrShowPasButton(): boolean {
    const reviewStatusValue = this.reviewForm.get('reviewStatus').value;
    if (this.reviewDetails == undefined || this.submissionDetails == undefined
      || this.reviewDetails == null || this.submissionDetails == null) {
      this.isPasButtonVisible = false;
    }
    else if (this.reviewDetails.reviewTypeId == ReviewType.SkipReview &&
      this.submissionDetails.statusId != SubmissionStatus.SubmittedtoPAS && this.loggedInUserRole == UserRoleStatus.Processor
      && reviewStatusValue == SubmissionStatus.ReviewPass) {
      this.isPasButtonVisible = true;
    }
    else if (this.reviewDetails.reviewTypeId == ReviewType.CompulsaryReview &&
      this.submissionDetails.statusId != SubmissionStatus.ReviewPass) {
      this.isPasButtonVisible = false;
    }
    else if (this.reviewDetails.reviewTypeId == ReviewType.CompulsaryReview &&
      this.submissionDetails.statusId == SubmissionStatus.ReviewPass && this.loggedInUserRole == UserRoleStatus.Processor) {
      this.isPasButtonVisible = true;
    }
    else {
      return false;
    }
  }

  private enableOrDisablePasButton(): void {
    const commentValue = (!!this.reviewForm.get('comment').value) ? this.reviewForm.get('comment').value.trim().length : this.reviewForm.get('comment').value;    
    const reviewStatusValue = this.reviewForm.get('reviewStatus').value;
    if (this.submissionDetails == undefined || this.submissionDetails == null) {
      this.isPasButtonDisable = true;
    }
    else if (this.submissionDetails.clearanceConsent && reviewStatusValue == SubmissionStatus.ReviewPass 
      && this.submissionDetails.isDataCompleted && this.loggedInUserRole == UserRoleStatus.Processor 
      && this.urlActionType == ActionReferenceType.EditSubmission && (!!commentValue || commentValue > 0)) {      
      this.isPasButtonDisable = false;
    }
    else {
      this.isPasButtonDisable = true;
    }
  }

  private hideOrShowReviewButton(): void {
    if (this.loggedInUserRole == UserRoleStatus.Processor) {
      this.isReviewButtonVisible = true;
    }
    else {
      this.isReviewButtonVisible = false;
    }
  }

  private hideOrShowProcessorButton(): void {
    const reviewStatusDropdownReadOnly = this.reviewControls.find(x => x.key == "reviewStatus");
    if (this.loggedInUserRole == UserRoleStatus.Reviewer) {
      reviewStatusDropdownReadOnly.readOnly = false;
      this.isProcessorButtonVisible = true;
    }
    else {
      this.isProcessorButtonVisible = false;
    }
  }

  private enableOrDisableSaveAndExitButton(): void {
    const commentValue = (!!this.reviewForm.get('comment').value) ? this.reviewForm.get('comment').value.trim().length : this.reviewForm.get('comment').value;
    if(!!commentValue || commentValue > 0){
      this.isSaveAndExitButtonDisable = false;
    }
    else{
      this.isSaveAndExitButtonDisable = true;
    }
  }

  private enableOrDisableReviewButton(): void {
    const commentValue = (!!this.reviewForm.get('comment').value) ? this.reviewForm.get('comment').value.trim().length : this.reviewForm.get('comment').value;
    const reviewStatusValue = this.reviewForm.get('reviewStatus').value;
    if (this.submissionDetails == undefined || this.reviewDetails == undefined
      || this.reviewDetails == null || this.submissionDetails == null) {
      this.isReviewButtonDisable = true;
    }
    else if (this.submissionDetails.statusId == SubmissionStatus.ReviewPending || this.submissionDetails.statusId == SubmissionStatus.UnderReviewPlay
      || this.submissionDetails.statusId == SubmissionStatus.UnderReviewPaused || this.submissionDetails.statusId == SubmissionStatus.ReviewPass) {
      this.isReviewButtonDisable = true;
    }
    else if (this.submissionDetails.clearanceConsent && (!!commentValue || commentValue > 0) 
    && this.submissionDetails.isDataCompleted && reviewStatusValue != SubmissionStatus.ReviewPass) {
      this.isReviewButtonDisable = false;
    }
    else {
      this.isReviewButtonDisable = true;
    }
  }

  private enableOrDisableProcessorButton(): void {
    const commentValue = (!!this.reviewForm.get('comment').value) ? this.reviewForm.get('comment').value.trim().length : this.reviewForm.get('comment').value;
    const reviewStatusValue = this.reviewForm.get('reviewStatus').value;
    if (this.submissionDetails == undefined || this.submissionDetails == null) {
      this.isProcessorButtonDisable = true;
    }
    else if (this.submissionDetails.clearanceConsent && (!!commentValue || commentValue > 0) && (reviewStatusValue == SubmissionStatus.ReviewPass
      || reviewStatusValue == SubmissionStatus.ReviewFail) && this.submissionDetails.isDataCompleted) {
      this.isProcessorButtonDisable = false;
    }
    else if (this.submissionDetails.statusId == SubmissionStatus.ReviewPass) {
      this.isProcessorButtonDisable = true;
    }
    else {
      this.isProcessorButtonDisable = true;
    }
  }

  public confirmationClick(event): void {
    if (!!event) {
      if (event.isConfirmed) {
        if (event.referenceType == ActionReferenceType.SubmitReview) {
          const comments: ReviewSubmit = {
            commentType: CommentType.Review,
            commentText: this.reviewForm.value.comment,
            submissionId: Number(this.submissionId),
            jsonData: ''
          };
          this._submissionService.submitForReview(comments).subscribe(response => {
            if (response.isSuccess) {
              this._toasterService.successMessage(`Case Number ${this.caseNumber} has been submitted for Review`);              
              this.isConfrimAlertVisible = false;
              this._router.navigate(['/Submissions']);
            }
            else {
              this._toasterService.errorMessage(response.message);
              this.isConfrimAlertVisible = false;
            }
          },
            error => {
              this.isConfrimAlertVisible = false;
            });
        }
        else {
          this.isConfrimAlertVisible = false;
        };

        if (event.referenceType == ActionReferenceType.SubmitforPAS) {
          this._submissionService.submitForPAS("").subscribe(response => {
            if (response.isSuccess) {
              this._toasterService.successMessage(`Case Number ${this.caseNumber} has been submitted to PAS`);
              this.isConfrimAlertVisible = false;
            }
            else {
              this.isConfrimAlertVisible = false;
              this._toasterService.errorMessage(response.message);

            }
          },
            error => {
              this.isConfrimAlertVisible = false;
            });
        }
        else {
          this.isConfrimAlertVisible = false;
        };

        if (event.referenceType == ActionReferenceType.SendBackToProcessor) {
          const comments: ReviewReply = {
            commentType: CommentType.Review,
            commentText: this.reviewForm.value.comment,
            submissionId: Number(this.submissionId),
            jsonData: '',
            reviewStatus: this.reviewForm.value.reviewStatus
          };
          this._submissionService.sendBackToProcessor(comments).subscribe(response => {
            if (response.isSuccess) {
              this._toasterService.successMessage(`Case Number ${this.caseNumber} has been Sent Back to ${this.generalInfoData.reviewInformation.processorName}`);
              this.isConfrimAlertVisible = false;
              this._router.navigate(['/Submissions']);
            }
            else {
              this.isConfrimAlertVisible = false;
              this._toasterService.errorMessage(response.message);

            }
          },
            error => {
              this.isConfrimAlertVisible = false;
            });
        }
        else {
          this.isConfrimAlertVisible = false;
        };
      }
      else {
        this.isConfrimAlertVisible = false;
      }
    }
  }

  private getInScopeSubmissionById(): void {
    try {
      this._submissionService.getInScopeSubmissionById(this.submissionId).subscribe(response => {
        if (response.isSuccess) {
          this.submissionDetails = response.result as Submissions;

          if (this.loggedInUserRole == UserRoleStatus.Processor) {
            this.getReviewDetails();
          }
          else {
            this.hideOrShowPasButton();
            this.hideOrShowReviewButton();
            this.hideOrShowProcessorButton();
            this.enableOrDisableReviewButton();
            this.enableOrDisablePasButton();
            this.enableOrDisableProcessorButton();
          }
        }
        else {
          this.submissionDetails = {} as Submissions;
        }
      },
        error => {
          this.submissionDetails = {} as Submissions;
        });
    } catch (error) {
      this.submissionDetails = {} as Submissions;
    }
  }

  public formChange(): void {
    this.reviewForm.get('comment').valueChanges.subscribe((selectedValue) => {
      this.enableOrDisableSaveAndExitButton();
      this.enableOrDisablePasButton();
      this.enableOrDisableReviewButton();
      this.enableOrDisableProcessorButton();
    }
    );
    this.reviewForm.get('reviewStatus').valueChanges.subscribe((selectedValue) => {
      this.enableOrDisableProcessorButton();
    }
    );
  }
}
