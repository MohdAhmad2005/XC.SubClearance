import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { DxDataGridComponent } from 'devextreme-angular';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { FilterConfig, GridSettings } from 'src/app/models/rule_engine/gridSettings';
import { contextEntityModel, ruleConfigurationType, ruleContextModel, ruleExecutionTypeModel, ruleSetDetailModel, ruleTypeModel, subEntityModel } from 'src/app/models/rule_engine/ruleConfigurationModel';
import { ConstRoles, messageConstant } from 'src/app/utility/rule-engine/rule-engine-constants';
import { environment } from 'src/environments/environment';
import DataSource from 'devextreme/data/data_source';
import { CommonMethods } from 'src/app/utility/rule-engine/commonMethods';
import { GlobalService } from 'src/app/Services/rule-engine/global.service';
import { MessageService } from 'src/app/Services/rule-engine/message.service';
import { UserInfoService } from 'src/app/Services/rule-engine/user-info.service';
import { ApplicationMessageService } from 'src/app/Services/rule-engine/application-message.service';
import { RuleConfigurationService } from 'src/app/Services/rule-engine/rule-configuration.service';
import { ActionReferenceType } from '../../../models/submission/submissionlist';
import { ToasterService } from '../../../common/toaster/toaster.service';

@Component({
  selector: 'app-rule-list',
  templateUrl: './rule-list.component.html',
  styleUrls: ['./rule-list.component.css']
})
export class RuleListComponent implements OnInit {
  private onDestroy$: Subject<void> = new Subject<void>();

  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  @ViewChild('closeRuleModal', { static: false }) closeModal: ElementRef
  //search = false;
  isRulePopUpVisible: boolean = false;
  rulePopupTitle: string = 'Add';
  showTargetEntity: boolean = false;
  showLOBDiv: boolean = false;
  showExecutionType: boolean = false;
  ruleDetailSubmitted: boolean = false;
  selectedRuleConfType: number = 1;

  ruleSetDetail: ruleSetDetailModel;
  ruleTypeList: Array<ruleTypeModel>;
  ruleExecutionTypeList: Array<ruleExecutionTypeModel>;
  sourceContextList: Array<ruleContextModel>;
  selectedContextEntities: Array<contextEntityModel>;
  sourceSubEntityList: Array<subEntityModel>;
  targetSubEntityList: Array<subEntityModel>;
  ruleConfigTypeList: Array<ruleConfigurationType> = [];
  lobList: any;

  selectedSourceSubEntityList: Array<subEntityModel>;
  selectedTargetSubEntityList: Array<subEntityModel>;

  gridSettings: GridSettings = new GridSettings();
  showFilterRow: boolean;
  showHeaderFilter: boolean;
  isSearch: boolean;
  isFilter: boolean;
  isSkip: number = 0;
  isTake: number = environment.DefaultPageSize;
  filterConfig: FilterConfig[] = [];
  elem;
  ruleList: any = {};
  error = this.appMessageService.getMessageByKey(messageConstant.ERRORMESSAGE);
  ruleSetId: number;
  userName: string;
  isDelete: boolean = false;
  //onDestroy$: Subscription;
  userRoles: any;
  isAdmin = true;
  //isUnderwriter = false;
  //isAnalystAuditor = false;
  //isEAUser = false;
  gridResizeHeight: number = this.globalService.gridDefaultHeight;//fixed if full screen is not enabled
  isFullScreen: boolean = false;
  public isConfrimAlertVisible: boolean = false;
  public confirmationMessage: string;
  public actionReference: ActionReferenceType;

  constructor(private globalService: GlobalService,
    private router: Router, private toastr: ToasterService, private titleService: Title,
    private userInfoService: UserInfoService,
    private appMessageService: ApplicationMessageService,
    private ruleConfigService: RuleConfigurationService
  ) {

    this.userName = this.userInfoService.getUserDetails().LoginName;
    this.userRoles = this.userInfoService.getUserDetails().Roles;

    this.getRoleName();
    this.selectedSourceSubEntityList = [];
    this.selectedTargetSubEntityList = [];
    this.ruleSetDetail = new ruleSetDetailModel();

  }

  ngOnInit() {
    this.loadRuleSetList();
    this.loadSettingData();
    this.initializeMasterList();
    this.loadLOBData();
  }

  loadLOBData() {
  }

  //Get Project Title
  loadSettingData() {
    this.titleService.setTitle(this.globalService.settingConfigData.clientName + " | Rule List");
  }
  //End


  loadRuleSetList() {
    this.ruleList = new DataSource({
      key: "RuleSetId",
      //This is paging sorting filter server side
      load: (loadOptions) => {
        this.gridSettings.UserName = this.userName;
        this.gridSettings.Skip = this.isSkip = (loadOptions.skip === undefined ? this.isSkip : loadOptions.skip);
        this.gridSettings.Take = this.isTake = (loadOptions.take === undefined ? this.isTake : loadOptions.take)
        this.gridSettings.Take = this.gridSettings.Take < 10 ? 10 : this.gridSettings.Take;
        if (loadOptions.filter) {
          this.filterConfig = [];
          //Set Grid Filter 
          this.filterConfig = CommonMethods.makeGridFilter(this.filterConfig, loadOptions); //this.makeGridFilter(loadOptions);


          if (this.filterConfig.length > 0)
            this.gridSettings.Filter = this.filterConfig;
        }
        else {
          this.gridSettings.Filter = null;
        }
        if (loadOptions.sort) {
          this.gridSettings.OrderBy = loadOptions.sort[0].selector;
          if (loadOptions.sort[0].desc) {
            this.gridSettings.OrderBy += ' desc';
          }
        }
        return this.ruleConfigService.getRuleSetList(this.gridSettings).toPromise()
          .then(result => {
            this.gridResizeHeight = this.globalService.calculateDefaultHeight(this.isFullScreen, 0, false);
            this.dataGrid.instance.option("height", this.gridResizeHeight);
            return {
              data: result.Entity,
              totalCount: result.TotalRowCount
            };
          })
          .catch(error => { throw this.appMessageService.getMessageByKey(messageConstant.DATALOADERRORMESSAGE) });
      }
    });

  }

  onToolbarPreparing(e) {
    e.toolbarOptions.items.unshift(
      {
        location: 'after',
        widget: 'dxButton',
        options: {
          icon: 'add',
          hint: 'Add New Rule',
          onClick: this.createRule.bind(this),
          elementAttr: {
            id: "btnAddRule"
          }
        }
      }
      , {
        location: 'after',
        widget: 'dxButton',
        options: {
          icon: 'filter', hint: 'Column Filter',
          onClick: this.enableFilter.bind(this)
        }
      },

      {
        location: 'after',
        widget: 'dxButton',
        options: {
          icon: 'search', hint: 'Column Search',
          onClick: this.enableSearch.bind(this)
        }
      }
      //, {
      //location: 'after',
      //widget: 'dxButton',
      //options: {
      //  icon: 'expand', hint: 'Expand Screen',
      //  onClick: this.openFullscreen.bind(this)
      //}
      //}

    );
  }

  createRule() {
    if (this.isAdmin) {
      this.ruleSetDetail = new ruleSetDetailModel();
      this.rulePopupTitle = 'Create';
      this.isRulePopUpVisible = true;
    }
  }

  initializeMasterList() {
    this.ruleConfigTypeList.push({
      id: 1,
      description: 'If-Then'
    });
    this.ruleConfigTypeList.push({
      id: 2,
      description: 'If-Then Else-If'
    });

    this.ruleConfigService.getRuleTypeList().pipe().subscribe(data => {
      this.ruleTypeList = data;
    });

    this.ruleConfigService.getRuleExecutionTypeList().pipe().subscribe(data => {
      this.ruleExecutionTypeList = data;
    });

    this.ruleConfigService.getRuleContextList().pipe().subscribe(data => {
      this.sourceContextList = data;
    });
  }

  enableSearch() {
    this.isSearch = !this.isSearch;
  }

  enableFilter() {
    this.isFilter = !this.isFilter;
    if (this.filterConfig.length > 0) {
      this.filterConfig = [];
      this.loadRuleSetList();
    }
  }

  /* Close fullscreen */
  //closeFullscreen() {
  //  $('#rulefullscreen').removeClass('fullscreen');

  //}

  openFullscreen() {
    //$('#rulefullscreen').toggleClass('fullscreen');
    this.isFullScreen = !this.isFullScreen;
    this.gridResizeHeight = this.globalService.calculateDefaultHeight(this.isFullScreen, 0, false);
    this.dataGrid.instance.option("height", this.gridResizeHeight);

  }

  public confirmationClick(event) {
    if (!!event) {
      if (event.isConfirmed) {
        this.deleteRule(this.ruleSetId);
      }
      else {
        this.isConfrimAlertVisible = false;
      }
    }
  }

  showDialog(ruleSetId: number): void {
    this.ruleSetId = ruleSetId;
    this.isDelete = true;
    this.isConfrimAlertVisible = true;
    this.confirmationMessage = 'Are you sure you would like to Delete?';

  }

  deleteRule(ruleSetId: number) {
    if (ruleSetId > 0) {
      this.ruleConfigService.deleteRuleSet(ruleSetId).subscribe(result => {
        if (result) {
          if (result.Response === true) {
            this.toastr.successMessage(this.appMessageService.getMessageByKey(messageConstant.RULEDELETED));
            this.loadRuleSetList();
          }
          else
            this.toastr.errorMessage(this.error);
        }
        this.isDelete = false;
        this.closeModal.nativeElement.click();
      });
    }
    else {
      this.toastr.warningMessage(this.appMessageService.getMessageByKey(messageConstant.SELECTRULE));
    }
    this.isDelete = false;
    this.closeModal.nativeElement.click();
  }

  ruleAssign(ruleId: number) {
    if (ruleId > 0) {
      this.toastr.errorMessage(this.appMessageService.getMessageByKey(messageConstant.ERRORMESSAGE));
    }
    else {
      this.toastr.warningMessage(this.appMessageService.getMessageByKey(messageConstant.SELECTRULE));
    }
  }

  getRoleName() {
    if (this.userRoles && this.userRoles.length > 0
      && this.userRoles.filter != null) {
      if (this.userRoles.filter(x => x.RoleName === ConstRoles.Admin).length > 0)
        this.isAdmin = true;
      //else if (this.userRoles.filter(x => x.RoleName.toUpperCase() === 'UNDERWRITER' || x.RoleName.toUpperCase() === 'UNDERWRITER ASSISTANT').length > 0) {
      //  this.isUnderwriter = true;
      //}
      //else if (this.userRoles.filter(x => x.RoleName.toUpperCase() === 'ANALYST' || x.RoleName.toUpperCase() === 'AUDITOR').length > 0)
      //  this.isAnalystAuditor = true;
      //if (this.userRoles.filter(x => x.RoleName.toUpperCase() === 'E&A USER').length > 0) {
      //  this.isEAUser = true;
      //}
    }
  }

  editRuleSet(ruleSetId: number) {
    this.ruleConfigService.getRuleSetDetail(ruleSetId).subscribe(ruleSetData => {
      //this.ruleSetDetail = null;
      this.ruleConfigService.setRuleSetObj = ruleSetData;
      //this.selectedRuleConfType = this.ruleSetDetail.Rules && this.ruleSetDetail.Rules.length > 1 ? 2 : 1;
      this.ruleConfigService.setRuleConfigurationType = this.ruleSetDetail.Rules && this.ruleSetDetail.Rules.length > 1 ? 2 : 1;
      //this.rulePopupTitle = 'Update';
      this.router.navigate(['rule-conf/rule-editor']);
      //this.isRulePopUpVisible = true;
    })
  }

  hideRulePopup() {
    this.isRulePopUpVisible = false;
  }

  onRuleTypeChange(event) {
    this.ruleSetDetail.RuleTypeDetail = event.selectedItem;
  }

  onRuleExecutionTypeChange(event) {
    this.ruleSetDetail.RuleExecutionTypeDetail = event.selectedItem;
  }

  onSourceContextSelected(event) {
    let selectedContext: ruleContextModel = event.selectedItem;
    this.showTargetEntity = selectedContext.HasTargetContext;
    this.showExecutionType = selectedContext.IsCommandApplicable;
    this.showLOBDiv = selectedContext.IsLOBRequired;

    this.ruleConfigService.getContextEntities(selectedContext.ContextId).pipe().subscribe(entityData => {
      this.selectedContextEntities = entityData;
    });

    if (this.showExecutionType == false) {
      this.ruleSetDetail.RuleExecutionTypeDetail = this.ruleExecutionTypeList[0];
    }

    this.ruleSetDetail.ContextDetail = selectedContext;
  }

  onSourceEntitySelected(event) {
    this.ruleConfigService.getSubEntitiesByEntityId(event.selectedItem.EntityId).pipe().subscribe(subEntities => {
      this.sourceSubEntityList = subEntities;
    });
    this.ruleSetDetail.SourceEntityDetail = event.selectedItem;
  }

  onSourceSubEntitySelected(event) {

    if (event && event.addedItems.length > 0) {
      event.addedItems.forEach(item => {
        if (this.selectedSourceSubEntityList.indexOf(item) == -1) {
          this.selectedSourceSubEntityList.push(item);
        }
      });
    }

    if (event && event.removedItems.length > 0) {
      event.removedItems.forEach(items => {
        let indexToRemove = this.selectedSourceSubEntityList.indexOf(items);
        if (indexToRemove > -1) {
          this.selectedSourceSubEntityList.splice(indexToRemove, 1);
        }
      })
    }
  }

  onTargetEntitySelected(event) {
    this.ruleConfigService.getSubEntitiesByEntityId(event.selectedItem.EntityId).pipe().subscribe(subEntities => {
      this.targetSubEntityList = subEntities;
    });
    this.ruleSetDetail.TargetEntityDetail = event.selectedItem;
  }

  onTargetSubEntitySelected(event) {

    if (event && event.addedItems.length > 0) {
      event.addedItems.forEach(item => {
        if (this.selectedTargetSubEntityList.indexOf(item) == -1) {
          this.selectedTargetSubEntityList.push(item);
        }
      });
    }

    if (event && event.removedItems.length > 0) {
      event.removedItems.forEach(items => {
        let indexToRemove = this.selectedTargetSubEntityList.indexOf(items);
        if (indexToRemove > -1) {
          this.selectedTargetSubEntityList.splice(indexToRemove, 1);
        }
      });
    }
  }

  onSubmitRuleDetail() {

    this.ruleConfigService.setSourceSubEntityList = this.selectedSourceSubEntityList;
    if (this.ruleSetDetail.ContextDetail && this.ruleSetDetail.ContextDetail.HasTargetContext == false) {
      this.ruleConfigService.setTargetSubEntityList = this.ruleConfigService.getSourceSubEntityList;
    }
    else {
      this.ruleConfigService.setTargetSubEntityList = this.selectedTargetSubEntityList;
    }
    this.ruleConfigService.setRuleSetObj = this.ruleSetDetail;
    this.ruleConfigService.setRuleConfigurationType = this.selectedRuleConfType;
    this.ruleDetailSubmitted = true;
    this.router.navigate(['rule-conf/rule-editor']);
  }
}
