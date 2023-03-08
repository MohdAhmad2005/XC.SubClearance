import { Component, OnDestroy, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
//import { NgxUiLoaderService } from 'ngx-ui-loader';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { actionExpressionModel, actionSubExpressionModel, creatorModel, entityFieldModel, masterDataSourceModel, operandTypeModel, operatorModel, queryDatabaseModel, ruleActionModel, ruleActionTypeModel, ruleExpressionFunctionParameters, ruleExpressionModel, ruleFunctionModel, ruleModel, ruleSetDetailModel, subEntityModel } from 'src/app/models/rule_engine/ruleConfigurationModel';
import { GlobalService } from 'src/app/Services/rule-engine/global.service';
import { MessageService } from 'src/app/Services/rule-engine/message.service';
import { UserInfoService } from 'src/app/Services/rule-engine/user-info.service';
import { ApplicationMessageService } from 'src/app/Services/rule-engine/application-message.service';
import { RuleConfigurationService } from 'src/app/Services/rule-engine/rule-configuration.service';
import { messageConstant, RC_DATATYPECONST, RC_OPERANDTYPE, RC_RULEACTIONTYPE } from 'src/app/utility/rule-engine/rule-engine-constants';
import { ToasterService } from '../../../common/toaster/toaster.service';

@Component({
  selector: 'app-rule-editor',
  templateUrl: './rule-editor.component.html',
  styleUrls: ['./rule-editor.component.css']
})


export class RuleEditorComponent implements OnInit, OnDestroy {
  private onDestroy$: Subject<void> = new Subject<void>();

  dxSelectCustomAttr = { 'class': 'customselectbox' };

  operandTypeConst = RC_OPERANDTYPE;
  dataTypeConst = RC_DATATYPECONST;
  ruleActionTypeConst = RC_RULEACTIONTYPE;

  ruleHasAggregateFunction: boolean = false;
  ruleDetailSubmitted: boolean = false;
  defaultSourceEntityItem: subEntityModel;
  defaultTargetEntityItem: subEntityModel;
  ruleSetDetail: ruleSetDetailModel;
  sourceSubEntityList: Array<subEntityModel> = [];
  targetSubEntityList: Array<subEntityModel> = [];
  operandTypeList: Array<operandTypeModel> = [];
  queryDatabaseList: Array<queryDatabaseModel> = [];
  ruleActionTypeList: Array<ruleActionTypeModel> = [];

  ruleList: Array<ruleModel>;
  ruleExpressionList: Array<ruleExpressionModel>;
  ruleExpFuncParameterList: Array<ruleExpressionFunctionParameters>;
  ruleActionList: Array<ruleActionModel>;
  actionExpressionList: Array<actionExpressionModel>;
  actionSubExpressionList: Array<actionSubExpressionModel>;

  leftEntityFieldList: Array<entityFieldModel> = [];
  rightEntityFieldList: Array<entityFieldModel> = [];
  comparisonOperatorList: Array<operatorModel> = [];
  logicalOperatorList: Array<operatorModel> = [];
  assignmentOperatorList: Array<operatorModel> = [];

  fieldListForEntity: Array<Array<entityFieldModel>> = [];
  operatorListForDataType: Array<Array<operatorModel>> = [];
  functionListForDataType: Array<Array<ruleFunctionModel>> = [];
  patternListForDataType: Array<string> = [];
  masterDataListForField: Array<Array<masterDataSourceModel>> = [];
  isQueryEditorVisible: boolean;
  selectedRuleExpressionId: number;
  selectedExpressionQuery: string;
  selectedDatabaseDetail: queryDatabaseModel;

  userName: string;
  userId: number;
  userRoles: any;

  constructor(
    private router: Router, private globalService: GlobalService,
    private toastr: ToasterService, private titleService: Title,
    //private ngxLoader: NgxUiLoaderService,
    private userInfoService: UserInfoService,
    private appMessageService: ApplicationMessageService,
    private ruleConfigService: RuleConfigurationService
  ) {
  }

  ngOnDestroy() {
    this.onDestroy$.next();
    this.onDestroy$.complete();
  }

  ngOnInit() {

    this.isQueryEditorVisible = false;
    this.userId = this.userInfoService.getUserDetails().UserId;
    this.userName = this.userInfoService.getUserDetails().LoginName;
    this.userRoles = this.userInfoService.getUserDetails().Roles;
    this.ruleSetDetail = this.ruleConfigService.getRuleSetObj;
    this.sourceSubEntityList = this.ruleConfigService.getSourceSubEntityList;
    this.targetSubEntityList = this.ruleConfigService.getTargetSubEntityList;

    //this.ngxLoader.start();

    this.ruleConfigService.getOperandTypeList()
      .pipe(takeUntil(this.onDestroy$))
      .subscribe(responseData => {
        this.operandTypeList = responseData;
      });

    this.ruleConfigService.getQueryDatabaseList()
      .pipe(takeUntil(this.onDestroy$))
      .subscribe(responseData => {
        this.queryDatabaseList = responseData;
      });

    this.ruleConfigService.getRuleActionTypes()
      .pipe(takeUntil(this.onDestroy$))
      .subscribe(responseData => {
        this.ruleActionTypeList = responseData;
      });

    this.ruleList = this.ruleSetDetail.Rules ? this.ruleSetDetail.Rules : [];
    this.ruleExpressionList = this.ruleSetDetail.RuleExpressions ? this.ruleSetDetail.RuleExpressions : [];
    this.ruleExpFuncParameterList = this.ruleSetDetail.RuleExpressionFunctionParameters ? this.ruleSetDetail.RuleExpressionFunctionParameters : [];
    this.ruleActionList = this.ruleSetDetail.RuleActions ? this.ruleSetDetail.RuleActions : [];
    this.actionExpressionList = this.ruleSetDetail.RuleActionExpressions ? this.ruleSetDetail.RuleActionExpressions : [];
    this.actionSubExpressionList = this.ruleSetDetail.RuleActionSubExpressions ? this.ruleSetDetail.RuleActionSubExpressions : [];

    this.ruleConfigService.getLogicalOperators()
      .pipe(takeUntil(this.onDestroy$))
      .subscribe(responseData => {
        this.logicalOperatorList = responseData;
      });
    this.ruleConfigService.getAssignmentOperators()
      .pipe(takeUntil(this.onDestroy$))
      .subscribe(responseData => {
        this.assignmentOperatorList = responseData;
      });

    this.ruleExpressionList.forEach(expression => {
      this.setSubEntityList(expression.LeftSubEntityDetail, this.sourceSubEntityList);
      this.setSubEntityList(expression.RightSubEntityDetail, this.sourceSubEntityList);

      let leftOperandDataTypeId = this.getExpressionLeftOperandDataType(expression);

      if (expression.LeftOpTypeId == this.operandTypeConst.Value) {
        this.setOperatorForDataType(leftOperandDataTypeId);
      }
      else {
        if (expression.LeftSubEntityDetail) {
          this.setFieldListForEntity(expression.LeftSubEntityDetail.SubEntityId);
          this.setOperatorForDataType(leftOperandDataTypeId);
        }
        if (expression.LeftOperandFieldDetail) {
          this.setMasterDataSourceForField(expression.LeftOperandFieldDetail.FieldId);
        }
      }

      if (expression.RightSubEntityDetail)
        this.setFieldListForEntity(expression.RightSubEntityDetail.SubEntityId);
      if (expression.RightOpTypeId == this.operandTypeConst.Function) {
        this.setFunctionListForDataType(leftOperandDataTypeId);
      }

    });
    this.actionExpressionList.forEach(actionExp => {
      this.setSubEntityList(actionExp.LeftSubEntityDetail, this.targetSubEntityList);
      this.setSubEntityList(actionExp.RightSubEntityDetail, this.sourceSubEntityList);
      if (actionExp.LeftSubEntityDetail) {
        this.setFieldListForEntity(actionExp.LeftSubEntityDetail.SubEntityId);
      }
      if (actionExp.LeftOperandFieldDetail) {
        this.setMasterDataSourceForField(actionExp.LeftOperandFieldDetail.FieldId);
      }

      if (actionExp.RightSubEntityDetail)
        this.setFieldListForEntity(actionExp.RightSubEntityDetail.SubEntityId);
      if (actionExp.RightOpTypeId == 3)
        this.setFunctionListForDataType(actionExp.LeftOperandFieldDetail.DataTypeId);
    });

    this.ruleExpFuncParameterList.forEach(funcParam => {
      this.setSubEntityList(funcParam.SubEntityDetail, this.sourceSubEntityList);
    });

    this.actionSubExpressionList.forEach(actionSubExp => {
      this.setSubEntityList(actionSubExp.RightSubEntityDetail, this.sourceSubEntityList);
    });

    this.sourceSubEntityList.forEach(item => {
      if (this.targetSubEntityList.findIndex(targetItem => targetItem.SubEntityId == item.SubEntityId) == -1) {
        this.targetSubEntityList.push(item);
      }
    });

    if (this.sourceSubEntityList && this.sourceSubEntityList.length > 0) {
      this.defaultSourceEntityItem = this.sourceSubEntityList[0];
      this.setFieldListForEntity(this.sourceSubEntityList[0].SubEntityId);
    }
    if (this.targetSubEntityList && this.targetSubEntityList.length > 0) {
      this.defaultTargetEntityItem = this.targetSubEntityList[0];
      this.setFieldListForEntity(this.targetSubEntityList[0].SubEntityId);
    }

    if (!this.ruleList || this.ruleList.length == 0) {
      this.generateNewRuleRecord();
    }
    if (this.ruleList.length == 1 && this.ruleConfigService.getSelectedRuleConfigurationType == 2) {
      this.generateNewRuleRecord();
    }



    //this.ngxLoader.stop();
  }

  generateNewRuleRecord() {
    let subArray = this.ruleList.slice(-1);
    let newRuleIndex = subArray.length > 0 ? subArray[0].RuleId + 1 : 1;
    this.ruleList.push(this.getDefaultRuleRecord(newRuleIndex));
    this.generateNewRuleRecordExpression(newRuleIndex);
    this.generateNewRuleRecordAction(newRuleIndex);
  }

  generateNewRuleRecordExpression(ruleId: number) {
    let subArray = this.ruleExpressionList.slice(-1);
    let newExpressionId = subArray.length > 0 ? subArray[0].ExpressionId + 1 : 1;
    this.ruleExpressionList.push(this.getDefaultRuleExpressionRecord(ruleId, newExpressionId));
  }

  generateNewRuleExpFunctionParameterRecord(ruleExpressionId: number) {
    let subArray = this.ruleExpFuncParameterList.slice(-1);
    let newParameterDetailId = subArray.length > 0 ? subArray[0].ParameterDetailId + 1 : 1;
    this.ruleExpFuncParameterList.push(this.getDefaultRuleExpFuncParameterRecord(ruleExpressionId, newParameterDetailId));
  }

  generateNewRuleRecordAction(ruleId: number) {
    let subArray = this.ruleActionList.slice(-1);
    let newActionId = subArray.length > 0 ? subArray[0].ActionId + 1 : 1;
    this.ruleActionList.push(this.getDefaultRuleActionRecord(ruleId, newActionId));
    this.generateNewActionExpression(newActionId);
  }

  generateNewActionExpression(actionId: number) {
    let subArray = this.actionExpressionList.slice(-1);
    let newExpressionId = subArray.length > 0 ? subArray[0].ExpressionId + 1 : 1;
    this.actionExpressionList.push(this.getDefaultActionExpressionRecord(actionId, newExpressionId));
  }

  generateNewActionSubExpression(actionExpressionId: number) {
    let subArray = this.actionSubExpressionList.slice(-1);
    let newSubExpressionId = subArray.length > 0 ? subArray[0].SubExpressionId + 1 : 1;
    this.actionSubExpressionList.push(this.getDefaultActionSubExpressionRecord(actionExpressionId, newSubExpressionId));
  }

  removeRuleRecord(ruleId) {
    let ruleIndex = this.ruleList.findIndex(exp => exp.RuleId == ruleId);
    if (ruleIndex > -1) {
      this.ruleList.splice(ruleIndex, 1);
      this.ruleExpressionList = this.ruleExpressionList.filter(item => item.RuleId != ruleId);
      this.ruleExpFuncParameterList = this.ruleExpFuncParameterList.filter(item => {
        return this.ruleExpressionList.findIndex(ruleExp => ruleExp.ExpressionId == item.ExpressionId) > -1;
      });

      // iterating in rule action list and removing all action for current rule being removed
      this.ruleActionList.forEach(action => {
        if (action.RuleId == ruleId) {
          this.removeActionRecord(action.ActionId);
        }
      });
    }
  }

  removeRuleExpRecord(expressionId, lastExpressionObject: ruleExpressionModel, isLastExpressions: boolean) {
    let expIndex = this.ruleExpressionList.findIndex(exp => exp.ExpressionId == expressionId);
    if (expIndex > -1) {
      this.ruleExpressionList.splice(expIndex, 1);
      this.ruleExpFuncParameterList = this.ruleExpFuncParameterList.filter(item => item.ExpressionId != expressionId);
    }

    if (lastExpressionObject && isLastExpressions && isLastExpressions == true) {
      lastExpressionObject.NextOperatorDetail = null;
    }
  }

  removeFunctionParameterRecord(
    parameterDetailId: number,
    ruleExpId: number,
    lastExpFuncParamterDetail: ruleExpressionFunctionParameters,
    isLastRecord: boolean
  ) {
    let parameterIndex = this.ruleExpFuncParameterList.findIndex(currentParam => currentParam.ParameterDetailId == parameterDetailId);
    if (parameterIndex > -1) {
      this.ruleExpFuncParameterList.splice(parameterIndex, 1);
    }
    if (this.ruleExpFuncParameterList.findIndex(exp => exp.ExpressionId == ruleExpId) == -1) {
      let currentExpression = this.ruleExpressionList.find(exp => exp.ExpressionId == ruleExpId);
      currentExpression.RightOpFunctionDetail = null;
      currentExpression.AssignmentOperatorDetail = null;
    }

    if (lastExpFuncParamterDetail && isLastRecord && isLastRecord == true) {
      lastExpFuncParamterDetail.AssignmentOperatorDetail = null;
    }
  }


  removeActionRecord(actionId) {
    let actionIndex = this.ruleActionList.findIndex(exp => exp.ActionId == actionId);
    if (actionId > -1) {
      this.ruleActionList.splice(actionIndex, 1);
      this.actionExpressionList = this.actionExpressionList.filter(exp => exp.ActionId != actionId);
      this.actionSubExpressionList = this.actionSubExpressionList.filter(subExp => this.actionExpressionList.findIndex(exp => exp.ExpressionId == subExp.ExpressionId) > -1);
    }
  }

  removeActionExpRecord(expressionId, lastExpressionObject: actionExpressionModel, isLastExpressions: boolean) {
    let expIndex = this.actionExpressionList.findIndex(exp => exp.ExpressionId == expressionId);
    if (expIndex > -1) {
      this.actionExpressionList.splice(expIndex, 1);
      this.actionSubExpressionList = this.actionSubExpressionList.filter(subExp => subExp.ExpressionId != expressionId);
    }

    if (isLastExpressions && lastExpressionObject && isLastExpressions == true) {
      lastExpressionObject.AssignmentOperatorDetail = null;
    }
  }

  removeActionSubExpRecord(subExpressionId: number, parentExpId: number, lastSubExpressionObject: actionSubExpressionModel, isLastRecord: boolean) {
    let expIndex = this.actionSubExpressionList.findIndex(exp => exp.SubExpressionId == subExpressionId);
    if (expIndex > -1) {
      this.actionSubExpressionList.splice(expIndex, 1);
    }
    if (this.actionSubExpressionList.findIndex(exp => exp.ExpressionId == parentExpId) == -1) {
      let currentExpression = this.actionExpressionList.find(exp => exp.ExpressionId == parentExpId);
      currentExpression.AssignmentOperatorDetail = null;
      currentExpression.RightOpFunctionDetail = null;
    }
    if (isLastRecord && lastSubExpressionObject && isLastRecord == true) {
      lastSubExpressionObject.AssignmentOperatorDetail = null;
    }
  }

  onLeftOpTypeChange(event: any, expressionId: number) {
    let operandTypeId = event.target.value;
    let currentExpression = this.ruleExpressionList.find(exp => { return exp.ExpressionId == expressionId });
    currentExpression.LeftOpTypeId = operandTypeId;
    currentExpression.LeftOpValue = '';
    currentExpression.LeftSubEntityDetail = null;
    currentExpression.LeftOperandFieldDetail = null;

    if (operandTypeId == this.operandTypeConst.Value) {
      this.setOperatorForDataType(this.dataTypeConst.Alphanumeric);
      this.setFunctionListForDataType(this.dataTypeConst.Alphanumeric);
    }
  }

  onLeftOpFocusOut(event: any, expressionId: number) {
    let leftOperandValue = '';
    let value = event.target.value;
    if (value && value != '')
      leftOperandValue = value;

    let currentExpression = this.ruleExpressionList.find(exp => { return exp.ExpressionId == expressionId });
    currentExpression.LeftOpValue = leftOperandValue;
  }

  onLeftEntitySelected(selectedEntityId: number, expressionId) {
    let expIndex = this.ruleExpressionList.findIndex(exp => { return exp.ExpressionId == expressionId });
    if (!selectedEntityId || selectedEntityId == 0) {
      this.ruleExpressionList[expIndex].LeftSubEntityDetail = null;
      return;
    }

    // storing list of observable  entityid wise, to reuse to get fields 
    this.setFieldListForEntity(selectedEntityId);
    this.ruleExpressionList[expIndex].LeftSubEntityDetail = this.sourceSubEntityList.find(entity => { return entity.SubEntityId == selectedEntityId });
  }

  onLeftFieldSelected(selectedFieldId, expressionId) {
    let currentExpression = this.ruleExpressionList.find(exp => { return exp.ExpressionId == expressionId });
    if (!selectedFieldId || selectedFieldId == 0) {
      currentExpression.LeftOperandFieldDetail = null;
      return;
    }

    let selectedField = this.getEntityFieldDetail(currentExpression.LeftSubEntityDetail.SubEntityId, selectedFieldId);
    this.setOperatorForDataType(selectedField.DataTypeId);
    this.setAllowedPatternForDataType(selectedField.DataTypeId);
    this.setFunctionListForDataType(selectedField.DataTypeId);
    if (selectedField && selectedField.IsSelectable == true) {
      this.setMasterDataSourceForField(selectedField.FieldId);
    }

    currentExpression.LeftOperandFieldDetail = selectedField;
  }

  onRightEntitySelected(selectedEntityId: number, expressionId: number) {
    let expIndex = this.ruleExpressionList.findIndex(exp => { return exp.ExpressionId == expressionId });
    if (!selectedEntityId || selectedEntityId == 0) {
      this.ruleExpressionList[expIndex].RightSubEntityDetail = null;
      return;
    }

    //storing list of observable  entityid wise, to reuse to get fields 
    this.setFieldListForEntity(selectedEntityId);

    this.ruleExpressionList[expIndex].RightSubEntityDetail = this.sourceSubEntityList.find(entity => { return entity.SubEntityId == selectedEntityId });
  }

  onRightFieldSelected(selectedFieldId, expressionId) {
    let currentExpression = this.ruleExpressionList.find(exp => { return exp.ExpressionId == expressionId });
    let selectedField = this.getEntityFieldDetail(currentExpression.RightSubEntityDetail.SubEntityId, selectedFieldId);
    currentExpression.RightOperandFieldDetail = selectedField;
  }

  onCompOperatorSelected(event: any, expressionId) {
    let selectedOperatorId = ((event.target.value.split(':')[1]) && (event.target.value.split(':')[1]).trim());
    let currentExpression = this.ruleExpressionList.find(exp => { return exp.ExpressionId == expressionId });
    if (!selectedOperatorId || selectedOperatorId == 0) {
      currentExpression.ComparisonOperatorDetail = null;
      return;
    }
    let dataTypeId = this.getExpressionLeftOperandDataType(currentExpression);
    let selectedOperatorDetail = this.operatorListForDataType[dataTypeId].find(item => { return item.OperatorId == selectedOperatorId });

    currentExpression.ComparisonOperatorDetail = selectedOperatorDetail;
  }

  onRightOpTypeChange(event: any, expressionId: number) {
    let operandTypeId = event.target.value;
    let currentExpression = this.ruleExpressionList.find(exp => { return exp.ExpressionId == expressionId });
    currentExpression.RightOpTypeId = operandTypeId;

    currentExpression.RightOpValue = '';
    currentExpression.RightSubEntityDetail = null;
    currentExpression.RightOpFunctionDetail = null;
    currentExpression.RightOperandFieldDetail = null;
    currentExpression.AssignmentOperatorDetail = null;

    this.ruleExpFuncParameterList = this.ruleExpFuncParameterList.filter(item => { return item.ExpressionId != expressionId });

    this.checkAndSetRuleActionType(currentExpression.RuleId);

    if (operandTypeId == this.operandTypeConst.Function) {
      let leftOperandDataTypeId = this.getExpressionLeftOperandDataType(currentExpression);
      this.setFunctionListForDataType(leftOperandDataTypeId);
    }
  }

  onRightOpFocusOut(event: any, valueArray: Array<any>, expressionId: number, appendValue: boolean) {
    let rightOperandValue = '';
    let value = event.target.value;
    if (value && value != '') {
      rightOperandValue = value;
    }
    else if (valueArray && valueArray.length > 0) {
      rightOperandValue = this.convertArrayToString(valueArray);
    }
    appendValue = appendValue ? appendValue : false;
    let currentExpression = this.ruleExpressionList.find(exp => { return exp.ExpressionId == expressionId });
    if (appendValue == true) {
      currentExpression.RightOpValue = currentExpression.RightOpValue ? currentExpression.RightOpValue.split(',')[0] + ',' + rightOperandValue : rightOperandValue;
    }
    else {
      currentExpression.RightOpValue = rightOperandValue;
    }
  }

  onFunctionSelected(event: any, expressionId: number) {
    let selectedFunctionId = event.target.value;
    let currentExpression = this.ruleExpressionList.find(exp => { return exp.ExpressionId == expressionId });
    if (!selectedFunctionId || selectedFunctionId == 0) {
      currentExpression.RightOpFunctionDetail = null;
      return;
    }
    let lefOperandDataTypeId = this.getExpressionLeftOperandDataType(currentExpression);
    let selectedFunctionDetail = this.functionListForDataType[lefOperandDataTypeId].find(item => { return item.FunctionId == selectedFunctionId });

    currentExpression.RightOpFunctionDetail = selectedFunctionDetail;
    this.ruleExpFuncParameterList = this.ruleExpFuncParameterList.filter(item => { return item.ExpressionId != expressionId });
    if (selectedFunctionDetail.HasParameter == true) {
      this.generateNewRuleExpFunctionParameterRecord(expressionId);
    }

    this.checkAndSetRuleActionType(currentExpression.RuleId);
  }

  onRuleExpAssignmentOperatorChange(event: any, expressionId: number) {
    let selectedOperatorId = event.target.value;
    let currentExpression = this.ruleExpressionList.find(exp => { return exp.ExpressionId == expressionId });
    if (!selectedOperatorId || selectedOperatorId == 0) {
      currentExpression.AssignmentOperatorDetail = null;
      return;
    }

    let selectedOperatorDetail = this.assignmentOperatorList.find(item => { return item.OperatorId == selectedOperatorId });

    currentExpression.AssignmentOperatorDetail = selectedOperatorDetail;

    if (this.ruleExpFuncParameterList.findIndex(exp => { return exp.ExpressionId == expressionId }) == -1) {
      this.generateNewRuleExpFunctionParameterRecord(expressionId);
    }
  }

  onFuncParameterTypeChange(event: any, parameterDetailId: number) {
    let operandTypeId = event.target.value;
    let currentParameterDetail = this.ruleExpFuncParameterList.find(exp => { return exp.ParameterDetailId == parameterDetailId });
    currentParameterDetail.ParameterTypeId = operandTypeId;
    if (operandTypeId == RC_OPERANDTYPE.Value) {
      currentParameterDetail.SubEntityDetail = null;
      currentParameterDetail.FieldDetail = null;
    }
    else if (operandTypeId == RC_OPERANDTYPE.Reference) {
      currentParameterDetail.ParameterValue = '';
    }
  }

  onFunctionParameterValueFocusOut(event: any, parameterDetailId: number) {
    let value = event.target.value;
    if (!value)
      value = '';
    let currentParameterDetai = this.ruleExpFuncParameterList.find(exp => { return exp.ParameterDetailId == parameterDetailId });
    currentParameterDetai.ParameterValue = value;
  }

  onFunctionParameterSubEntitySelected(selectedEntityId: number, parameterDetailId: number) {
    let currentParameterIndex = this.ruleExpFuncParameterList.findIndex(exp => { return exp.ParameterDetailId == parameterDetailId });
    if (!selectedEntityId || selectedEntityId == 0) {
      this.ruleExpFuncParameterList[currentParameterIndex].SubEntityDetail = null;
      return;
    }

    //storing list of fileds entityid wise, to reuse to get fields 
    this.setFieldListForEntity(selectedEntityId);

    this.ruleExpFuncParameterList[currentParameterIndex].SubEntityDetail = this.sourceSubEntityList.find(entity => { return entity.SubEntityId == selectedEntityId });
  }

  onFunctionParameterFieldSelected(selectedFieldId: number, parameterDetailId: number) {
    let currentParameterDetail = this.ruleExpFuncParameterList.find(exp => { return exp.ParameterDetailId == parameterDetailId });
    let selectedField = this.getEntityFieldDetail(currentParameterDetail.SubEntityDetail.SubEntityId, selectedFieldId);
    currentParameterDetail.FieldDetail = selectedField;
  }

  onFuncParamAssignmentOperatorChange(event: any, expressionId: number, parameterDetailId: number) {
    let selectedOperatorId = event.target.value;
    let currentFuncParameter = this.ruleExpFuncParameterList.find(exp => { return exp.ParameterDetailId == parameterDetailId });
    if (!selectedOperatorId || selectedOperatorId == 0) {
      currentFuncParameter.AssignmentOperatorDetail = null;
      return;
    }
    let selectedOperatorDetail = this.assignmentOperatorList.find(item => { return item.OperatorId == selectedOperatorId });

    currentFuncParameter.AssignmentOperatorDetail = selectedOperatorDetail;

    if (this.ruleExpFuncParameterList.findIndex(exp => { return exp.ExpressionId == expressionId && exp.ParameterDetailId > parameterDetailId }) == -1) {
      this.generateNewRuleExpFunctionParameterRecord(expressionId);
    }
  }

  onNextOperatorChange(event: any, ruleId: number, expressionId: number) {
    let selectedOperatorId = event.target.value;
    let currentExpression = this.ruleExpressionList.find(exp => { return exp.ExpressionId == expressionId });
    if (!selectedOperatorId || selectedOperatorId == 0) {
      currentExpression.NextOperatorDetail = null;
      return;
    }
    let selectedOperatorDetail = this.logicalOperatorList.find(item => { return item.OperatorId == selectedOperatorId });


    currentExpression.NextOperatorDetail = selectedOperatorDetail;

    if (this.ruleExpressionList.findIndex(exp => { return exp.RuleId == ruleId && exp.ExpressionId > expressionId }) == -1) {
      this.generateNewRuleRecordExpression(ruleId);
    }
  }

  //#region Rule Action Methods

  onActionLeftEntitySelected(selectedEntityId: number, actionExpId) {
    let expIndex = this.actionExpressionList.findIndex(exp => { return exp.ExpressionId == actionExpId });
    if (!selectedEntityId || selectedEntityId == 0) {
      this.actionExpressionList[expIndex].LeftSubEntityDetail = null;
      return;
    }

    //storing list of observable  entityid wise, to reuse to get fields 
    this.setFieldListForEntity(selectedEntityId);
    this.actionExpressionList[expIndex].LeftSubEntityDetail = this.targetSubEntityList.find(entity => { return entity.SubEntityId == selectedEntityId });
  }

  onActionLeftFieldSelected(selectedFieldId, actionExpId) {
    let currentExpression = this.actionExpressionList.find(exp => { return exp.ExpressionId == actionExpId });
    if (!selectedFieldId || selectedFieldId == 0) {
      currentExpression.LeftOperandFieldDetail = null;
      return;
    }

    let selectedField = this.getEntityFieldDetail(currentExpression.LeftSubEntityDetail.SubEntityId, selectedFieldId);
    this.setFunctionListForDataType(selectedField.DataTypeId);
    if (selectedField && selectedField.IsSelectable == true) {
      this.setMasterDataSourceForField(selectedField.FieldId);
    }
    currentExpression.LeftOperandFieldDetail = selectedField;
  }

  onActionRightEntitySelected(selectedEntityId, actionExpId) {
    let expIndex = this.actionExpressionList.findIndex(exp => { return exp.ExpressionId == actionExpId });
    if (!selectedEntityId || selectedEntityId == 0) {
      this.actionExpressionList[expIndex].RightSubEntityDetail = null;
      return;
    }

    //storing list of observable  entityid wise, to reuse to get fields 
    this.setFieldListForEntity(selectedEntityId);
    this.actionExpressionList[expIndex].RightSubEntityDetail = this.sourceSubEntityList.find(entity => { return entity.SubEntityId == selectedEntityId });
  }

  onSubExpRightEntitySelected(selectedEntityId, subExpId) {
    let subExpIndex = this.actionSubExpressionList.findIndex(exp => { return exp.SubExpressionId == subExpId });
    if (!selectedEntityId || selectedEntityId == 0) {
      this.actionSubExpressionList[subExpIndex].RightOpFunctionDetail = null;
      return;
    }

    //storing list of observable  entityid wise, to reuse to get fields 
    this.setFieldListForEntity(selectedEntityId);
    this.actionSubExpressionList[subExpIndex].RightSubEntityDetail = this.sourceSubEntityList.find(entity => { return entity.SubEntityId == selectedEntityId });
  }

  onActionRightFieldSelected(selectedFieldId, actionExpId) {
    let currentExpression = this.actionExpressionList.find(exp => { return exp.ExpressionId == actionExpId });
    let selectedField = this.getEntityFieldDetail(currentExpression.RightSubEntityDetail.SubEntityId, selectedFieldId);
    currentExpression.RightOperandFieldDetail = selectedField;
  }
  onSubExpRightFieldSelected(selectedFieldId, subExpId) {
    let currentSubExpression = this.actionSubExpressionList.find(exp => { return exp.SubExpressionId == subExpId });
    let selectedField = this.getEntityFieldDetail(currentSubExpression.RightSubEntityDetail.SubEntityId, selectedFieldId);
    currentSubExpression.RightOperandFieldDetail = selectedField;
  }

  onActionRightOpTypeChange(event: any, actionExpId: number) {
    let operandTypeId = event.target.value;
    let currentExpression = this.actionExpressionList.find(exp => { return exp.ExpressionId == actionExpId });
    currentExpression.RightOpTypeId = operandTypeId;
    if (operandTypeId == RC_OPERANDTYPE.Value) {
      currentExpression.RightSubEntityDetail = null;
      currentExpression.RightOpFunctionDetail = null;
      currentExpression.RightOperandFieldDetail = null;
    }
    else if (operandTypeId == RC_OPERANDTYPE.Reference) {
      currentExpression.RightOpFunctionDetail = null;
      currentExpression.RightOpValue = '';
    }
    else if (operandTypeId == RC_OPERANDTYPE.Function) {
      currentExpression.RightSubEntityDetail = null;
      currentExpression.RightOpValue = '';
      currentExpression.RightOperandFieldDetail = null;
      this.setFunctionListForDataType(currentExpression.LeftOperandFieldDetail.DataTypeId);
    }

    this.actionSubExpressionList = this.actionSubExpressionList.filter(item => { return item.ExpressionId != actionExpId });
    currentExpression.AssignmentOperatorDetail = null;
  }

  onSubExpOperandTypeChange(event: any, subExpId: number, parentExpId: number) {
    let operandTypeId = event.target.value;
    let parentExpression = this.actionExpressionList.find(exp => { return exp.ExpressionId == parentExpId });
    let currentSubExpression = this.actionSubExpressionList.find(exp => { return exp.SubExpressionId == subExpId });
    currentSubExpression.RightOpTypeId = operandTypeId;
    if (operandTypeId == RC_OPERANDTYPE.Value) {
      currentSubExpression.RightSubEntityDetail = null;
      currentSubExpression.RightOpFunctionDetail = null;
      currentSubExpression.RightOperandFieldDetail = null;
    }
    else if (operandTypeId == RC_OPERANDTYPE.Reference) {
      currentSubExpression.RightOpFunctionDetail = null;
      currentSubExpression.RightOpValue = '';
    }
    else if (operandTypeId == RC_OPERANDTYPE.Function) {
      currentSubExpression.RightSubEntityDetail = null;
      currentSubExpression.RightOpValue = '';
      currentSubExpression.RightOperandFieldDetail = null;
      this.setFunctionListForDataType(parentExpression.LeftOperandFieldDetail.DataTypeId);
    }
  }

  onActionRightOpFocusOut(event: any, valueArray: Array<any>, actionExpId: number) {
    let value = event.target.value;
    let currentExpression = this.actionExpressionList.find(exp => { return exp.ExpressionId == actionExpId });
    currentExpression.RightOpValue = value;
  }

  onSubExpRightOpFocusOut(event: any, subExpressionId: number) {
    let value = event.target.value;
    let currentSubExpression = this.actionSubExpressionList.find(exp => { return exp.SubExpressionId == subExpressionId });
    currentSubExpression.RightOpValue = value;
  }


  onActionFunctionSelected(event: any, actionExpId: number) {
    let selectedFunctionId = event.target.value;
    let currentExpression = this.actionExpressionList.find(exp => { return exp.ExpressionId == actionExpId });
    this.actionSubExpressionList = this.actionSubExpressionList.filter(item => { return item.ExpressionId != actionExpId });
    if (!selectedFunctionId || selectedFunctionId == 0) {
      currentExpression.RightOpFunctionDetail = null;
      return;
    }
    let selectedFunctionDetail = this.functionListForDataType[currentExpression.LeftOperandFieldDetail.DataTypeId].find(item => { return item.FunctionId == selectedFunctionId });

    currentExpression.RightOpFunctionDetail = selectedFunctionDetail;

    if (selectedFunctionDetail.HasParameter == true) {
      this.generateNewActionSubExpression(actionExpId);
    }
  }

  onSubExpFunctionSelected(selectedFunctionId, subExpId: number, parentExpId: number) {
    let parentExpression = this.actionExpressionList.find(exp => { return exp.ExpressionId == parentExpId });
    let currentSubExpression = this.actionSubExpressionList.find(exp => { return exp.SubExpressionId == subExpId });
    if (!selectedFunctionId || selectedFunctionId == 0) {
      currentSubExpression.RightOpFunctionDetail = null;
      return;
    }
    let selectedFunctionDetail = this.functionListForDataType[parentExpression.LeftOperandFieldDetail.DataTypeId].find(item => { return item.FunctionId == selectedFunctionId });

    currentSubExpression.RightOpFunctionDetail = selectedFunctionDetail;
  }

  onActionAssignmentOperatorChange(event: any, actionId: number, actionExpId: number) {
    let selectedOperatorId = event.target.value;
    let currentExpression = this.actionExpressionList.find(exp => { return exp.ExpressionId == actionExpId });
    if (!selectedOperatorId || selectedOperatorId == 0) {
      currentExpression.AssignmentOperatorDetail = null;
      return;
    }
    else if (!currentExpression.LeftOperandFieldDetail || currentExpression.LeftOperandFieldDetail.FieldId == 0) {
      this.toastr.warningMessage(this.appMessageService.getMessageByKey(messageConstant.RC_ACTIONEXPRESSIONLEFTOPERANDMISSING));
      currentExpression.AssignmentOperatorDetail = null;
      return;
    }

    let selectedOperatorDetail = this.assignmentOperatorList.find(item => { return item.OperatorId == selectedOperatorId });

    currentExpression.AssignmentOperatorDetail = selectedOperatorDetail;

    if (this.actionSubExpressionList.findIndex(exp => { return exp.ExpressionId == actionExpId }) == -1) {
      // && exp.ExpressionId > actionExpId
      this.generateNewActionSubExpression(actionExpId);
    }
  }

  onSubExpAssignmentOperatorChange(event: any, parentExpId: number, subExpId: number) {
    let selectedOperatorId = event.target.value;
    let currentSubExpression = this.actionSubExpressionList.find(exp => { return exp.SubExpressionId == subExpId });
    if (!selectedOperatorId || selectedOperatorId == 0) {
      currentSubExpression.AssignmentOperatorDetail = null;
      return;
    }
    let selectedOperatorDetail = this.assignmentOperatorList.find(item => { return item.OperatorId == selectedOperatorId });

    currentSubExpression.AssignmentOperatorDetail = selectedOperatorDetail;

    if (this.actionSubExpressionList.findIndex(exp => { return exp.ExpressionId == parentExpId && exp.SubExpressionId > subExpId }) == -1) {
      // && exp.ExpressionId > actionExpId
      this.generateNewActionSubExpression(parentExpId);
    }
  }
  //#endregion

  createRule() {
    this.toastr.clear();
    if (this.ruleList.length === 0) {
      this.toastr.errorMessage(this.appMessageService.getMessageByKey(messageConstant.RC_RULECOLLECTIONEMPTY));
      return;
    }
    else if (this.ruleExpressionList.length == 0) {
      this.toastr.errorMessage(this.appMessageService.getMessageByKey(messageConstant.RC_RULEEXPRESSIONEMPTY));
      return;
    }
    else if (this.ruleExpressionList.findIndex(exp => { return (!exp.LeftOpValue && exp.LeftOpValue == '') && (!exp.LeftOperandFieldDetail || exp.LeftOperandFieldDetail.FieldId <= 0) }) > -1) {
      this.toastr.errorMessage(this.appMessageService.getMessageByKey(messageConstant.RC_RULEEXPRESSIONINCOMPLETE));
      return;
    }
    else if (this.actionExpressionList.length == 0) {
      this.toastr.errorMessage(this.appMessageService.getMessageByKey(messageConstant.RC_RULEEXPRESSIONEMPTY));
      return;
    }
    else if (this.actionExpressionList.findIndex(exp => { return (!exp.RightOpValue && exp.RightOpValue == '') && (!exp.RightOperandFieldDetail || exp.RightOperandFieldDetail.FieldId <= 0) }) > -1) {
      this.toastr.errorMessage(this.appMessageService.getMessageByKey(messageConstant.RC_RULEEXPRESSIONINCOMPLETE));
      return;
    }

    else {

      this.ruleDetailSubmitted = true;
      //this.ngxLoader.start();
      this.ruleSetDetail.CreatorDetail = new creatorModel();
      this.ruleSetDetail.CreatorDetail.CreatedBy = this.userId;
      this.ruleSetDetail.CreatorDetail.CreatedOn = new Date(this.globalService.appUserDate());
      this.ruleSetDetail.CreatorDetail.CreatedByIP = '0.0.0.0';
      this.ruleSetDetail.Rules = this.ruleList;
      this.ruleSetDetail.RuleExpressions = this.ruleExpressionList;
      this.ruleSetDetail.RuleExpressionFunctionParameters = this.ruleExpFuncParameterList;
      this.ruleSetDetail.RuleActions = this.ruleActionList;
      this.ruleSetDetail.RuleActionExpressions = this.actionExpressionList;
      this.ruleSetDetail.RuleActionSubExpressions = this.actionSubExpressionList;
      let strMessageKey = this.ruleSetDetail.RuleSetId && this.ruleSetDetail.RuleSetId > 0 ? messageConstant.RULEUPDATED : messageConstant.RULEINSERTED;
      this.ruleConfigService.createRuleSet(this.ruleSetDetail).pipe(takeUntil(this.onDestroy$))
        .subscribe(
          data => {
            //this.ngxLoader.stop();
            if (data != null && data != 'undefined') {
              this.toastr.successMessage(this.appMessageService.getMessageByKey(strMessageKey));
              this.router.navigate(['rule-conf/rule-list']);
            }
            else {
              this.toastr.errorMessage(this.appMessageService.getMessageByKey(messageConstant.ERRORMESSAGE));
            }
          },
          error => {
            //this.ngxLoader.stop();
            this.toastr.errorMessage(this.appMessageService.getMessageByKey(messageConstant.TRYAGAINERROR));
          });
    }
    this.ruleDetailSubmitted = false;
  }

  getComparisonOperatorCollection(operandTypeId: number, entityDetail: subEntityModel, selectedFieldDetail: entityFieldModel): Array<operatorModel> {
    let dataTypeId = operandTypeId == this.operandTypeConst.Value ? this.dataTypeConst.Alphanumeric : selectedFieldDetail.DataTypeId;
    if (!entityFieldModel || !this.operatorListForDataType[dataTypeId])
      return [];

    let skipOperatorId = 0;
    if (operandTypeId == this.operandTypeConst.Reference) {
      let currentEntityFieldDetail = this.getEntityFieldDetail(entityDetail.SubEntityId, selectedFieldDetail.FieldId);

      if (currentEntityFieldDetail && currentEntityFieldDetail.IsSelectable == false) {
        skipOperatorId = 11; //contains operator
      }
    }

    return this.operatorListForDataType[dataTypeId].filter(op => {
      return op.OperatorId != skipOperatorId;
    });
  }

  getDelimitedValueAtIndex(orgValue: string, index: number): string {
    if (!orgValue)
      return '';
    else if (orgValue.indexOf(',') == -1)
      return index == 0 ? orgValue : '';
    return orgValue.split(',')[index];
  }

  getDefaultRuleRecord(ruleId: number): ruleModel {
    return {
      RuleId: ruleId,
      Expression: '',
      ActionTypeId: 0
    };
  }

  getDefaultRuleExpressionRecord(ruleId: number, expressionId: number): ruleExpressionModel {
    return {
      ExpressionId: expressionId,
      RuleId: ruleId,
      ComparisonOperatorDetail: null,
      LeftOpTypeId: this.operandTypeConst.Reference,
      LeftOpValue: '',
      LeftSubEntityDetail: this.defaultSourceEntityItem ? this.defaultSourceEntityItem : null,
      LeftOperandFieldDetail: null,
      RightOpTypeId: this.operandTypeConst.Value,
      RightOpValue: '',
      RightOpFunctionDetail: null,
      RightSubEntityDetail: this.defaultSourceEntityItem ? this.defaultSourceEntityItem : null,
      RightOperandFieldDetail: null,
      AssignmentOperatorDetail: null,
      NextOperatorDetail: null,
      QueryDatabaseDetail: null
    };
  }

  getDefaultRuleExpFuncParameterRecord(ruleExpId: number, parameterDetailId: number): ruleExpressionFunctionParameters {
    return {
      ExpressionId: ruleExpId,
      ParameterDetailId: parameterDetailId,
      ParameterTypeId: this.operandTypeConst.Value,
      ParameterValue: '',
      SubEntityDetail: this.defaultTargetEntityItem ? this.defaultTargetEntityItem : null,
      FieldDetail: null,
      AssignmentOperatorDetail: null
    };
  }

  getDefaultRuleActionRecord(ruleId: number, actionId: number): ruleActionModel {
    return {
      ActionId: actionId,
      RuleId: ruleId
    };
  }

  getDefaultActionExpressionRecord(actionId: number, expressionId: number): actionExpressionModel {
    return {
      ExpressionId: expressionId,
      ActionId: actionId,
      LeftSubEntityDetail: this.defaultTargetEntityItem ? this.defaultTargetEntityItem : null,
      LeftOperandFieldDetail: null,
      RightOpTypeId: this.operandTypeConst.Value,
      RightOpValue: '',
      RightOpFunctionDetail: null,
      RightSubEntityDetail: this.defaultTargetEntityItem ? this.defaultTargetEntityItem : null,
      RightOperandFieldDetail: null,
      AssignmentOperatorDetail: null
    };
  }

  getDefaultActionSubExpressionRecord(actionExpId: number, subExpressionId: number): actionSubExpressionModel {
    return {
      ExpressionId: actionExpId,
      SubExpressionId: subExpressionId,
      RightOpTypeId: this.operandTypeConst.Value,
      RightOpValue: '',
      RightOpFunctionDetail: null,
      RightSubEntityDetail: this.defaultTargetEntityItem ? this.defaultTargetEntityItem : null,
      RightOperandFieldDetail: null,
      AssignmentOperatorDetail: null
    };
  }

  getExpressionList(ruleId: number): Array<ruleExpressionModel> {
    return this.ruleExpressionList.filter((currentExp) => {
      return currentExp.RuleId == ruleId;
    });
  }

  getRuleExpressionFunctionParameterList(expressionId: number): Array<ruleExpressionFunctionParameters> {
    return this.ruleExpFuncParameterList.filter((currentParameter) => {
      return currentParameter.ExpressionId == expressionId;
    });
  }

  getActionList(ruleId: number): Array<ruleActionModel> {
    return this.ruleActionList.filter((currentAct) => {
      return currentAct.RuleId == ruleId;
    });
  }


  getActionExpressionList(actionId: number): Array<actionExpressionModel> {
    return this.actionExpressionList.filter((currentExp) => {
      return currentExp.ActionId == actionId;
    });
  }

  getActionSubExpressionList(expressionId: number): Array<actionSubExpressionModel> {
    return this.actionSubExpressionList.filter((currentSubExp) => {
      return currentSubExp.ExpressionId == expressionId;
    });
  }

  setFieldListForEntity(subEntityId: number) {
    if (!subEntityId)
      return;
    if (!this.fieldListForEntity[subEntityId]) {
      this.ruleConfigService.getEntityFieldList(subEntityId).pipe(takeUntil(this.onDestroy$)).subscribe(fieldData => {
        this.fieldListForEntity[subEntityId] = fieldData;
      });
    }
  }

  setOperatorForDataType(dataTypeId: number) {
    if (!dataTypeId)
      return;

    if (!this.operatorListForDataType[dataTypeId]) {
      this.ruleConfigService.getComparisonOperators(dataTypeId).pipe(takeUntil(this.onDestroy$)).subscribe(operatorData => {
        this.operatorListForDataType[dataTypeId] = operatorData;
      });
    }
  }

  setAllowedPatternForDataType(dataTypeId: number) {
    if (!dataTypeId)
      return;

    if (!this.patternListForDataType[dataTypeId]) {
      switch (dataTypeId) {
        case 1:
          this.patternListForDataType[dataTypeId] = '^[0-9]*$';
          break;
        default:
          this.patternListForDataType[dataTypeId] = '';
          break;
      }
    }
  }

  setFunctionListForDataType(dataTypeId: number) {
    if (!dataTypeId)
      return;

    if (!this.functionListForDataType[dataTypeId] || this.functionListForDataType[dataTypeId].length == 0) {
      this.ruleConfigService.getPreDefinedFunctionList(dataTypeId)
        .pipe(takeUntil(this.onDestroy$))
        .subscribe(functionData => {
          this.functionListForDataType[dataTypeId] = functionData;
        });
    }
  }

  getFunctionListForDataType(dataTypeId: number, includeParameterized: boolean, includeAggregateFunction: boolean) {
    let returnData: any;
    if (!dataTypeId || this.functionListForDataType[dataTypeId] == undefined)
      return returnData;
    return this.functionListForDataType[dataTypeId].filter(p => {
      return (includeParameterized == true || p.HasParameter == false) && p.IsAggregate == includeAggregateFunction
    });
  }

  setMasterDataSourceForField(fieldId: number) {
    if (!fieldId)
      return;
    if (!this.masterDataListForField[fieldId] || this.masterDataListForField[fieldId].length == 0) {
      this.ruleConfigService.getMasterDataSourceList(fieldId, this.ruleSetDetail.LOBId).pipe(takeUntil(this.onDestroy$))
        .subscribe(masterData => {
          this.masterDataListForField[fieldId] = masterData;
        });
    }
  }

  setSubEntityList(entityDetail: subEntityModel, destEntityList: Array<subEntityModel>) {
    if (entityDetail && entityDetail.SubEntityId > 0 && destEntityList.findIndex(item => { return item.SubEntityId == entityDetail.SubEntityId }) == -1) {
      destEntityList.push(entityDetail);
    }
  }

  isFunctionOperandAllowed(dataTypeId: number) {
    if (!this.functionListForDataType[dataTypeId] || this.functionListForDataType[dataTypeId].length == 0) {
      return false;
    }

    return true;
  }

  isMasterSourceDataAllowed(entityId: number, fieldId: number) {
    let currentField = this.getEntityFieldDetail(entityId, fieldId);
    if (currentField)
      return currentField.IsSelectable;
    return false;
  }

  getEntityFieldDetail(entityId: number, fieldId: number) {
    if (!this.fieldListForEntity[entityId])
      return undefined;
    return this.fieldListForEntity[entityId].find(field => { return field.FieldId == fieldId });
  }

  convertStringToArray(valueString: string, expressionId: number): Array<string> {
    let valueArray: Array<any> = [];
    if (!valueString || valueString.length == 0)
      return valueArray;
    valueArray = valueString.split(',');
    return valueArray;
  }

  convertArrayToString(inputArray: Array<any>): string {
    if (!inputArray || inputArray.length == 0)
      return '';
    return inputArray.reduce((prevValue, currentValue) => {
      return (prevValue ? prevValue + ',' : '') + currentValue;
    }, null);
  }

  showQueryEditor(exprpessionId: number) {
    let currentExpression = this.ruleExpressionList.find(exp => { return exp.ExpressionId == exprpessionId });
    this.selectedExpressionQuery = currentExpression.RightOpValue;
    this.selectedRuleExpressionId = exprpessionId;
    this.selectedDatabaseDetail = currentExpression.QueryDatabaseDetail;
    this.isQueryEditorVisible = true;
  }

  hideQueryEditor() {
    this.selectedExpressionQuery = '';
    this.selectedRuleExpressionId = 0;
    this.selectedDatabaseDetail = null;
    this.isQueryEditorVisible = false;
  }

  onQueryDatabaseSelectionChange(event) {
    this.selectedDatabaseDetail = event.selectedItem;
  }

  onQuerySave() {
    let currentExpression = this.ruleExpressionList.find(exp => { return exp.ExpressionId == this.selectedRuleExpressionId });
    currentExpression.RightOpValue = this.selectedExpressionQuery;
    currentExpression.QueryDatabaseDetail = this.selectedDatabaseDetail;
    this.hideQueryEditor();
  }

  getExpressionLeftOperandDataType(expression: ruleExpressionModel) {
    if (expression.LeftOpTypeId == this.operandTypeConst.Value)
      return this.dataTypeConst.Alphanumeric;
    return expression.LeftOperandFieldDetail ? expression.LeftOperandFieldDetail.DataTypeId : 0;
  }

  checkAndSetRuleActionType(ruleId) {
    let hasAnyAggregateFunction = this.ruleExpressionList.findIndex(exp => {
      return exp.RuleId == ruleId && exp.RightOpFunctionDetail && exp.RightOpFunctionDetail.IsAggregate == true;
    })
    if (hasAnyAggregateFunction > -1) {
      this.ruleList.find(item => item.RuleId == ruleId).ActionTypeId = this.ruleActionTypeConst.AggregateOperation;
    }
    else {
      this.ruleList.find(item => item.RuleId == ruleId).ActionTypeId = 0;
    }
  }
  CloseButtonClicked() {
    this.router.navigate(['rule-conf/rule-list']);
  }
}
