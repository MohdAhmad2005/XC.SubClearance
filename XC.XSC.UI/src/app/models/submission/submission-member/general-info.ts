export interface Comment {
    id: number;
    commentType: number;
    commentDate: Date;
    description: string;
    commentBy: string;
}

export interface ReviewInformationItem {
    processorName: string;
    receivedDate: Date;
    dueDate: Date;
    reviewerName: string;
    reviewSubmitDate: Date;
    reviewApprovalDate: Date;
    reviewStatus: number;
    comments: Comment[];
}

export interface PasInformationItem {
    submissionId: number;
    submitToPASDate: Date;
    status: number;
    stringMessage: string;
}

export interface ResultItem {
    reviewInformation: ReviewInformationItem;
    pasInformation: PasInformationItem;
}

export interface GeneralInformationResponse {
    isSuccess: boolean;
    message: string;
    result: ResultItem;
}