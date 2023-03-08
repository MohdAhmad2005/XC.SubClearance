export interface LastSessionReport {
    time: number
    type: string
    realmId: string
    clientId: string
    userId: string
    sessionId: string
    ipAddress: string
    error: any
}

export interface LastSessionResponse {
    isSuccess: boolean;
    message: string;
    result: LastSessionReport;
}
 