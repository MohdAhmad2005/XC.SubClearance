export interface UserResponse{
    isSuccess:boolean;
    message:string;
    result: UserItem[];
}

export interface UserItem{
    id: string;
    createdTimestamp: number;
    userName: string;
    enabled: boolean;
    totp: boolean;
    emailVerified: boolean;
    firstName: string;
    lastName: string;
    email: string;
    isTeamManager: string;
    manager: ManagerItem[];
    disableableCredentialTypes: string[];
    requiredActions: string[];
    notBefore: number;
    access: AccessItem;
    holidayList: HolidayListItem[];
    region: RegionItem[];
    lob: LobItem[];
    businessDetails: string[];
    team: TeamItem[];
    role: RealmRolesItem[];
}

export interface ManagerItem{
    managerId: string;
    managerName: string;
}

export interface AccessItem{
    manageGroupMembership: boolean;
    view: boolean;
    mapRoles: boolean;
    impersonate: boolean;
    manage: boolean;
}

export interface HolidayListItem{
    holidayListId: string;
    holidayListName: string;
}

export interface RegionItem{
    regionId: string;
    regionName: string;
}

export interface LobItem{
    lobId: string;
    lobName: string;
}

export interface TeamItem{
    teamId: string;
    teamName: string;
}

export interface RealmRolesItem{
    roleId: string;
    roleName: string;
}


export interface UserRoleResponse{
    isSuccess:boolean;
    message:string;
    result: UserRoleItem;
}

export interface UserRoleItem{
    realmMappings: RealmMappingItem[];
}

export interface RealmMappingItem{
    id: string;
    name: string;
    description: string;
    composite: boolean;
    clientRole: boolean;
    containerId: string;
}