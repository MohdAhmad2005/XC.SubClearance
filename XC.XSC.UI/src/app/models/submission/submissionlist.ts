export class Submissions {

  submissionId:number;

  caseNumber: string;

  brokerName: string;

  insuredName: string;

  fromEmail: string;

  recieveDate: Date;

  dueDate: Date;

  statusId: number;

  statusName:string;

  statusLabel:string;

  stageId:number;

  stageName:string;

  stageLabel:string;

  stageColor:string

  assignedTo: string;

  comments: string;

  emailInfoId:number;

  lobId:string;

  lobName:string;
  clearanceConsent:boolean;
  isDataCompleted:boolean;

}

export interface inScopeSubmission{

  currentPage:number;

  totalItems:number;

  totalPages:number;

  submissions:Submissions[]

}



export interface SubmissionList {

  currentPage: number;

  totalItems: number;

  totalPages: number;

  submissions: Submissions[]

}



export interface TaskTatMetricsResponse {

  receivedDate: Date;

  definedTat: number;

  dueDate: Date;

  tatMissed: boolean;

  daysOverdue: number;

  businessDaysText: string;

}



export interface ISubmissionStatusResponse{

  id:number;

  name:string;

  color:string;

  label:string;

  statusCount:number;

}



export interface SubmissionStatus {

  id: number;

  name: string;

  color: string;

  label: string;

  orderNo: number;

  tenantId: string;

  createdDate: Date;

  createdBy: string;

  modifieddate: Date;

  modifiedBy: string;

  isActive: boolean;

}



export interface AuditHistoryResponse {

  stageid: number;

  stagename: string;

  stageData: string;

  result: string;

 

}

export interface AuditHistoryResponseDuration {

  processingDays: number;

  processingHour: number;

  processingMins: number;

  processingSecs: number;

  reviewDays: number;

  reviewHour: number;

  reviewMins: number;

  reviewSecs: number;

  queryDays: number;

  queryHour: number;

  queryMins: number;

  querySecs: number;

  totalDay: number;

  totalHour: number;

  totalMins: number;

  totalSecs: number;

  result: string;

}



export interface SubmissionClearancesResponse {

  ruleName: string;

  ruleStatus: string;

  remark: string;

}



export enum ActionReferenceType {
  None = 0,
  UnderQuery = "UnderQuery",

  SendBackQueue = "SendBackToQueue",

  AuditHistory = "AuditHistory",

  TatMetrices = "TatMetrices",

  AssignToSelf ="AssignToSelf",

  EditSubmission= "EditSubmission",

  ReAllocation = "ReAllocation",
  AddSlaManagemnt="AddSlaManagemnt",

  EditSlaManagement ="EditSlaManagement",

  ViewSubmission= "ViewSubmission",
  AddReviewManagemnt="AddReviewManagemnt",
  CloseModal="CloseModal",
  EditReviewManagement ="EditReviewManagement",
  DeleteReviewManagement="DeleteReviewManagement",
  SubmitReview ="SubmitReview",
  SubmitforPAS="SubmitforPAS",
  SendBackToProcessor="SendBackToProcessor",
}


export class AssignSubmissionToUserRequest{
  submissionId: number;
  userId: string
}
export interface SubmissionFields {

  SubmissionHeader: SubmissionHeader[],

  DataField: SubmissionDataFields[],

  SortKey: SubmissionSortFields[],

  DataType: {}

}



export enum SubmissionHeader {

  CaseNumber = 'Case Number',

  InsuredName = 'Insured Name',

  BrokerName = 'Broker Name',

  ReceviedDate = 'Recevied Date',

  DueDate = 'Due Date',

  AssignTo = 'Assign To',

  Status = 'Status',

  AssignToSelf = 'Assign To Self'

}

export enum SubmissionDataFields {

  CaseNumber = 'caseNumber',

  InsuredName = 'insuredName',

  BrokerName = 'brokerName',

  ReceviedDate = 'recieveDate',

  DueDate = 'dueDate',

  AssignTo = 'assignedTo',

  Status = 'statusLabel',

  AssignToSelf = 'dropdown'

}

export enum SubmissionSortFields {

  CaseNumber = 'CaseId',

  InsuredName = 'insuredName',

  BrokerName = 'brokerName',

  ReceviedDate = 'EmailInfo.ReceivedDate',

  DueDate = 'dueDate',

  AssignTo = 'AssignedId',

  Status = 'SubmissionStatusId'

}

export class SubmissionsScopeCountResponse{
  totalCount:number;
  inScopeCount:number;
  outScopeCount:number;
}

export class SubmissionPerformanceResponse{
  date:Date;
  processorName:string;
  assignedCount:number;
  completedCount:number;
  accuracy:number;
  tatBreachedCount:number;
}

