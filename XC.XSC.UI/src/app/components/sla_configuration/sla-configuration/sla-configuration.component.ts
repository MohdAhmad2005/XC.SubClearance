import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { Formconstant } from 'src/app/common/constant/constant';
import { QuestionControlService } from 'src/app/common/dynamic-form/question-control.service';
import { GenericModalComponent } from 'src/app/common/generic-modal/generic-modal.component';
import { ModalDialogComponent } from 'src/app/common/modal-dialog/modal-dialog.component';
import { ToasterService } from 'src/app/common/toaster/toaster.service';
import { SLAEscalationType, SLAFormKeys, SLAGlobalFilterField, SLAType } from 'src/app/enum/sla';
import { GenericGridSettings, gridDefaultFilters, PageInfo } from 'src/app/models/config/genricGridSetting';
import { IslaConfigurationRequest, IslaConfigurationResponse, MailBoxFilter } from 'src/app/models/Sla/sla';
import { ActionReferenceType } from 'src/app/models/submission/submissionlist';
import { EmailInfoService } from 'src/app/Services/email-info/email-info.service';
import { SharedService } from 'src/app/Services/shared.service';
import { SlaManagementService } from 'src/app/Services/sla/sla-configuration.service';
import { UserService } from 'src/app/Services/uam/user/user.service';
import { RegularExpressionServiceService } from 'src/services/regular-expression-service.service';

@Component({
  selector: 'app-sla-configuration',
  templateUrl: './sla-configuration.component.html',
  styleUrls: ['./sla-configuration.component.css']
})
export class SlaConfigurationComponent implements OnInit {
  public isConfrimAlertVisible: boolean = false;
  public actionReference: ActionReferenceType;
  public confirmationMessage: string;
  private formValues: any;
  private modalReference: any;
  public pageInfo: PageInfo;
  public genericGridSettings: GenericGridSettings = new GenericGridSettings();
  public genericGridCurrentSetting: gridDefaultFilters = new gridDefaultFilters();
  public columnDefination = [];
  public tableContent: IslaConfigurationResponse[];
  public globalFilterField: any[] = [SLAGlobalFilterField.Region, SLAGlobalFilterField.Team, SLAGlobalFilterField.Lob, SLAGlobalFilterField.Type, SLAGlobalFilterField.SlaDefinition, SLAGlobalFilterField.MailBoxId, SLAGlobalFilterField.UpdatedBy];
  public actionList: any;
  private regionId: any;
  private teamId: any;
  private lobId: any;
  private isActive: boolean;
  private dynaForm: FormGroup;
  
  constructor(private _modalService: NgbModal, private _activeModal: NgbActiveModal, private _slaService: SlaManagementService,
    private _sharedService: SharedService, private _userService: UserService,
    private _toasterService: ToasterService, private controlService: QuestionControlService,
    private _emailService: EmailInfoService,
    private _regularExpressionService: RegularExpressionServiceService,

  ) { }

  ngOnInit(): void {
    this.getColumnDefinition();
    this.getActionList();
    this.getTableContent();

  }
  getColumnDefinition() :void{
    this.columnDefination.push({ header: 'Region', field: 'region', sortKey: 'region' },);
    this.columnDefination.push({ header: 'Team', field: 'team', sortKey: 'team' },);
    this.columnDefination.push({ header: 'LOB', field: 'lob', sortKey: 'lob' },);
    this.columnDefination.push({ header: 'SLA Type', field: 'typeName', sortKey: 'typeName' },);
    this.columnDefination.push({ header: 'SLA Definition', field: 'slaDefinition', sortKey: 'slaDefinition' },);
    this.columnDefination.push({ header: 'Mailbox', field: 'mailBoxName', sortKey: 'mailBoxName' },);
    this.columnDefination.push({ header: 'Updated By', field: 'updatedBy', sortKey: 'updatedBy' },);
  }
  getActionList() :void{
    this.actionList = [
      { name: 'Edit', css: 'fa fa-edit', action_type: ActionReferenceType.EditSlaManagement },
    ];
  }

  public openPopUp(event, actionType: any, controls):void{
    if (actionType == ActionReferenceType.EditSlaManagement) {
      let ngbModalOptions: NgbModalOptions = {
        backdrop: 'static',
        keyboard: false,
        size: 'lg'
      };
      this.dynaForm = this.controlService.toFormGroup(controls);
      this.modalReference = this._modalService.open(GenericModalComponent, ngbModalOptions);
      this.modalReference.componentInstance.header = "Edit SLA";
      this.modalReference.componentInstance.param = Formconstant.slaFormedit;
      this.modalReference.componentInstance.controls = controls;
      this.modalReference.componentInstance.dynaForm = this.dynaForm;
      if (event.data.type == SLAType.TAT) {
        this.modalReference.componentInstance.controls.forEach(x => {
          if (x.key == SLAFormKeys.Percentage || x.key == SLAFormKeys.SamplePercentage) {
            x.active = false;
            this.dynaForm.controls[x.key].clearValidators();
            this.dynaForm.controls[x.key].updateValueAndValidity();
            x.validations = [];
          }
          if (x.key == SLAFormKeys.Day || x.key == SLAFormKeys.Hours || x.key == SLAFormKeys.Min || x.key == SLAFormKeys.IsEscalation || x.key == SLAFormKeys.TaskType) {
            x.active = true;
            if (x.key == SLAFormKeys.Day || x.key == SLAFormKeys.IsEscalation) {
              this.dynaForm.controls[x.key].setValidators([Validators.required]);
              this.dynaForm.controls[x.key].updateValueAndValidity();
              x.validations = [];
              var requiredValidator = {
                "name": "required",
                "validator": "Validators.required",
                "message": x.label + " is required"
              }
              x.validations.push(requiredValidator);
            }
          }
        });
      }
      if (event.data.type == SLAType.Accuracy) {
        this.modalReference.componentInstance.controls.forEach(x => {
          if (x.key == SLAFormKeys.Percentage || x.key == SLAFormKeys.SamplePercentage) {
            x.active = true;
            this.dynaForm.controls[x.key].setValidators([Validators.required]);
            this.dynaForm.controls[x.key].updateValueAndValidity();
            x.validations = [];
            var requiredValidator = {
              "name": "required",
              "validator": "Validators.required",
              "message": x.label + " is required"
            }
            x.validations.push(requiredValidator);
            if (x.key == SLAFormKeys.Percentage) {
              this.dynaForm.controls[x.key].setValidators([Validators.pattern(this._regularExpressionService.NumbersBetweenZeroToHundered)]);
              this.dynaForm.controls[x.key].updateValueAndValidity();
              var patternValidator = {
                "name": "pattern",
                "validator": this._regularExpressionService.NumbersBetweenZeroToHundered,
                "message": "Numbers are allowed between 0 - 100 only"
              }
              x.validations.push(patternValidator);
            }
          }
          if (x.key == SLAFormKeys.Day || x.key == SLAFormKeys.Hours || x.key == SLAFormKeys.Min || x.key == SLAFormKeys.IsEscalation || x.key == SLAFormKeys.TaskType) {
            x.active = false;
            if (x.key == SLAFormKeys.Day || x.key == SLAFormKeys.IsEscalation) {
              this.dynaForm.controls[x.key].clearValidators();
              this.dynaForm.controls[x.key].updateValueAndValidity();
              x.validations = [];
            }
          }
        });
      }
      if (event.data.regionId && event.data.teamId && event.data.lobId) {
        const mailBoxFilter:MailBoxFilter={regionId:event.data.regionId, lobId:event.data.lobId, teamId:event.data.teamId}
        this._emailService.getMailBoxDetail(mailBoxFilter).subscribe(res => {
          if (res.isSuccess) {
            this.modalReference.componentInstance.controls.forEach(x => {
              if (x.key == SLAFormKeys.MailBoxId) {
                x.options = res.result;
              }
            })
          }
          else {
            this.modalReference.componentInstance.controls.forEach(x => {
              if (x.key == SLAFormKeys.MailBoxId) {
                x.options = [];
              }
            })
          }
        })
      }
      else {
        this.modalReference.componentInstance.controls.forEach(x => {
          if (x.key == SLAFormKeys.MailBoxId) {
            x.options = [];
          }
        })
      }
      event.data.isEscalation = event.data.isEscalation == true ? SLAEscalationType.Active : SLAEscalationType.Inactive;
      this.modalReference.componentInstance.data = event.data;
      this.isActive = event.data.isActive;
      this.modalReference.componentInstance.emitService.subscribe((emmittedValue) => {
        if (emmittedValue == ActionReferenceType.CloseModal) {
          this.actionReference = ActionReferenceType.CloseModal;
          this.isConfrimAlertVisible = true;
          this.confirmationMessage = 'Are you sure you would like to Cancel?';
          return true;
        }
        else {
          if (emmittedValue.invalid) {
            return false;
          }
          this.formValues = emmittedValue.value;
          this.formValues.id = event.data.id;
          this.actionReference = ActionReferenceType.EditSlaManagement;
          this.isConfrimAlertVisible = true;
          this.confirmationMessage = 'Are you sure you would like to Update?';
          return true;
        }
      });
    }
  }

  public setActionEvents(event: any): void {
    if (!!event) {
      let ActionType = event.event.action_type;
      this.getcontrols(event, ActionType,)
    }
  }
  public getcontrols(event, action_type):void {
    this.controlService.getFormData(Formconstant.slaFormedit).then(item => {
      const controls = item.response;
      this.openPopUp(event, action_type, controls)

    });
  }
  private displayModalDialog(message?: string): void {
    const modalRef = this._modalService.open(ModalDialogComponent);
    if (message == undefined)
      modalRef.componentInstance.ModalBody = "Unable to process your request. please try again later.";
    else
      modalRef.componentInstance.ModalBody = message;
  }
  public Addpopup():void {
    this.controlService.getFormData(Formconstant.slaForm).then(item => {
      const controls = item.response;
      this.add(controls);
    });

  }
  add(controls: any) : void{
    let ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    this.dynaForm = this.controlService.toFormGroup(controls);
    this.modalReference = this._modalService.open(GenericModalComponent, ngbModalOptions);
    this.modalReference.componentInstance.header = "Add SLA";
    this.modalReference.componentInstance.param = Formconstant.slaForm
    this.modalReference.componentInstance.controls = controls;
    this.modalReference.componentInstance.dynaForm = this.dynaForm;
    this.modalReference.componentInstance.valueChangeEmitter.subscribe((emittedValue) => {
      if (!emittedValue.control.includes(SLAFormKeys.Type)) {
        if (emittedValue.control.includes(SLAFormKeys.RegionId)) {
          this.regionId = emittedValue.res;
        }
        if (emittedValue.control.includes(SLAFormKeys.TeamId)) {
          this.teamId = emittedValue.res;
        }
        if (emittedValue.control.includes(SLAFormKeys.LobId)) {
          this.lobId = emittedValue.res;
        }
        if (this.regionId && this.teamId && this.lobId) {
          this.dynaForm.controls[SLAFormKeys.MailBoxId].reset()
          const mailBoxFilter:MailBoxFilter={regionId:this.regionId, lobId:this.lobId, teamId:this.teamId}
          this._emailService.getMailBoxDetail(mailBoxFilter).subscribe(res => {
            if (res.isSuccess) {
              this.modalReference.componentInstance.controls.forEach(x => {
                if (x.key == SLAFormKeys.MailBoxId) {
                  x.options = res.result;
                }
              })
            }
            else {
              this.modalReference.componentInstance.controls.forEach(x => {
                if (x.key == SLAFormKeys.MailBoxId) {
                  x.options = [];
                }
              })
            }
          })
        }
        else {
          this.modalReference.componentInstance.controls.forEach(x => {
            if (x.key == SLAFormKeys.MailBoxId) {
              x.options = [];
            }
          })
        }
      }
      if (emittedValue.control.includes(SLAFormKeys.Type) && emittedValue.res == SLAType.TAT) {
        this.modalReference.componentInstance.controls.forEach(x => {
          if (x.key == SLAFormKeys.Percentage || x.key == SLAFormKeys.SamplePercentage) {
            x.active = false;
            x.validations = [];
            this.dynaForm.get(x.key).setValue(null);
            this.dynaForm.controls[x.key].clearValidators();
            this.dynaForm.controls[x.key].updateValueAndValidity();
            this.dynaForm.controls[x.key].markAsPristine();
            this.dynaForm.controls[x.key].markAsUntouched();
          }
          if (x.key == SLAFormKeys.Day || x.key == SLAFormKeys.Hours || x.key == SLAFormKeys.Min || x.key == SLAFormKeys.IsEscalation || x.key == SLAFormKeys.TaskType) {
            x.active = true;
            if (x.key == SLAFormKeys.Day || x.key == SLAFormKeys.IsEscalation) {
              this.dynaForm.controls[x.key].setValidators([Validators.required]);
              this.dynaForm.controls[x.key].updateValueAndValidity();
              x.validations = [];
              var requiredValidator = {
                "name": "required",
                "validator": "Validators.required",
                "message": x.label + " is required"
              }
              x.validations.push(requiredValidator);
            }
          }
        });
      }
      if (emittedValue.control.includes(SLAFormKeys.Type) && emittedValue.res == SLAType.Accuracy) {
        this.modalReference.componentInstance.controls.forEach(x => {
          if (x.key == SLAFormKeys.Percentage || x.key == SLAFormKeys.SamplePercentage) {
            x.active = true;
            this.dynaForm.controls[x.key].setValidators([Validators.required]);
            this.dynaForm.controls[x.key].updateValueAndValidity();
            x.validations = [];
            var requiredValidator = {
              "name": "required",
              "validator": "Validators.required",
              "message": x.label + " is required"
            }
            x.validations.push(requiredValidator);
            if (x.key == SLAFormKeys.Percentage) {
              this.dynaForm.controls[x.key].setValidators([Validators.pattern(this._regularExpressionService.NumbersBetweenZeroToHundered)]);
              this.dynaForm.controls[x.key].updateValueAndValidity();
              var patternValidator = {
                "name": "pattern",
                "validator": this._regularExpressionService.NumbersBetweenZeroToHundered,
                "message": "Numbers are allowed between 0 - 100 only"
              }
              x.validations.push(patternValidator);
            }
          }
          if (x.key == SLAFormKeys.Day || x.key == SLAFormKeys.Hours || x.key == SLAFormKeys.Min || x.key == SLAFormKeys.IsEscalation || x.key == SLAFormKeys.TaskType) {
            x.active = false;
            if (x.key == SLAFormKeys.Day || x.key == SLAFormKeys.IsEscalation) {
              x.validations = [];
              this.dynaForm.get(x.key).setValue(null);
              this.dynaForm.controls[x.key].clearValidators();
              this.dynaForm.controls[x.key].updateValueAndValidity();
              this.dynaForm.controls[x.key].markAsPristine();
              this.dynaForm.controls[x.key].markAsUntouched();
            }
          }
        });
      }
      if (emittedValue.control.includes(SLAFormKeys.Type) && emittedValue.res == null) {
        this.modalReference.componentInstance.controls.forEach(x => {
          if (x.key == SLAFormKeys.Percentage || x.key == SLAFormKeys.SamplePercentage || x.key == SLAFormKeys.Day || x.key == SLAFormKeys.Hours || x.key == SLAFormKeys.Min || x.key == SLAFormKeys.IsEscalation || x.key == SLAFormKeys.TaskType) {
            x.active = false;
            this.dynaForm.get(x.key).setValue(null);
            this.dynaForm.controls[x.key].clearValidators();
            this.dynaForm.controls[x.key].updateValueAndValidity();
            x.validations = [];
            this.dynaForm.controls[x.key].markAsPristine();
            this.dynaForm.controls[x.key].markAsUntouched();
          }
        });
      }
    });
    this.modalReference.componentInstance.emitService.subscribe((emmittedValue) => {
      if (emmittedValue == ActionReferenceType.CloseModal) {
        this.actionReference = ActionReferenceType.CloseModal;
        this.isConfrimAlertVisible = true;
        this.confirmationMessage = 'Are you sure you would like to cancel?';
        return true;
      }
      else {
        if (emmittedValue.invalid) {
          this._sharedService.validateAllFormFields(emmittedValue, emmittedValue.controls);
          return false;
        }
        this.formValues = emmittedValue.value;
        this.actionReference = ActionReferenceType.AddSlaManagemnt;
        this.isConfrimAlertVisible = true;
        this.confirmationMessage = 'Are you sure you would like to submit?';
        return true;
      }
    });
    this.regionId = undefined;
    this.teamId = undefined;
    this.lobId = undefined;
  }
  public getTableContent():void{
    this._slaService.getSlaConfigurationByDetails().subscribe(response => {
      if (response.isSuccess) {
        this.tableContent = response.result;
      }
      else {
        this.tableContent = null;
      }
    },
      error => {
        this.tableContent = null;
        this.displayModalDialog();
      });
  }
  public confirmationClick(event):void {
    if (!!event) {
      if (event.isConfirmed) {
        if (event.referenceType == ActionReferenceType.AddSlaManagemnt) {
          this.formValues = this.formValues as IslaConfigurationRequest;
          this.formValues.name = this.formValues.name.trim();
          if (this.formValues.type == 1) {
            this.formValues.isEscalation = this.formValues.isEscalation == SLAEscalationType.Active ? true : false;
            this.formValues.samplePercentage = 0;
            this.formValues.percentage = 0;
            this.formValues.hours=0;
            this.formValues.min=0;
            this.formValues.taskType=0;
          }
          else {
            this.formValues.day = 0;
            this.formValues.isEscalation = false;
            this.formValues.hours=0;
            this.formValues.min=0;
            this.formValues.taskType=0;
          }
          this._slaService.saveSlaConfiguration(this.formValues).subscribe(response => {
            if (response.isSuccess) {
              this._toasterService.successMessage(response.message, this._toasterService.modalToastKey);
              this.isConfrimAlertVisible = false;
              this.modalReference.close();
              this.formValues = null
              this.getTableContent();
            }
            else {
              this.isConfrimAlertVisible = false;
              this._toasterService.errorMessage(response.message, this._toasterService.modalToastKey);
              this.getTableContent();
            }
          },
            error => {
              this.isConfrimAlertVisible = false;
              this.getTableContent();
            });
        }
        if (event.referenceType == ActionReferenceType.CloseModal) {
          this.modalReference.close();
          this.isConfrimAlertVisible = false;
        }
        if (event.referenceType == ActionReferenceType.EditSlaManagement) {
          this.formValues = this.formValues as IslaConfigurationRequest;
          this.formValues.isActive = this.isActive;
          this.formValues.name = this.formValues.name.trim();
          if (this.formValues.type == 1) {
            this.formValues.isEscalation = this.formValues.isEscalation == SLAEscalationType.Active ? true : false;
            this.formValues.samplePercentage = 0;
            this.formValues.percentage = 0;
          }
          else {
            this.formValues.day = 0;
            this.formValues.isEscalation = false;
          }
          this._slaService.updateSlaConfiguration(this.formValues).subscribe(response => {
            if (response.isSuccess) {
              this._toasterService.successMessage(response.message, this._toasterService.modalToastKey);
              this.isConfrimAlertVisible = false;
              this.modalReference.close();
              this.formValues = null
              this.getTableContent();
            }
            else {
              this.isConfrimAlertVisible = false;
              this._toasterService.errorMessage(response.message, this._toasterService.modalToastKey);
              this.getTableContent();
            }
          },
            error => {
              this.isConfrimAlertVisible = false;
              this.getTableContent();
            });
        }
      }
      else {
        this.isConfrimAlertVisible = false;
      }
    }
  }

  public checkBoxChange(value: any):void {
    if (!!value) {
      if (value.event.target.checked) {
        var data = value.data as IslaConfigurationRequest;
        data.isActive = true;
        this._slaService.updateSlaConfiguration(data).subscribe(response => {
          if (response.isSuccess) {
            this._toasterService.successMessage(response.message);
            this.getTableContent();
          }
          else {
            this._toasterService.errorMessage(response.message);
            this.getTableContent();
          }
        },
          error => {
            this.getTableContent();
          });
      }
      else {
        var data = value.data as IslaConfigurationRequest;
        data.isActive = false;
        this._slaService.updateSlaConfiguration(data).subscribe(response => {
          if (response.isSuccess) {
            this._toasterService.successMessage(response.message);
            this.getTableContent();
          }
          else {
            this._toasterService.errorMessage(response.message);
            this.getTableContent();
          }
        },
          error => {
            this.getTableContent();
          });
      }
    }
  }
}
