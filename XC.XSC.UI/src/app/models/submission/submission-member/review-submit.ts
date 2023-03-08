export interface ReviewSubmit{  
    commentType:string
    commentText: string        
    submissionId:number
    jsonData?:string
}

export interface ReviewReply{  
    commentType:string
    commentText: string        
    submissionId:number
    jsonData?:string
    reviewStatus: number
}