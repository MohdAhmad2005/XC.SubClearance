import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { NotificationModel } from 'src/app/models/rule_engine/notification';

@Injectable({
  providedIn: 'root'
})
export class GlobalService {
  AccountNumber: number = 0;
  processId: number = 0;
  accountName: string = "test";
  //defaultDateFormat: string = "dd-MM-yyy";
  dateSerializationFormat: string = "yyyy-MM-dd";
  clientId: number = 1;
  lobId: number = 1;
  lobName: string = "";
  toolName: string = "CATOne";
  AccountInfohidden: boolean = true;
  message: NotificationModel;
  messageReceive: Subject<NotificationModel> = new Subject<NotificationModel>();
  //userTimeZone: string;
  signalRconnectionState = 0;
  //Set Process level configuration
  TaskProcess: string = "SYSTEM";
  GeoCoding: number = 2;
  Prediction: number = 100;
  ModeOfOperation: number = 1;
  ProcessStatus: string = null;
  ClientName: string = "Xceedance";
  ModelName: string = "Xceedance";
  qaTaskCode: string = "";
  settingConfigData: any = { clientName: '', loginLogoPath: '', forgotLogoPath: '', productName: '', expirationDateDiff: 0, dueDateValidationEnabled: false };//{ apiURL: '', appId: '', clientId: '', allowUnauthorizePage: '', enableSignalRMessages: '', pagesAllowed: '', pageSize: '', clientName: '', loginLogoPath: '', forgotLogoPath: '', productName: '' };
  productName: string = "";
  clientHeader: string = "";
  loginLogoPath: string = "";
  forgotLogoPath: string = "";
  geocodeRowId: any;
  selectedProcessIdFromRm: number = 0;
  formNames: any = {
    queryResolution: 'Query Resolution',
    modellingQCComplete: 'Modelling QC Complete',
    toolDevelopmentOrEnhancement: 'Tool Development or Enhancement',
    submitReportQC: 'Submit Report QC'
  };
  AccountId: string;
  statusChange: Subject<number> = new Subject<number>();
  processTimeChange: Subject<number> = new Subject<number>();
  notifications: Array<NotificationModel> = new Array<NotificationModel>();
  fromLMS: boolean = false;
  scrubToolRunBy: string = "ByCMR";
  showLMS: boolean = true;
  //appUserDate: string = new Date().toISOString();
  headerScreenHeight: number;
  footerScreenHeight: number;
  gridMenuHeight: number = 90;
  gridDefaultHeight: number = 450;
  draftProcessId = 0;
  saveDraftEmitter = new EventEmitter<any>();
  notification: NotificationModel[] = [];
  totalNotification: number = 0;
  allowedFilesTypes: any;
  getDistinctOptions = new EventEmitter<any[]>();
  lmsAccPerils: Array<any> = [];
  isReviewEdit: boolean = false;
  // this variable is for calling the location api from lms for scrubbing tool used from lms
  scrubFromLMSEmitter = new EventEmitter<any>();
  
  //do not use apiService instead of HttpClient as it will cause circular dependency 
  constructor(private http: HttpClient) {

  }

  onProcessTimeChange() {
    this.processTimeChange.next(0);
  }

  appUserDate() {
    return new Date().toISOString();
  }

  calculateDefaultHeight(isFullScreen: boolean, sizeReduce: number, isAdvancedSearch: boolean): number {

    var headerHeight = this.headerScreenHeight;
    var footerHeight = this.footerScreenHeight;
    var gridResizeHeight = window.innerHeight - (headerHeight + footerHeight + this.gridMenuHeight);
    //if (isFullScreen) {
    //  gridResizeHeight = gridResizeHeight- this.gridMenuHeight
    //}
    if (!isFullScreen && isAdvancedSearch)
      gridResizeHeight = gridResizeHeight - 155;
    //if (isFullScreen && isAdvancedSearch)
    //  gridResizeHeight = gridResizeHeight + 170;
    if (isFullScreen && !isAdvancedSearch)
      gridResizeHeight = gridResizeHeight + 170;
    if (sizeReduce > 0)
      gridResizeHeight = gridResizeHeight - sizeReduce;

    return gridResizeHeight;
  }
  onStatusChange() {
    this.statusChange.next(this.processId);
  }

  //Load Setting config file
  loadSettingConfig() {

    this.getSettingConfig().subscribe(res => {

      this.settingConfigData.clientName = res.clientName;
      this.settingConfigData.loginLogoPath = res.loginLogoPath;
      this.settingConfigData.forgotLogoPath = res.forgotLogoPath;
      this.settingConfigData.productName = res.productName;
      this.settingConfigData.expirationDateDiff = res.ExpirationDateDiff;
      this.settingConfigData.dueDateValidationEnabled = res.DueDateValidationEnabled;
      //this.clientHeader = res.clientName;
      //this.loginLogoPath = res.loginLogoPath;
      //this.forgotLogoPath = res.forgotLogoPath;
      //this.productName = res.productName;
    })
    return;
  }

  getSettingConfig(): Observable<any> {
    return this.http.get('./assets/config/setting.json');
  }

  broadcast() {
    this.messageReceive.next(this.message)
  }

  referenceLookupType = {
    limitBasis: "LimitBasis",
    limitType: "LimitType",
    dedBasis: "DeductibleBasis",
    dedType: "DeductibleType",
    limitCode: "LimitCode",
    dedCode: "DeductibleCode",
    values: 'Values',
    aggregates: 'Aggregates',
    dropDown: 'DropDown',
    limitAppAusAcct: 'LimitAppAusAcct',
    constLimitBasis: "LimitBasis",
    constLimitType: "LimitType",
    constLimitCode: "LimitCode",
    constDedBasis: "DeductibleBasis",
    constDedType: "DeductibleType",
    constDedCode: "DeductibleCode",
    criteria: 'CriteriaColumns'
  }

  isNullOrUndefied(val): boolean {
    if (val === null || val === undefined) {
      return true;
    }
    return false;
  }

  getNotificationData() {
    return { "Notifications": this.notifications, "TotalNotifications": this.notifications.filter(x => !x.read).length };
  }

}
