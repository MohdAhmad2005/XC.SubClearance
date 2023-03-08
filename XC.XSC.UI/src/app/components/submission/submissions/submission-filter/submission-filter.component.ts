import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { RegularExpressionServiceService } from 'src/services/regular-expression-service.service';
import { SubmissionScope } from 'src/app/enum/submission/submission-scope';
import { SubmissionStatus } from 'src/app/models/submission/submissionlist';
import { SubmissionService } from 'src/app/Services/submission/submission.service';
import { UserService } from 'src/app/Services/uam/user/user.service';
import { DateparserService } from 'src/app/utility/dateparser.service';
import { ToasterService } from '../../../../common/toaster/toaster.service';
import { ActivatedRoute } from '@angular/router';
import { UserFilterModel } from 'src/app/models/uam/filter/user-filter';
import { EnvironmentService } from 'src/services/environment-service.service';

@Component({
  selector: 'app-submission-filter',
  templateUrl: './submission-filter.component.html',
  styleUrls: ['./submission-filter.component.css']
})
export class SubmissionFilterComponent implements OnInit {

  @Output() InScopeSubmissionFilters: EventEmitter<object> = new EventEmitter();
  @Output() OutScopeSubmissionFilters: EventEmitter<object> = new EventEmitter();
  @Output() SubmissionScopeChange: EventEmitter<number> = new EventEmitter();
  @Output() Reset = new EventEmitter();

  public submissionSearch: FormGroup;
  public statusList: Array<SubmissionStatus>;
  public scopeList = SubmissionScope;
  public scopeListKeys = [];
  private submissionScope = this.scopeList.InScope;
  public assignTo: any;
  public isScopeVisible: boolean = true;
  public isCaseNumberVisible: boolean = true;
  public isInsuredNameVisible: boolean = true;
  public isStatusVisible: boolean = true;
  public isBrokerNameVisible: boolean = true;
  public isReceivedDateVisible: boolean = true;
  public isDueDateVisible: boolean = true;
  public isAssignedToVisible: boolean = true;
  public paramMode: boolean = true;
  public queryParam: any;
  public selectedInOutScope: number;
  public paramDateRange: Date[] = [];
  public isValidFilters: boolean = false;
  public status: number;
  public isReset: boolean = false;
  public bsDateRangeConfig: any = { dateInputFormat: this._envService.dateFormat, rangeInputFormat: this._envService.dateFormat,showWeekNumbers: false };

  constructor(private _regularExpressionService: RegularExpressionServiceService,
    private _submissionService: SubmissionService,
    private _dateParserService: DateparserService,
    private _userService: UserService,
    private _toasterService: ToasterService,
    private _activatedRoute: ActivatedRoute,
    private _envService: EnvironmentService
    ) {

    this.scopeListKeys = Object.keys(this.scopeList).filter(f => !isNaN(Number(f)));;
  }

  ngOnInit(): void {
    this.setFilter();
    this.createForm();
  }

  private createForm(): void {
    this.submissionSearch = new FormGroup({
      inOutScope: new FormControl(''),
      caseNumber: new FormControl(''),
      insuredName: new FormControl(''),
      brokerName: new FormControl(''),
      statusList: new FormControl(''),
      assignTo: new FormControl(''),
      receivedDate: new FormControl(),
      dueDate: new FormControl(),
    })

    this.submissionSearch.patchValue({
      inOutScope: this.submissionScope,
    });

    if (!!this.queryParam.scope && this.paramMode == true && this.isReset == false) {
      this.submissionSearch.patchValue({
        inOutScope: this.selectedInOutScope,
        receivedDate: [new Date(this.paramDateRange[0]), new Date(this.paramDateRange[1])],
      });
      this.todaySubmission();
    }

    if (this.submissionSearch.get('inOutScope').value != SubmissionScope.OutScope) {
      this.getStatusAndUserList();
    }
  }

  private getStatusAndUserList() {
    this._submissionService.getSubmissionStatus().subscribe(response => {
      if (!!response.isSuccess) {
        this.statusList = response.result;
        var filteredStatus = this.statusList.filter(x=>x.id == this.status);
        if(filteredStatus && filteredStatus.length){
          this.submissionSearch.get('statusList').setValue(filteredStatus[0].id);
          this.todaySubmission();
        }
      }
      else {
        this._toasterService.errorMessage(response.message);
      }
    },
      error => {
        this._toasterService.warningMessage(error.message);
      });

    const userFilter = <UserFilterModel>{
      attributes: [],
      permissions: []
    };

    this._userService.getUsersByFilters(userFilter).subscribe(response => {
      if (!!response.isSuccess) {
        this.assignTo = response.result;
        this._submissionService.assignToUserList = response.result;
      }
      else {
        this._toasterService.errorMessage(response.message);
      }
    },
      error => {
        this._toasterService.warningMessage(error.message);
      });
  }

  public todaySubmission(): void {
    this._toasterService.clear();
    if (this.submissionScope == this.scopeList.OutScope) {
      if (this.submissionSearch.get('caseNumber').value == '' && (this.submissionSearch.get('receivedDate').value == null || this.submissionSearch.get('receivedDate').value == '')) {
        this._toasterService.errorMessage("Please fill at-least one field.");
        return;
      }
    }
    else if (this.submissionScope == this.scopeList.InScope) {
      if (this.submissionSearch.get('caseNumber').value == '' && this.submissionSearch.get('insuredName').value == '' &&
        this.submissionSearch.get('brokerName').value == '' && this.submissionSearch.get('statusList').value == '' &&
        this.submissionSearch.get('assignTo').value == '' && this.submissionSearch.get('receivedDate').value == null &&
        this.submissionSearch.get('dueDate').value == null) {
        this._toasterService.errorMessage("Please fill at-least one field.");
        return;
      }
    }

    this.isValidFilters = this.validateSubmissionFilters();
    if (this.isValidFilters) {
      const submissionSearchCriteria = {
        isInScope: (this.submissionSearch.get('inOutScope').value == 1) ? true : false,
        caseNumber: this.submissionSearch.get('caseNumber').value,
        insuredName: this.submissionSearch.get('insuredName').value,
        brokerName: this.submissionSearch.get('brokerName').value,
        statusId: this.submissionSearch.get('statusList').value,
        assignedTo: this.submissionSearch.get('assignTo').value,
        receivedFromDate: (!!this.submissionSearch.get('receivedDate').value) ? this._dateParserService.FormatToISOString(this.submissionSearch.get('receivedDate').value[0]) : '',
        receivedToDate: (!!this.submissionSearch.get('receivedDate').value) ? this._dateParserService.FormatToISOString(this.submissionSearch.get('receivedDate').value[1]) : '',
        dueFromDate: (!!this.submissionSearch.get('dueDate').value) ? this._dateParserService.FormatToISOString(this.submissionSearch.get('dueDate').value[0]) : '',
        dueToDate: (!!this.submissionSearch.get('dueDate').value) ? this._dateParserService.FormatToISOString(this.submissionSearch.get('dueDate').value[1]) : ''

      };

      if (this.submissionScope == this.scopeList.OutScope) {
        this.OutScopeSubmissionFilters.emit(submissionSearchCriteria);
      }
      else {
        this.InScopeSubmissionFilters.emit(submissionSearchCriteria);
      }
    }
  }

  public validateSubmissionFilters(): boolean {
    let isValid: boolean = true;
    let caseNumber = this.submissionSearch.get('caseNumber').value;
    let brokerName = this.submissionSearch.get('brokerName').value;
    let insuredName = this.submissionSearch.get('insuredName').value;
    if (caseNumber.length) {
      if (!this._regularExpressionService.caseNumber.test(caseNumber)) {
        this._toasterService.errorMessage("Please Enter at-least 1 numeric after Prefix for Case Number.");
        isValid = false;
      }
    }
    if (brokerName.length && brokerName.length < 3) {
      this._toasterService.errorMessage("Please Enter at-least 3 alphabet for Broker Name.");
      isValid = false;
    }
    if (insuredName.length && insuredName.length < 3) {
      this._toasterService.errorMessage("Please Enter at-least 3 alphabet for Insured Name.");
      isValid = false;
    }
    return isValid;
  }

  public reset(): void {
    this._toasterService.clear();
    this.isReset = true;
    this.createForm();
    this.Reset.emit();
  }

  public changeSubmissionScope(event: any): void {
    if (!!event.target) {
      this.submissionScope = event.target.value;
      if (this.submissionScope == this.scopeList.OutScope) {
        this.setInOutScopeControlsVisibility(false);
      }
      else if (this.submissionScope == this.scopeList.InScope) {
        this.setInOutScopeControlsVisibility(true);
      }
      this.paramMode = false;
      this.createForm();
      this.SubmissionScopeChange.emit(this.submissionScope);
    }
  }

  private setInOutScopeControlsVisibility(flag: boolean): void {
    this.isInsuredNameVisible = flag;
    this.isBrokerNameVisible = flag;
    this.isStatusVisible = flag;
    this.isDueDateVisible = flag;
    this.isAssignedToVisible = flag;
  }

  private setFilter() {
    this._activatedRoute.queryParams.subscribe((query: any) => {
      if (!!query) {
        this.status = query.submissionStatusId;
        this.queryParam = query;
        this.paramDateRange = query.date;
        if (!!query.scope) {
          this.selectedInOutScope = this.submissionScope = query.scope;
          if (query.scope == this.scopeList.InScope) {
            this.setInOutScopeControlsVisibility(true);
          }
          else {
            this.setInOutScopeControlsVisibility(false);
          }
        }
        if (!!query.scope || !!query.date) {
          this.createForm();
          this.SubmissionScopeChange.emit(query.scope);
        }
        return;
      }
    });
  }

  public onDateValueChange(formValue:any){
    let selectedDateRange=[];
    const date = this.submissionSearch.get(formValue).value;
    if(date.length > 0){
      date.forEach(element => {
        selectedDateRange.push(this._dateParserService.FormatToCurrentTimeZone(element));
      });  
    this.submissionSearch.get(formValue).setValue(selectedDateRange);
    }
    selectedDateRange=[];
}
}

