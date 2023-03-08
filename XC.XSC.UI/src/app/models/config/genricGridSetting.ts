
export class GenericGridSettings {
  public first: number;
  public rows: number;
  public sortField: string;
  public sortOrder: number;
  public globalFilter: number;
  public page: number;
  public limit: number;
  public mulitSortMeta: string;
  public filters: Array<FilterConfig>;
};

export class FilterConfig {
  public columnName: string;
  public operator: string;
  public value: string;
  public logicalOperator: string;
};

export class PageInfo {
  public currentPage: number=0; 
  public totalItems: number=0;
  public totalpages: number=0;
}

export class gridDefaultFilters {
  public first: number;
  public rows: number;
  public sortField: number;
  public sortOrder: number;
  public globalFilter: number;
  public mulitSortMeta: string;
  public filters: Array<FilterConfig>;
}

export enum SortOrder {
  Ascending = 0,
  Descending = 1
}


