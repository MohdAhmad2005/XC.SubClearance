import { Component, OnInit } from '@angular/core';
import { SortOrder } from 'src/app/models/config/genricGridSetting';
import { TabDirective } from 'ngx-bootstrap/tabs';
import { SubmissionBindingType } from 'src/app/enum/submission/submission-binding-type';
import { ITab } from 'src/app/models/ITab';
import { SubmissionSortFields } from 'src/app/models/submission/submissionlist';
import { Subject } from 'rxjs';


@Component({
  selector: 'app-landing-dashboard',
  templateUrl: './landing-dashboard.component.html',
  styleUrls: ['./landing-dashboard.component.css']
})
export class LandingDashboardComponent implements OnInit {

  public submissionControlsVisibility: any;
  public submissionBindingType: SubmissionBindingType;
  public tabs: ITab[];
  public SubmissionFilter: any;
  public isActionVisible: boolean = true;
  public region: number;
  public lob: number;
  public defaultSortOrder: SortOrder;
  public defaultSortField: SubmissionSortFields;
  public reloadEmailAutomation: Subject<any> = new Subject();

  constructor() { }

  ngOnInit(): void {

    this.submissionControlsVisibility = {
      isInsuredNameVisble: true,
      isBrokerNameVisble: true,
      isReceivedDateVisble: true,
      isDueDateVisble: true,
      isAssignedToVisble: false,
      isStatusVisble: true,
      isActionVisble: false,
      isAsignedToSelfVisible: false
    }
    this.submissionBindingType = SubmissionBindingType.MySubmission;
    this.tabs = [
      { title: 'My Submission', removable: false, disabled: false, active: true, customClass: 'mb-1' },
      { title: 'New Submission', removable: false, disabled: false, active: false, customClass: 'mb-1' },
    ];
  }

  public createSubmissionFilter(): void {
    this.SubmissionFilter = {
      isInScope: true,
      isMySubmission: (this.submissionBindingType == SubmissionBindingType.MySubmission) ? true : false,
      isNewSubmission: (this.submissionBindingType == SubmissionBindingType.NewSubmission) ? true : false
    }
  }

  public onTabSelect(data: TabDirective, index): void {
    if (index == 0) {
      this.submissionControlsVisibility = null;
      this.submissionBindingType = null;
      this.submissionControlsVisibility = {
        isInsuredNameVisble: true,
        isBrokerNameVisble: true,
        isReceivedDateVisble: true,
        isDueDateVisble: true,
        isAssignedToVisble: false,
        isStatusVisble: true,
        isActionVisble: false,
        isAsignedToSelfVisible: false
      }
      this.submissionBindingType = SubmissionBindingType.MySubmission;
      this.isActionVisible = true;
      this.createSubmissionFilter();

    }
    else {
      this.defaultSortOrder = SortOrder.Descending;
      this.defaultSortField = SubmissionSortFields.DueDate;

      this.submissionControlsVisibility = null;
      this.submissionBindingType = null;
      this.submissionControlsVisibility = {
        isInsuredNameVisble: true,
        isBrokerNameVisble: true,
        isReceivedDateVisble: true,
        isDueDateVisble: true,
        isAssignedToVisble: false,
        isStatusVisble: false,
        isActionVisble: false,
        isAsignedToSelfVisible: true
      }
      this.submissionBindingType = SubmissionBindingType.NewSubmission;
      this.isActionVisible = false;
      this.createSubmissionFilter();

    }
  }

  public changeRegionOrLob(isDashboardLoadedFirstTime: boolean): void {
    let val = {
      refreshControl:true,
      isLoadedFirstTime: isDashboardLoadedFirstTime
    }
    this.reloadEmailAutomation.next(val);
    this.createSubmissionFilter();
  }
}
