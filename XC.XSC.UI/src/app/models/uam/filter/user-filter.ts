export interface UserFilterModel{
    attributes: AttributesItems[];
    permissions: string[];
}

export interface AttributesItems{
    name: string;
    value: string[];
}