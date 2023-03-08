import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EnvironmentService } from '../../../services/environment-service.service';
import { GenericGridSettings } from 'src/app/models/config/genricGridSetting';
import { AssignSubmissionToUserRequest, AuditHistoryResponse,AuditHistoryResponseDuration, TaskTatMetricsResponse } from 'src/app/models/submission/submissionlist';
import { ConvertObjectToQueryparamService } from '../../utility/common/coonvert-object-to-queryparam.service';
import { IResponse } from 'src/app/models/IResponse';
import { CommonStorageService } from '../../common/common-storage/common-storage.service';

@Injectable({
  providedIn: 'root'
})

export class SubmissionService {
  public assignToUserList: any
  public allUserInfo: any;
  public getAllUserInfo(event: any): void{
      this.allUserInfo = event;
  }
  
  public _apiUrl: any = {
    getPerformance: 'Submission/getPerformance',
    getSubmissionCounts:'Submission/getSubmissionScopeCount',
    getAllSubmission: 'Submission/GetAll',
    getTaskTatMetricsApi: 'submission/getTaskTatMetrics',
    getSubmissionStatus: 'SubmissionStatus/getSubmissionStatus',
    submissionUnderQuery:'submission/submissionUnderQuery',
    sendBackQueue:'submission/sendAssignedSubmissionBackToQueue',
    assignSubmissionToSelfApi: 'submission/assignSubmissionToSelf',
    getSubmissiosnGlanceApi: 'submission/getSubmissionsGlance',
    getAuidutHistoryApi:'TaskAuditHistory/getTaskAduditHistory',
    getAuidutHistoryDurationApi:'TaskAuditHistory/GetTaskAduditHistoryDuration',
    getSubmissionClearancesApi: 'submission/getSubmissionClearances',
    addClearanceCommentApi: 'submission/addClearanceComment',
    getInScopeSubmissionById:'submission/getInScopeSubmissionById',
    getSubmissionData: (id: any) =>`SubmissionExtraction/getSubmissionTransformedData/${id}`,
    saveSubmissionData:`SubmissionExtraction/updateSubmissionTransformedData`,
    getUserRegionsApi: 'user/getUserRegions',
    getUserLobApi: 'lob/getUserLob',
    getSubmissionForm:'SubmissionExtraction/getSubmissionForm',
    getSubmissions: 'submission/getSubmissions',
    getActions:()=> `Submission/getSubmissionActions`,
    sendSubmissionToReview: 'Submission/sendSubmissionToReview',
    sendSubmissionToProcessorApi: 'Submission/sendSubmissionToProcessor',
    getSubmissionGeneralInformationApi: 'Submission/getSubmissionGeneralInformation',
    assignSubmissionToUserApi: 'Submission/assignSubmissionToUser',
    saveSubmissionCommentApi: 'Submission/saveSubmissionComment'
  };

  constructor(private _httpClient: HttpClient,
    private _envService: EnvironmentService,
    private _objToQueryParam: ConvertObjectToQueryparamService,
    private _commonStorageService: CommonStorageService) {
  }
  
  public getPerformance = (startDate: any, endDate: any, performaceType:number):Observable<IResponse> => {
    var commonStorageData = this._commonStorageService.getCommonStorage();
    return this._httpClient.get<IResponse>(this.getRoute(`${this._apiUrl.getPerformance}?startDate=${startDate}&endDate=${endDate}&performanceType=${performaceType}&region=${commonStorageData.region}&lob=${commonStorageData.lob}`, this._envService.smartClearApiUrl));
  }

  public getSubmissionScopeCounts = (startDate: any, endDate: any): Observable<IResponse> => {
    var commonStorageData = this._commonStorageService.getCommonStorage();
    return this._httpClient.get<IResponse>(this.getRoute(`${this._apiUrl.getSubmissionCounts}?startDate=${startDate}&endDate=${endDate}&region=${commonStorageData.region}&lob=${commonStorageData.lob}`,this._envService.smartClearApiUrl));
  }

  public getAllSubmissionList(){
    return this._httpClient.get<IResponse>(this.getRoute(this._apiUrl.getAllSubmission, this._envService.smartClearApiUrl));
  }

  public getTaskTatMetrics = (submissionId: number): Observable<TaskTatMetricsResponse> => {
    return this._httpClient.get<TaskTatMetricsResponse>(this.getRoute(`${this._apiUrl.getTaskTatMetricsApi}/${submissionId}`, this._envService.smartClearApiUrl));
  }

  public getSubmissiosnGlance = (): Observable<IResponse> => {
    var commonStorageData = this._commonStorageService.getCommonStorage();
    return this._httpClient.get<IResponse>(this.getRoute(`${this._apiUrl.getSubmissiosnGlanceApi}?region=${commonStorageData.region}&lob=${commonStorageData.lob}`, this._envService.smartClearApiUrl));
  }
  public submissionUnderQuery = (Commentobj: any): Observable<IResponse> => {
    return this._httpClient.post<IResponse>(this.getRoute(`${this._apiUrl.submissionUnderQuery}`,this._envService.smartClearApiUrl),Commentobj);
  }
  public sendBackQueue = (submissionId: number): Observable<IResponse> => {
    return this._httpClient.get<IResponse>(this.getRoute(`${this._apiUrl.sendBackQueue}/${submissionId}`, this._envService.smartClearApiUrl));
  }

  public getSubmissionStatus() {
    return this._httpClient.get<IResponse>(this.getRoute(this._apiUrl.getSubmissionStatus, this._envService.smartClearApiUrl));
  }
 
  public assignSubmissionToSelf = (submissionId: number): Observable<IResponse> => {
    return this._httpClient.post<IResponse>(this.getRoute(`${this._apiUrl.assignSubmissionToSelfApi}`, this._envService.smartClearApiUrl),submissionId);
  }


  public getSubmissions(submissionFilter: any, gridSetting: GenericGridSettings) {
    var commonStorageData = this._commonStorageService.getCommonStorage();
    let queryStringData = this._objToQueryParam.setQueryParamString(submissionFilter);
    let gridFilterData = this._objToQueryParam.setQueryParamString(gridSetting);
    return this._httpClient.get<IResponse>(this.getRoute(`${this._apiUrl.getSubmissions}?${gridFilterData}&${queryStringData}&region=${commonStorageData.region}&lob=${commonStorageData.lob}`, this._envService.smartClearApiUrl));
  }

  public getSubmissionClearances = (submissionId: number): Observable<IResponse> => {
    return this._httpClient.get<IResponse>(this.getRoute(`${this._apiUrl.getSubmissionClearancesApi}/${submissionId}`, this._envService.smartClearApiUrl));
  }

  public addClearanceComment = (Commentobj: any): Observable<IResponse> => {
    return this._httpClient.post<IResponse>(this.getRoute(`${this._apiUrl.addClearanceCommentApi}`, this._envService.smartClearApiUrl), Commentobj);
  }

  public getInScopeSubmissionById = (submissionId: number): Observable<IResponse> => {
    return this._httpClient.get<IResponse>(this.getRoute(`${this._apiUrl.getInScopeSubmissionById}/${submissionId}`, this._envService.smartClearApiUrl));
  }


  public getAuditHistory = (submissionId: number): Observable<AuditHistoryResponse> => {
    return this._httpClient.get<AuditHistoryResponse>(this.getRoute(`${this._apiUrl.getAuidutHistoryApi}/${submissionId}`, this._envService.smartClearApiUrl));
  }
  
  public getAuditHistoryDuration = (submissionId: number): Observable<AuditHistoryResponseDuration> => {
    return this._httpClient.get<AuditHistoryResponseDuration>(this.getRoute(`${this._apiUrl.getAuidutHistoryDurationApi}/${submissionId}`, this._envService.smartClearApiUrl));
  }
  public getSubmissionData = (url:string): Observable<any> => {
    return this._httpClient.get<any>(this.getRoute(url, this._envService.smartClearApiUrl));
  }

  public saveSubmissiondata = (data: any): Observable<IResponse> => {
    return this._httpClient.post<IResponse>(this.getRoute(`${this._apiUrl.saveSubmissionData}`, this._envService.smartClearApiUrl), data);
  }

  public getSubmissionGeneralInformation = (submissionId: number): Observable<IResponse> => {
    return this._httpClient.get<IResponse>(this.getRoute(`${this._apiUrl.getSubmissionGeneralInformationApi}/${submissionId}`, this._envService.smartClearApiUrl));
  }

  public getUserLobApi() {
    return this._httpClient.get<IResponse>(this.getRoute(this._apiUrl.getUserLobApi, this._envService.smartClearApiUrl));
  }
  public getUserRegions() {
    return this._httpClient.get<IResponse>(this.getRoute(this._apiUrl.getUserRegionsApi, this._envService.smartClearApiUrl));
  }

  public getSubmissionForm(url:any) {
    return this._httpClient.get<IResponse>(this.getRoute(url, this._envService.smartClearApiUrl));
  }

  public saveAndExit = (data: any): Observable<IResponse> => {
    return this._httpClient.post<IResponse>(this.getRoute(`${this._apiUrl.saveSubmissionData}`, this._envService.smartClearApiUrl), data);
  }

  public submitForReview = (reviewData: any): Observable<IResponse> => {
    return this._httpClient.post<IResponse>(this.getRoute(this._apiUrl.sendSubmissionToReview, this._envService.smartClearApiUrl), reviewData);
  }

  public sendBackToProcessor = (reviewReplyData: any): Observable<IResponse> => {
    return this._httpClient.post<IResponse>(this.getRoute(this._apiUrl.sendSubmissionToProcessorApi, this._envService.smartClearApiUrl), reviewReplyData);
  }

  public submitForPAS = (pasData: any): Observable<IResponse> => {
    return this._httpClient.post<IResponse>(this.getRoute(this._apiUrl.saveSubmissionData, this._envService.smartClearApiUrl), pasData);
  }

  public getActionResult():Observable<IResponse>{
    return this._httpClient.get<IResponse>(this.getRoute(this._apiUrl.getActions(), this._envService.smartClearApiUrl));
  }

  public assignSubmissionToUser = (assignSubmissionToUser: AssignSubmissionToUserRequest): Observable<IResponse> =>{
    return this._httpClient.post<IResponse>(this.getRoute(this._apiUrl.assignSubmissionToUserApi, this._envService.smartClearApiUrl),assignSubmissionToUser);
  }

  public saveSubmissionComment = (comment: any): Observable<IResponse> => {
    return this._httpClient.post<IResponse>(this.getRoute(this._apiUrl.saveSubmissionCommentApi, this._envService.smartClearApiUrl), comment);
  }




  private getRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }

}
