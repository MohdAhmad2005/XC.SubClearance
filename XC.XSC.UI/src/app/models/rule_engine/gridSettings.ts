export class AdvancedFilterConfig {
    public AccountStatus: string;
    public Perils: string;
    public Lob: string;
    public RequestType: string;
    public FromDate: string;
    public ToDate: string;
    public AccountType: string;
    public AccountBy: string;
    public AccountName: string;
    public Comment: string;
    public FilterColumn: string;
    public AppDate: string;
  }
  
export class GridSettings extends AdvancedFilterConfig {
    public UserName: string;
    public Skip: number;
    public Take: number;
    public OrderBy: string;
    public Filter: Array<FilterConfig>;
};

export class FilterConfig {
    public ColumnName: string;
    public Operator: string;
    public Value: string;
    public LogicalOperator: string;
};

