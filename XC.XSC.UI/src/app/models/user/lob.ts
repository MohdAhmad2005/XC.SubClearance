export interface Lob {
  id: number;
  name: string;
  lobId: string;
  description: string;
  submissions: any;
  tenantId: string;
  createdDate: Date;
  createdBy: string;
  modifieddate: Date;
  modifiedBy: string;
  isActive: boolean;
}
