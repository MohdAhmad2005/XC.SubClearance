
export interface TenantResponse {
    result: TenantConfig,
    isSuccess: boolean,
    message: string
}

export interface TenantConfig {
    iamUrl: string,
    tenantName: string,
    clientId: string,
    initOptions: Options
}

export interface Options {
    onLoad: string,
    redirectUri: string
}