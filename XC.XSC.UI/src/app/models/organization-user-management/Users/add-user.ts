export interface AddOrganizationUserRequest{
    createdTimestamp: number;
    username: string;
    enabled: boolean;
    totp: boolean;
    emailVerified: boolean;
    firstName: string;
    lastName: string;
    email: string;
    attributes: AttributesItems;
    disableableCredentialTypes: any[];
    requiredActions: any[];
    notBefore: number;
    access: AccessItems;
    realmRoles: string[];
}

export interface AttributesItems{
    holidayListId: string[];
    managerId: string[];
    isTeamManager: string[];
    regionId: string[];
    teamId: string[];
    businessDetails: string[];
    lobId: string[];
    role: string[];
}

export interface AccessItems{
    manageGroupMembership: boolean;
    view: boolean;
    mapRoles: boolean;
    impersonate: boolean;
    manage: boolean;
}

export interface AddUserResponse
{
    isSuccess: boolean;
    message: string;
    result: AddOrganizationUserRequest[]
}
