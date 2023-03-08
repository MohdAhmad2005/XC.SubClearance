import { Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { LazyLoadEvent } from 'primeng/api';
import { DateparserService } from 'src/app/utility/dateparser.service';
import { PageInfo } from 'src/app/models/config/genricGridSetting';
import { SharedService } from 'src/app/Services/shared.service';
import { UserInfoDto } from 'src/app/models/user/user-info-dto';
import { AccountService } from 'src/app/Services/account/account.service';
import { UserAccountDetailResponse } from 'src/app/models/account/user-account';

@Component({
  selector: 'generic-table',
  templateUrl: './generic-table.component.html',
  styleUrls: ['./generic-table.component.css'],
  encapsulation: ViewEncapsulation.None,
})

export class GenericTableComponent implements OnInit {
  @Input() RowData: any[];
  @Input() ColumnDefination: any[];
  @Input() FilterField: any[];
  rowActions:any[];
  @Input() IsActionVisible: boolean = false;
  @Output() LinkEventEmitter: EventEmitter<any> = new EventEmitter();
  @Output() ActionEventEmitter: EventEmitter<any> = new EventEmitter();
  @Output() OnClickDropdownEventEmitter: EventEmitter<any> = new EventEmitter();
  @Output() LazyLoading = new EventEmitter();
  loading: boolean = false;
  public pageNumber: number;
  public totalPages: number;
  public ActionList: any;
  public totalRecords: number;
  public isActionNotVisible: boolean;
  @Input() Actions: any[];
  @Input() set PageInfo(value: PageInfo) {
    if (!!value) {
      this.totalRecords = value.totalItems;
      this.totalPages = value.totalpages;
      this.pageNumber = value.currentPage;
    }
  }
  private _userProfile: UserInfoDto;

  constructor(private _dateformatter: DateparserService, private sharedService:SharedService,
    private _accountService: AccountService,) { }

  ngOnInit(): void {
    this.sharedService.getActionEventEmitter.subscribe(res=>{
      this.ActionList = res
    });
    this.getCurrentUserProfile();
  }

  public onClickViewEdit(rowData: any, column: any) {
    this.LinkEventEmitter.emit({ 'data': rowData, 'column': column })
  }

  public getClass(rowData: any, colField: string): String {
    if (colField == 'statusLabel') {
      return (!!rowData.statusColor) ? rowData.statusColor : '';
    }
    return '';
  }

  public getDate(data: any) {
    return (this._dateformatter.FormatDate(data))
  }

  public onActionClick(actionItem: any, rowData: any): void {
    this.ActionEventEmitter.emit({ event: actionItem, data: rowData });
  }

  public loadTableData(event: LazyLoadEvent): void {
    if (!!this.RowData && this.RowData.length > 0) {
      this.LazyLoading.emit(event);
    }
  }

  public dropdownChangeEvent(event: any, rowData: any): void {
    const value = { selectedValue: event.target.value, rowData: rowData }
    this.OnClickDropdownEventEmitter.emit(value);
  }

  getActionByStatus(rowData: any) {
    try {
      let user = `${this.userInfo.firstName} ${this.userInfo.lastName}`;
      if (rowData.assignedTo === user && this.ActionList) {
        this.rowActions = this.sharedService.getActionbyStatus(rowData.statusId, this.ActionList.submissionStatusMappedAction);
      } else {
        this.rowActions = this.ActionList?.notAssignedSubmissionActions?.allowedActions;
      }
    } catch (e) {
    }
  }

  get userInfo(): UserInfoDto {
    return this._userProfile;
  }

  private getCurrentUserProfile():void{
    this._accountService.getUserAccountDetails().subscribe(
      (response: UserAccountDetailResponse) => {
        if (response.isSuccess) {
          this._userProfile = <UserInfoDto>{
            email: response.result[0].email,
            firstName: response.result[0].firstName,
            lastName: response.result[0].lastName,
            id: response.result[0].id
          };
        }
        else {
          console.log(response.message);
        }
      },
      (error) => {
        console.log(error);
      }
    );
  }
}