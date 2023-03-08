import { Injectable } from '@angular/core';
import { endpointAction, endpointController } from 'src/app/Services/rule-engine/endpoint-configuration';
import { environment } from 'src/environments/environment';
import { GridSettings } from 'src/app/models/rule_engine/gridSettings';
import { ruleSetDetailModel, subEntityModel } from 'src/app/models/rule_engine/ruleConfigurationModel';
import { ApiService } from 'src/app/Services/rule-engine/api.service';

@Injectable({
  providedIn: 'root'
})
export class RuleConfigurationService {

  public BaseUrl: string;
  ruleSetDetail: ruleSetDetailModel;
  sourceSubEntities: Array<subEntityModel>;
  targetSubEntities: Array<subEntityModel>;
  ruleConfigurationTypeId: number;

  constructor(private apiService: ApiService) {
    this.BaseUrl = environment.ruleEngineApiUrl;

  }

  getRuleContextList() {
    return this.apiService.get(endpointController.RuleConfigurationController + endpointAction.GetRuleContextList);
  }

  getContextEntities(contextId: number) {
    return this.apiService.get(endpointController.RuleConfigurationController + endpointAction.GetContextEntities + '/' + contextId);
  }

  getRuleTypeList() {
    return this.apiService.get(endpointController.RuleConfigurationController + endpointAction.GetRuleTypeList);
  }

  getRuleExecutionTypeList() {
    return this.apiService.get(endpointController.RuleConfigurationController + endpointAction.GetRuleExecutionTypeList);
  }

  getRuleActionTypes() {
    return this.apiService.get(endpointController.RuleConfigurationController + endpointAction.GetRuleActionTypes);
  }

  getEntityFieldList(entityId: number) {
    return this.apiService.get(endpointController.RuleConfigurationController + endpointAction.GetEntityFields + '/' + entityId);
  }

  getSubEntitiesByEntityId(entityId: number) {
    return this.apiService.get(endpointController.RuleConfigurationController + endpointAction.GetSubEntitiesByEntityId + '/' + entityId);
  }

  getOperandTypeList() {
    return this.apiService.get(endpointController.RuleConfigurationController + endpointAction.GetOperandTypeList);
  }

  getQueryDatabaseList() {
    return this.apiService.get(endpointController.RuleConfigurationController + endpointAction.GetQueryDatabaseList);
  }

  getComparisonOperators(dataTypeId: number) {
    return this.apiService.get(endpointController.RuleConfigurationController + endpointAction.GetComparisonOperatorList + '/' + dataTypeId);
  }

  getLogicalOperators() {
    return this.apiService.get(endpointController.RuleConfigurationController + endpointAction.GetLogicalOperatorList);
  }

  getAssignmentOperators() {
    return this.apiService.get(endpointController.RuleConfigurationController + endpointAction.GetAssignmentOperatorList);
  }

  getRuleSetList(gridSetting: GridSettings) {
    return this.apiService.post(endpointController.RuleConfigurationController + endpointAction.GetRuleSetList, gridSetting);
  }

  getRuleSetDetail(ruleSetId: number) {
    return this.apiService.get(endpointController.RuleConfigurationController + endpointAction.GetRuleSetDetail + '/' + ruleSetId);
  }

  getPreDefinedFunctionList(dataTypeId: number) {
    return this.apiService.get(endpointController.RuleConfigurationController + endpointAction.GetRuleFunctionList + '/' + dataTypeId);
  }

  getMasterDataSourceList(fieldId: number, lobId: number) {
    lobId = lobId ? lobId : 0;
    return this.apiService.get(endpointController.RuleConfigurationController + endpointAction.GetMasterDataListForField + '/' + fieldId + '?lobId=' + lobId);
  }

  createRuleSet(ruleSetDetail: ruleSetDetailModel) {
    if (ruleSetDetail.RuleSetId && ruleSetDetail.RuleSetId > 0)
      return this.apiService.post(endpointController.RuleConfigurationController + endpointAction.UpdateRuleSet + '/' + ruleSetDetail.RuleSetId, ruleSetDetail);
    else
      return this.apiService.post(endpointController.RuleConfigurationController + endpointAction.CreateRuleSet, ruleSetDetail);
  }

  updateRuleSet(ruleSetId: number, ruleSetDetail: ruleSetDetailModel) {
    return this.apiService.post(endpointController.RuleConfigurationController + endpointAction.UpdateRuleSet + '/' + ruleSetId, ruleSetDetail);
  }

  deleteRuleSet(ruleSetId: number) {
    return this.apiService.post(endpointController.RuleConfigurationController + endpointAction.DeleteRuleSetDetail + '/' + ruleSetId)
  }

  changeRuleSetStatus(ruleSetId: number) {
    return this.apiService.post(endpointController.RuleConfigurationController + endpointAction.ChangeRuleSetStatus + '/' + ruleSetId)
  }

  // getTransformationList(transformationRuleProcess: transformationRuleProcess) {
  //   return this.apiService.post(endpointController.RuleConfigurationController + endpointAction.GetTransformationList, transformationRuleProcess);
  // }


  // createTransformationRuleSet(transformationRuleDetail: ruleConfigurationProcessWise) {
  //   if (transformationRuleDetail.RuleSetModelConfig.RuleSetId > 0)
  //     return this.apiService.post(endpointController.RuleConfigurationController + endpointAction.UpdateRuleSet + '/' + transformationRuleDetail.RuleSetModelConfig.RuleSetId, transformationRuleDetail.RuleSetModelConfig);
  //   else
  //     return this.apiService.post(endpointController.RuleConfigurationController + endpointAction.CreateTransformationRuleSet, transformationRuleDetail);
  // }

  // activateDeactivateRuleSet(ruleSetToOperate: activateRuleSet) {
  //   return this.apiService.post(endpointController.RuleConfigurationController + endpointAction.ActivateDeactivateRuleSet, ruleSetToOperate);
  // }

  set setRuleSetObj(ruleSetDetail: ruleSetDetailModel) {
    this.ruleSetDetail = ruleSetDetail;
  }

  get getRuleSetObj(): ruleSetDetailModel {
    return this.ruleSetDetail;
  }

  set setSourceSubEntityList(entityList: Array<subEntityModel>) {
    this.sourceSubEntities = entityList;
  }

  get getSourceSubEntityList(): Array<subEntityModel> {
    return this.sourceSubEntities ? this.sourceSubEntities : [];
  }

  get getTargetSubEntityList(): Array<subEntityModel> {
    return this.targetSubEntities ? this.targetSubEntities : [];
  }

  set setTargetSubEntityList(entityList: Array<subEntityModel>) {
    this.targetSubEntities = entityList;
  }

  get getSelectedRuleConfigurationType(): number {
    return this.ruleConfigurationTypeId;
  }

  set setRuleConfigurationType(type: number) {
    this.ruleConfigurationTypeId = type;
  }
}
