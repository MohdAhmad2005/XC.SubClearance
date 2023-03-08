export interface UpdateUserResponse
{
    isSuccess: boolean;
    message: string;
    result: UpdateOrganizationUserRequest[]
}

export interface UpdateOrganizationUserRequest{
    id: string;
    createdTimestamp: number;
    userName: string;
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