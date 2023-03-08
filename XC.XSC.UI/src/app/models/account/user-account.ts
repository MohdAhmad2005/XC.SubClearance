export interface UserAccountDetailResponse{
  isSuccess:boolean;
  message:string;
  result: UserAccountItem[];
}

export interface UserAccountItem{
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

interface ManagerItem{
  managerId: string;
  managerName: string;
}

interface AccessItem{
  manageGroupMembership: boolean;
  view: boolean;
  mapRoles: boolean;
  impersonate: boolean;
  manage: boolean;
}

interface HolidayListItem{
  holidayListId: string;
  holidayListName: string;
}

interface RegionItem{
  regionId: string;
  regionName: string;
}

interface LobItem{
  lobId: string;
  lobName: string;
}

interface TeamItem{
  teamId: string;
  teamName: string;
}

interface RealmRolesItem{
  roleId: string;
  roleName: string;
}