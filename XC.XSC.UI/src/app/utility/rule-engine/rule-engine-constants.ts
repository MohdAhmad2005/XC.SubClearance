export const messageConstant = {
    ERRORMESSAGE: "ErrorMessage",
    DATALOADERRORMESSAGE: "DataLoadErrorMessage",
    TRYAGAINERROR: "TryAgainError",
    UREQUESTRES: "URequestedRes",
    SESSIONEXPIRED: "SessionExpired",
    AUTHENTICATETOKEN: "AuthenticateToken",
    REQUESTTIMEOUT: "RequestTimeout",
    SOMETHINGWRONGSERVER: "SomethingWrongServer",
    INVALIDDATA: "InvalidData",
    REQUESTRESOURCE: "RequestResource",
    UNAUTHORIZED: "Unauthorized",
    INVALIDUSERMESSAGE: "InvalidUserMessage",
    UNABLETOCONNECTMESSAGE: "UnableToConnectMessage",
    LOGINSWRMESSAGE: "LoginSWRMessage",
    RC_ACTIONEXPRESSIONLEFTOPERANDMISSING: "RC_ActionExpressionLeftOperandMissing",
    RC_RULECOLLECTIONEMPTY: "RC_RuleCollectionEmpty",
    RC_RULEEXPRESSIONEMPTY: "RC_RuleExpressionEmpty",
    RC_RULEEXPRESSIONINCOMPLETE: "RC_RuleExpressionIncomplete",
    RC_RULEACTIONEMPTY: "RC_RuleActionEmpty",
    RC_RULEACTIONINCOMPLETE: "RC_RuleActionIncomplete",
    RULESETDEACTIVATED: "RuleSetDeactivated",
    RULESETACTIVATED: "RuleSetActivated",
    RULEDELETED: "RuleDeleted",
    RULEINSERTED: "RuleInserted",
    RULEUPDATED: "RuleUpdated",
    SELECTRULE: "SelectRule",
}

export const RC_OPERANDTYPE = {
    Value: 1,
    Reference: 2,
    Function: 3,
    Query: 4
}

export const RC_RULEACTIONTYPE = {
    NormalOperation: 1,
    AggregateOperation: 2
}

export const RC_DATATYPECONST = {
    Alphanumeric: 7
}

export const ConstRoles = {
    Admin: "Admin",
    Analyst: "Analyst",
    Underwriter: "Underwriter",
    UnitLead: "Unit Lead",
    Auditor: "Auditor",
    UnderwriterAssistant: "Underwriter Assistant",
    EAUser: "E&A User",
    TeamLead: "Team Lead",
    Processor: "Processor"
}
