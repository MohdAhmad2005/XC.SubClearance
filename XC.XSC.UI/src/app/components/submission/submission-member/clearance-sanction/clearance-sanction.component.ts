import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToasterService } from '../../../../common/toaster/toaster.service';
import { CommentsClearance } from '../../../../models/submission/Comments';
import { SubmissionClearancesResponse } from '../../../../models/submission/submissionlist';
import { SubmissionService } from '../../../../Services/submission/submission.service';

@Component({
  selector: 'app-clearance-sanction',
  templateUrl: './clearance-sanction.component.html',
  styleUrls: ['./clearance-sanction.component.css']
})
export class ClearanceSanctionComponent implements OnInit {

  private submissionId: number;
  public commentsClearance: CommentsClearance
  public submissionClearancesResponse: SubmissionClearancesResponse[];
  public commentsClearanceformGroup: FormGroup;
  public isConfrimAlertVisible: boolean = false;
  public confirmationMessage: string;
  @Output() ClearanceSanction: EventEmitter<void> = new EventEmitter();

  constructor(
    private _routeParams: ActivatedRoute,
    private _submissionService: SubmissionService,
    private _toasterService: ToasterService,
    private _router: Router,
  ) {
  }

  ngOnInit(): void {
    this._routeParams.queryParams.subscribe(params => {
      this.submissionId = params['submissionId']
    });

    this.getSubmissionClearances();

    this.commentsClearanceformGroup = new FormGroup({
      remark: new FormControl('', Validators.required),
      clearanceConscent: new FormControl('', Validators.required)
    });
  }

  private getSubmissionClearances(): void {
    try {
      this._submissionService.getSubmissionClearances(this.submissionId).subscribe(response => {
        if (response.isSuccess) {
          this.submissionClearancesResponse = response.result as SubmissionClearancesResponse[];
        }
        else {
          this.submissionClearancesResponse = [] as SubmissionClearancesResponse[];
        }
      },
        error => {
          this.submissionClearancesResponse = [] as SubmissionClearancesResponse[];
        });
    } catch (error) {
      this.submissionClearancesResponse = [] as SubmissionClearancesResponse[];
    }
  }

  public addClearanceComment(): void {    
    const commentsClearance: CommentsClearance = {
      commentText: this.commentsClearanceformGroup.get('remark').value,
      submissionId: this.submissionId,
      clearanceConscent: this.commentsClearanceformGroup.get('clearanceConscent').value,
    };
    this._submissionService.addClearanceComment(commentsClearance).subscribe(response => {
      if (response.isSuccess) {
        this._toasterService.successMessage('Completed sanction screening and the account can be cleared.');
        this.ClearanceSanction.emit();
      }
      else {
        this._toasterService.errorMessage(response.message);
      }
    },
      error => {
        this._toasterService.errorMessage('Session is time out.');
      });
  }

  public SaveExit(): void {
    this.isConfrimAlertVisible = true;
    this.confirmationMessage = "Are you sure you want to save & exit?";   
  }

  public confirmationClick(event) {
    if (!!event) {
      if (event.isConfirmed) {
        const commentsClearance: CommentsClearance = {          
          commentText: this.commentsClearanceformGroup.get('remark').value,
          submissionId: this.submissionId,
          clearanceConscent: this.commentsClearanceformGroup.get('clearanceConscent').value,
        };
        this._submissionService.addClearanceComment(commentsClearance).subscribe(response => {
          if (response.isSuccess) {
            this._toasterService.successMessage('Completed sanction screening and the account can be cleared.');
            this._router.navigate(["Submissions"]);
          }
          else {
            this._toasterService.errorMessage(response.message);
          }
        },
          error => {
            this._toasterService.errorMessage(error);
          });
      }
      this.isConfrimAlertVisible = false;
    }
  }
}
