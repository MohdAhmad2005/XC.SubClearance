import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { ModalDialogComponent } from '../../../common/modal-dialog/modal-dialog.component';
import { SubmissionBindingType } from '../../../enum/submission/submission-binding-type';
import { SubmissionService } from '../../../Services/submission/submission.service';
import { TaskAuditHistoryComponent } from 'src/app/components/submission/submissions/task-audit-history/task-audit-history.component';
import { TaskTatMetricsComponent } from 'src/app/components/submission/submissions/task-tat-metrics/task-tat-metrics.component';
import { BindGridSettingService } from 'src/app/utility/common/bind-grid-settings.service';
import { GenericGridSettings, PageInfo, gridDefaultFilters, SortOrder } from 'src/app/models/config/genricGridSetting';
import { UnderQueryComponent } from '../submissions/under-query/under-query/under-query.component';
import { ActionReferenceType, Submissions, SubmissionSortFields } from 'src/app/models/submission/submissionlist';
import { ToasterService } from '../../../common/toaster/toaster.service';
import { actions } from 'src/app/common/constant/constant';
import { SharedService } from 'src/app/Services/shared.service';
import { NotificationServiceService } from '../../../common/notification/notification-service.service';
import { ReAllocationComponent } from '../submissions/re-allocation/re-allocation.component';

@Component({
  selector: 'app-submission-list',
  templateUrl: './submission-list.component.html',
  styleUrls: ['./submission-list.component.css']
})

export class SubmissionListComponent implements OnInit {

  private submissionFilters: any;
  public actionsResult:any[];
  public isSubmissionListVisible: boolean = true;
  public columnDefination = [];
  public tableContent: Submissions[];
  public submissionBindingType: SubmissionBindingType;
  public pageInfo: PageInfo;
  public genericGridSettings: GenericGridSettings = new GenericGridSettings();
  public genericGridCurrentSetting: gridDefaultFilters = new gridDefaultFilters();

  public globalFilterField: any[] = ['caseNumber', 'insuredName'];

  private isCaseNumberVisble: boolean = true;
  private isInsuredNameVisble: boolean = false;
  private isBrokerNameVisble: boolean = false;
  private isReceivedDateVisble: boolean = false;
  private isDueDateVisble: boolean = false;
  private isAssignedToVisble: boolean = false;
  private isStatusVisble: boolean = false;
  private isAsignedToSelfVisible: boolean = false;
  private isActionVisble: boolean = false;
  public isConfrimAlertVisible: boolean = false;
  public confirmationMessage: string;
  public submissionId: number;
  public caseNumber: string;
  public actionReference: ActionReferenceType;

  public assignToSelfList: any = [
    'Select',
    'Assign to Myself'];

  @Input() set SubmissionListControlsVisibility(value: any) {
    this.tableContent = [];
    this.pageInfo = new PageInfo();
    if (!!value) {
      this.setSubmissionListColumnVisibility(value);
    }
  }
  @Input() set SubmissionFilters(value: any) {
    if (!!value) {
      this.submissionFilters = value;
      this.getTableContent('');
    }
  }
  @Input() set BindingType(value: SubmissionBindingType) {
    if (!!value) {
      this.submissionBindingType = value;
    }
  }

  @Input() IsActionVisible: boolean;

  @Input() SortField: SubmissionSortFields;
  @Input() SortOrder: SortOrder;

  constructor(private _submissionService: SubmissionService,
    private _modalService: NgbModal,
    private _router: Router,
    private _bindGridSettingService: BindGridSettingService,
    private _toasterService: ToasterService,
    private sharedService: SharedService,
    private _notificationService: NotificationServiceService
    ) { }

  ngOnInit(): void {
    this.getActions();
  }

  private setSubmissionListColumnVisibility(data: any): void {
    this.isInsuredNameVisble = data.isInsuredNameVisble;
    this.isBrokerNameVisble = data.isBrokerNameVisble;
    this.isReceivedDateVisble = data.isReceivedDateVisble;
    this.isDueDateVisble = data.isDueDateVisble;
    this.isAssignedToVisble = data.isAssignedToVisble;
    this.isStatusVisble = data.isStatusVisble;
    this.isAsignedToSelfVisible = data.isAsignedToSelfVisible;
    this.isActionVisble = data.isActionVisble;
    this.setSubmissionListColumns();
  }

  private setSubmissionListColumns(): void {
    this.columnDefination = [];
    if (this.isCaseNumberVisble) {
      this.columnDefination.push({ header: 'Case Number', field: 'caseNumber', sortKey: 'CaseId', type: 'link' })
    }
    if (this.isInsuredNameVisble) {
      this.columnDefination.push({ header: 'Insured Name', field: 'insuredName', sortKey: 'insuredName' },)
    }
    if (this.isBrokerNameVisble) {
      this.columnDefination.push({ header: 'Broker Name', field: 'brokerName', sortKey: 'brokerName' },)
    }
    if (this.isReceivedDateVisble) {
      this.columnDefination.push({ header: 'Recevied Date', field: 'recieveDate', type: 'date', sortKey: 'EmailInfo.ReceivedDate' },)
    }
    if (this.isDueDateVisble) {
      this.columnDefination.push({ header: 'Due Date', field: 'dueDate', type: 'date', sortKey: 'DueDate' },)
    }
    if (this.isAssignedToVisble) {
      this.columnDefination.push({ header: 'Assigned To', field: 'assignedTo', sortKey: 'AssignedId' },)
    }
    if (this.isStatusVisble) {
      this.columnDefination.push({ header: 'Status', field: 'statusLabel', sortKey: 'SubmissionStatusId' },)
    }
    if (this.isAsignedToSelfVisible) {
      this.columnDefination.push({ header: 'Assign To Self', type: 'dropdown', data: this.assignToSelfList },)
    }
    if (this.isActionVisble) {
      //this.columnDefination.push({ header: 'Action', render: (data) => { return this.setActionFields(data); } })
    }
  }
  public setActionEvents(event: any): void {        
    if (!!event) {
      let ActionType = event.event.actionType;
      this.submissionId = event.data.submissionID;
      if(ActionType == ActionReferenceType.ViewSubmission){
        let routeLink = event.event.url;
        this._router.navigate([routeLink], {
          queryParams: {
            submissionId: this.submissionId            
          }})
      }
      if (ActionType == ActionReferenceType.AuditHistory) {
        let ngbModalOptions: NgbModalOptions = {
          backdrop: 'static',
          keyboard: false,
        };
        const modalRef = this._modalService.open(TaskAuditHistoryComponent, ngbModalOptions);
        modalRef.componentInstance.submissionId = event.data.submissionID;
      }
      if (ActionType == ActionReferenceType.TatMetrices) {
        let ngbModalOptions: NgbModalOptions = {
          backdrop: 'static',
          keyboard: false,
          size: 'lg'
        };
        const modalRef = this._modalService.open(TaskTatMetricsComponent, ngbModalOptions);
        modalRef.componentInstance.submissionId = event.data.submissionID;
      }
      if (ActionType == ActionReferenceType.UnderQuery) {
        let ngbModalOptions: NgbModalOptions = {
          backdrop: 'static',
          keyboard: false,
          size: 'lg'
        };
      
        const modalRef = this._modalService.open(UnderQueryComponent, ngbModalOptions);
        modalRef.componentInstance.submissionId = event.data.submissionID;
        modalRef.componentInstance.caseNumber = event.data.caseNumber;
        modalRef.result.then((data) => {
          if(data){
            this.getTableContent(this.genericGridCurrentSetting);
            this._toasterService.successMessage("Case No " + event.data.caseNumber + " has been put Under Query" );
           
          }
        },
        (error) => {
          this._toasterService.errorMessage('An error has occurred.')
        });
      }
      if (ActionType == ActionReferenceType.SendBackQueue) {

        this.submissionId = event.data.submissionID;
        this.caseNumber = event.data.caseNumber;
        this.isConfrimAlertVisible = true;
        this.actionReference = ActionReferenceType.SendBackQueue;
        this.confirmationMessage = "Are you sure you want to send the Case " + this.caseNumber + " back to the queue?";

      }
      if (ActionType == ActionReferenceType.EditSubmission) {
        let routeLink = event.event.url;
        this._router.navigate([routeLink], {
          queryParams: {
            submissionId: this.submissionId,
            action: event.event.actionType
          }})
      }
      if (ActionType == ActionReferenceType.ReAllocation){
        let ngbModalOptions: NgbModalOptions = {
          backdrop: 'static',
          keyboard: false,
          size: 'lg'
        };
        const modalRef = this._modalService.open(ReAllocationComponent, ngbModalOptions);
        modalRef.componentInstance.submissionId = event.data.submissionID;
        this._submissionService.getAllUserInfo(event.data);
        modalRef.result.then((data =>{
          if(data){
            this.getTableContent(this.genericGridCurrentSetting);
          }
        }));
      }
    }
  }

  public getSelectedRowData(event: any): void {
    let columnName = event.column.field;

    this.submissionId = event.data.submissionID;
    if (columnName == 'caseNumber') {

      this._router.navigate(["SubmissionMember"], {
        queryParams: {
          submissionId: this.submissionId
        }})
    }
  }

  public getTableContent(event: any) {
    this.genericGridSettings = this._bindGridSettingService.BindGridSettings(event);

    if (event === '') {
      this.genericGridSettings.sortField = (!!this.SortField) ? this.SortField : this.genericGridSettings.sortField;
      this.genericGridSettings.sortOrder = (!!this.SortOrder) ? this.SortOrder : this.genericGridSettings.sortOrder;      
    }

    const currentSelectedFilters: gridDefaultFilters = {
      first: event.first,
      rows: event.rows,
      sortField: event.sortedField,
      sortOrder: event.sortOrder,
      globalFilter: event.globalFilter,
      mulitSortMeta: event.mulitSortMeta,
      filters: event.filters
    }
    this.genericGridCurrentSetting = currentSelectedFilters;
    if (this.isSubmissionListVisible) {
      this._submissionService.getSubmissions(this.submissionFilters, this.genericGridSettings).subscribe(response => {
        if (response.isSuccess) {
          this.tableContent = response.result.submissions;
          const pageInfo: PageInfo = {
            currentPage: response.result.currentPage,
            totalItems: response.result.totalItems,
            totalpages: response.result.totalPages
          }
          this.pageInfo = pageInfo;
        }
        else {
          this.tableContent = null;
          const pageInfo: PageInfo = {
            currentPage: 1,
            totalItems: 0,
            totalpages: 0
          }
          this.pageInfo = pageInfo;
        }
      },
        error => {
          this.displayModalDialog();
        });

    }
  }

  private displayModalDialog(message?: string): void {
    const modalRef = this._modalService.open(ModalDialogComponent);
    if (message == undefined)
      modalRef.componentInstance.ModalBody = "Unable to process your request. please try again later.";
    else
      modalRef.componentInstance.ModalBody = message;
  }

  public dropDownEventEmitter(data: any): void {
    if (this.submissionBindingType == SubmissionBindingType.NewSubmission && data.selectedValue.toLowerCase() == 'assign to myself') {
      this.submissionId = data.rowData.submissionID;
      this.caseNumber = data.rowData.caseNumber;
      this.actionReference = ActionReferenceType.AssignToSelf;
      this.isConfrimAlertVisible = true;
      this.confirmationMessage = 'Are you sure you would like to assign Case Number ' + this.caseNumber + ' to yourself?';


    }
  }

  public confirmationClick(event) {
    if (!!event) {
      if (event.isConfirmed) {
        if (event.referenceType == ActionReferenceType.AssignToSelf) {
          this._submissionService.assignSubmissionToSelf(this.submissionId).subscribe(response => {
            if (response.isSuccess) {
              this._notificationService.refereshNotification();
              this._toasterService.successMessage('Successfully assigned Case Number ' + this.caseNumber + ' to you');
              this.isConfrimAlertVisible = false;
              this.getTableContent(this.genericGridCurrentSetting);
            }
            else {
              this.isConfrimAlertVisible = false;
              this._toasterService.errorMessage(response.message);
              this.getTableContent(this.genericGridCurrentSetting);
            }
          },
            error => {
              this.isConfrimAlertVisible = false;
              this.displayModalDialog();
              this.getTableContent(this.genericGridCurrentSetting);
            });

        }
        if (event.referenceType == ActionReferenceType.SendBackQueue) {
          this._submissionService.sendBackQueue(this.submissionId).subscribe(response => {
            if (response.isSuccess) {
              this._notificationService.refereshNotification();
              this._toasterService.successMessage('Case Number ' + this.caseNumber + ' successfully send back to queue');
              this.getTableContent(this.genericGridCurrentSetting);
              this.isConfrimAlertVisible = false
            }
            else {
              this.isConfrimAlertVisible = false;
              this.displayModalDialog();
              this.getTableContent(this.genericGridCurrentSetting);
            }
          },
            error => {
              this.isConfrimAlertVisible = false;
              this.displayModalDialog();
              this.getTableContent(this.genericGridCurrentSetting);
            });
        }
      }
      else {
        if (event.referenceType == ActionReferenceType.AssignToSelf) {
           this.getTableContent(this.genericGridCurrentSetting);
        }
        this.isConfrimAlertVisible = false;
      }
    } 
  };

  getActions(){
    this._submissionService.getActionResult().subscribe({
      next: (response) => {
        this.actionsResult = response['result']
        // this.actionsResult = actions.actions[0].submissionStatusMappedAction;
        this.sharedService.getActionEventEmitter.emit(this.actionsResult);
      }, 
      error: (err: any) => { },
      complete: () => { }
    }
    )
  }
}
