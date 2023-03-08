export interface IslaConfigurationRequest{  
    regionId:string
    teamId: string        
    lobId:number
    mailBoxId:string
    name:string
    type:number
    day:number
    hours:number
    min:number
    percentage:number
    samplePercentage:number
    taskType:number
    isEscalation:boolean
    isActive:boolean
    id:number
}

export interface IslaConfigurationResponse{
    id:number
    region:string
    team: string        
    lob:string
    mailBox:string
    name:string
    type:number
    day:number
    hours:number
    min:number
    percentage:number
    samplePercentage:number
    taskType:number
    isEscalation:boolean
    regionid:number
    teamId:number
    lobId:number
    mailBoxId:number
    UpdatedBy:string
    SlaDefinition:string
    typeName:string
    mailBoxName:string
}

export interface MailBoxFilter{
    regionId:any
    lobId:any
    teamId:any
}