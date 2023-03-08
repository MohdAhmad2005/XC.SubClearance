export enum SLAFormKeys{
    RegionId="regionId",
    TeamId="teamId",
    LobId="lobId",
    MailBoxId="mailBoxId",
    Type="type",
    Name="name",
    Percentage="percentage",
    Day="day",
    Hours="hours",
    Min="min",
    IsEscalation="isEscalation",
    TaskType="taskType",
    SamplePercentage="samplePercentage"  
}

export enum SLAGlobalFilterField{
    Region="region",
    Team="team",
    Lob="lob",
    Type="type",
    SlaDefinition="slaDefinition",
    MailBoxId="mailBoxId",
    UpdatedBy="updatedBy"
}

export enum SLAType{
    TAT=1,
    Accuracy=2
}

export enum SLAEscalationType{
Active="1",
Inactive ="2"
}