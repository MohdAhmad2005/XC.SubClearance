export interface IEmailInfoResponse{
    id:number;
    emailId:string;
    fromName:string;
    fromEmail:string;
    toEmail:string;
    ccEmail:string;
    subject:string;
    body:string;
    lobId:string;
    messageId:string;
    parentMessageId:string;
    totalDocuments:number;
    receivedDate:Date;
    documentId:string;
    isDuplicate:string;
    extractedBodyDetails:string;
    attachments:IEmailInfoAttachmentResponse[];
  }
  
  export interface IEmailInfoAttachmentResponse{
    id:number;
    emailInfoId:string;
    fileName:string;
    fileType:string;
    documentId:string;
    attachmentId:string;
    fileSize:number;
    sizeUnit:string;
    createdBy:string;
  }
  