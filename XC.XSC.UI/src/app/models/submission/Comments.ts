export interface Comments{
      
        CommentType:string
        CommentText: string        
        SubmissionId:number
        JsonData?:string
       
}

export interface CommentsClearance {

  commentText: string
  submissionId: number
  clearanceConscent: boolean
}

