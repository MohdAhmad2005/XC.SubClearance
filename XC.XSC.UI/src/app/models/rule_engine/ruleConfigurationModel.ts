import { GridSettings } from 'src/app/models/rule_engine/gridSettings';

export class masterDataSourceModel {
    Code: string;
    Description: string;
}

export class creatorModel {
    CreatedBy: number;
    CreatedOn: Date;
    CreatedByIP: string;
}

export class ruleTypeModel {
    public RuleTypeId: number;
    public RuleTypeName: string;
}

export class ruleExecutionTypeModel {
    public ExecutionTypeId: number;
    public Name: string;
    public IsCustomFlow: boolean;
}

export class ruleActionTypeModel {
    public ActionTypeId: number;
    public TypeName: string;
}

export class ruleContextModel {
    public ContextId: number;
    public ContextName: string;
    public HasTargetContext: boolean;
    public IsLOBRequired: boolean;
    public IsCommandApplicable: boolean;
}

export class contextEntityModel {
    public EntityId: number;
    public EntityName: string;
    public DisplayName: string;
}

export class subEntityModel {
    public SubEntityId: number;
    public SubEntityName: string;
    public DisplayName: string;
}

export class entityFieldModel {
    public FieldId: number;
    public LOBId: boolean;
    public DisplayName: string;
    public DataTypeId: number;
    public IsSelectable: boolean;
}

export class dataTypeModel {
    public DataTypeId: number;
    public Name: string;
}

export class operatorModel {
    public OperatorId: number;
    public Name: string;
    public Expression: string;
    public OperatorTypeId: number;
}

export class operandTypeModel {
    public OperandTypeId: number;
    public Description: number;
}

export class ruleFunctionModel {
    public FunctionId: number;
    public FunctionName: string;
    public HasParameter: boolean;
    public IsAggregate: boolean;
}

export class ruleModel {
    constructor() {
        this.ActionTypeId = 0;
    }
    public RuleId: number;
    public FallbackRuleId?: number;
    public Expression: string;
    public ActionTypeId: number;
}

export class ruleExpressionModel {
    public RuleId: number;
    public ExpressionId: number;
    public LeftOpTypeId: number;
    public LeftOpValue: string;
    public LeftSubEntityDetail: subEntityModel;
    public LeftOperandFieldDetail: entityFieldModel;
    public ComparisonOperatorDetail: operatorModel;
    public RightOpTypeId: number;
    public RightOpValue: string;
    public RightOpFunctionDetail: ruleFunctionModel;
    public RightSubEntityDetail: subEntityModel;
    public RightOperandFieldDetail: entityFieldModel;
    public QueryDatabaseDetail: queryDatabaseModel;
    public AssignmentOperatorDetail: operatorModel;
    public NextOperatorDetail: operatorModel;
}

export class ruleExpressionFunctionParameters {
    public ExpressionId: number;
    public ParameterDetailId: number;
    public ParameterTypeId: number;
    public ParameterValue: string;
    public SubEntityDetail: subEntityModel;
    public FieldDetail: entityFieldModel;
    public AssignmentOperatorDetail: operatorModel;
}

export class ruleActionModel {
    public RuleId: number;
    public ActionId: number;
}

export class actionExpressionModel {
    public ActionId: number;
    public ExpressionId: number;
    public LeftSubEntityDetail: subEntityModel;
    public LeftOperandFieldDetail: entityFieldModel;
    public RightOpTypeId: number;
    public RightOpValue: string;
    public RightOpFunctionDetail: ruleFunctionModel;
    public RightSubEntityDetail: subEntityModel;
    public RightOperandFieldDetail: entityFieldModel;
    public AssignmentOperatorDetail: operatorModel;
}

export class actionSubExpressionModel {
    public ExpressionId: number;
    public SubExpressionId: number;
    public RightOpTypeId: number;
    public RightOpValue: string;
    public RightOpFunctionDetail: ruleFunctionModel;
    public RightSubEntityDetail: subEntityModel;
    public RightOperandFieldDetail: entityFieldModel;
    public AssignmentOperatorDetail: operatorModel;
}


export class ruleSetDetailModel {
    RuleSetId: number;
    RuleSetName: string;
    Message: string;
    Description: string;
    LOBId: number;
    IsCustom: boolean;
    IsMaster: boolean;
    ContextDetail: ruleContextModel;
    SourceEntityDetail: contextEntityModel;
    TargetEntityDetail: contextEntityModel;
    RuleTypeDetail: ruleTypeModel;
    RuleExecutionTypeDetail: ruleExecutionTypeModel;
    Rules: Array<ruleModel>;
    RuleExpressions: Array<ruleExpressionModel>;
    RuleExpressionFunctionParameters: Array<ruleExpressionFunctionParameters>;
    RuleActions: Array<ruleActionModel>;
    RuleActionExpressions: Array<actionExpressionModel>;
    RuleActionSubExpressions: Array<actionSubExpressionModel>;
    CreatorDetail: creatorModel;
    constructor() {
        this.IsCustom = false;
        this.IsMaster = false;
        this.RuleTypeDetail = new ruleTypeModel();
        this.RuleExecutionTypeDetail = new ruleExecutionTypeModel();
        this.ContextDetail = new ruleContextModel();
        this.SourceEntityDetail = new contextEntityModel();
        this.TargetEntityDetail = new contextEntityModel();
        this.CreatorDetail = new creatorModel();
        this.Rules = [];
        this.RuleExpressions = [];
        this.RuleExpressionFunctionParameters = [];
        this.RuleActions = [];
        this.RuleActionExpressions = [];
        this.RuleActionSubExpressions = [];
    }
}

export class ruleConfigurationType {
    id: number;
    description: string;
}

export class ruleConfigurationProcessWise {
    RefRuleSetId: number;
    ProcessId: number;
    Model: string;
    TransformationContextKey: string;
    RuleSetModelConfig: ruleSetDetailModel;
    constructor() {
        this.RuleSetModelConfig = new ruleSetDetailModel();
    }
}

export class transformationRuleProcess {
    ProcessId: number;
    Model: string;
    TransformationContextKey: string;
    IsCustom: boolean;
    FilterConfig: GridSettings;
}

export class activateRuleSet {
    RuleSetId: number;
    ProcessId: number;
    Model: string;
    TransformationContextKey: string;
    IsActive: boolean;
}

export class queryDatabaseModel {
    DatabaseId: number;
    DatabaseName: string;
}
