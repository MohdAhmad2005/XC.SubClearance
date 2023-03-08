import { Component, OnInit } from '@angular/core';
import { SubmissionBindingType } from '../../../enum/submission/submission-binding-type';
import { SubmissionScope } from '../../../enum/submission/submission-scope';

@Component({
  selector: 'app-submissions',
  templateUrl: './submissions.component.html',
  styleUrls: ['./submissions.component.css']
})
export class SubmissionsComponent implements OnInit {

  public submissionFiltersInScope: any;
  public submissionFiltersOutscope: any;
  public submissionListControlsVisibility: any;
  public submissionBindingType: SubmissionBindingType;
  public isOutScopeSubmissionListVisible: boolean = false;
  private scopeList = SubmissionScope;
  public resetOutScopeSubmission: any;
  constructor() { }
  
  ngOnInit(): void {

    this.setSubmissionlistControlVisibility();
    this.submissionBindingType = SubmissionBindingType.SearchSubmission;
  }
  private setSubmissionlistControlVisibility(): void {
    this.submissionListControlsVisibility = {
      isInsuredNameVisble: true,
      isBrokerNameVisble: true,
      isReceivedDateVisble: true,
      isDueDateVisble: true,
      isAssignedToVisble: true,
      isStatusVisble: true,
      isActionVisble: true,
    }
  }

  public getSubmissionScope(submissionScope: number):void {

    if (!!submissionScope && submissionScope == this.scopeList.OutScope) {
      this.isOutScopeSubmissionListVisible = true;
      this.submissionFiltersOutscope = null;
    }
    else {
      this.isOutScopeSubmissionListVisible = false;
      this.submissionFiltersInScope = null;
    }
  }

  public getOutScopeSubmissionFilters(data: object):void {
      this.submissionFiltersOutscope = data;
  }
  public getInScopeSubmissionFilters(data: object):void {
      this.submissionFiltersInScope = data;
  }

  public resetSearchResult():void {
    this.setSubmissionlistControlVisibility();
    if (this.isOutScopeSubmissionListVisible) {
      this.resetOutScopeSubmission = {
        resetFilters:true
      }

    }
  }
}
