export interface IReviewConfigurationRequest{
    id:number;
    regionId:number;
    teamId:number;
    lobId:number;
    processorId:string[];
    reviewType:number;
    reviewerId:string;
    isActive:boolean
}

export interface IReviewConfigurationUpdate{
    id:number;
    regionId:number;
    teamId:number;
    lobId:number;
    reviewType:number;
    reviewerId:string;
    isActive:boolean
}

export interface IReviewConfigurationResponse{
    id:number;
    regionId:number;
    regionName:string
    teamId:number;
    teamName:string;
    lobId:number;
    lobName:string;
    processorId:number;
    processorName:string
    reviewTypeId:number;
    reviewType:string;
    reviewerId:number;
    reviewerName:string;
    isActive:boolean
}