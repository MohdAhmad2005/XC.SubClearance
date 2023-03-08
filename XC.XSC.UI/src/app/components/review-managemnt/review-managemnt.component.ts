import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NgbActiveModal, NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { Formconstant } from 'src/app/common/constant/constant';
import { QuestionControlService } from 'src/app/common/dynamic-form/question-control.service';
import { GenericModalComponent } from 'src/app/common/generic-modal/generic-modal.component';
import { ModalDialogComponent } from 'src/app/common/modal-dialog/modal-dialog.component';
import { ToasterService } from 'src/app/common/toaster/toaster.service';
import { IReviewConfigurationRequest, IReviewConfigurationResponse } from 'src/app/models/review-management/review-management';
import { ActionReferenceType } from 'src/app/models/submission/submissionlist';
import { UserFilterModel, AttributesItems } from 'src/app/models/uam/filter/user-filter';
import { ReviewManagementService } from 'src/app/Services/review-management/review-management.service';
import { SharedService } from 'src/app/Services/shared.service';
import { UserService } from 'src/app/Services/uam/user/user.service';

@Component({
  selector: 'app-review-managemnt',
  templateUrl: './review-managemnt.component.html',
  styleUrls: ['./review-managemnt.component.css']
})

export class ReviewManagemntComponent implements OnInit {
  public isConfrimAlertVisible: boolean = false;
  public actionReference: ActionReferenceType;
  public confirmationMessage: string;
  private formValues: any;
  private modalReference: any;
  public columnDefination = [];
  public tableContent: IReviewConfigurationResponse[];
  public globalFilterField: any[] = ['regionName', 'teamName', 'lobName', 'processorName', 'reviewType', 'reviewerName'];
  public actionList: any;
  private regionId: string;
  private teamId: string;
  private lobId: string;
  private dynaForm: FormGroup;
  private isActive: boolean;

  constructor(private _modalService: NgbModal, private _reviewService: ReviewManagementService, private _sharedService: SharedService,
    private _toasterService: ToasterService, private _userService: UserService, private controlService: QuestionControlService) { }

  ngOnInit(): void {
    this.getColumnDefinition();
    this.getActionList();
    this.getTableContent();
  }

  private getColumnDefinition() {
    this.columnDefination.push({ header: 'Region', field: 'regionName', sortKey: 'regionName' },);
    this.columnDefination.push({ header: 'Team', field: 'teamName', sortKey: 'teamName' },);
    this.columnDefination.push({ header: 'LOB', field: 'lobName', sortKey: 'lobName' },);
    this.columnDefination.push({ header: 'Processor', field: 'processorName', sortKey: 'processorName' },);
    this.columnDefination.push({ header: 'Reviewer', field: 'reviewerName', sortKey: 'reviewerName' },);
    this.columnDefination.push({ header: 'Review Type', field: 'reviewType', sortKey: 'reviewType' },);
  }

  private getActionList() {
    this.actionList = [
      { name: 'Edit', css: 'fa fa-edit', action_type: ActionReferenceType.EditReviewManagement },
      { name: 'Delete', css: 'fa fa-trash', action_type: ActionReferenceType.DeleteReviewManagement },
    ];
  }

  public setActionEvents(event: any): void {
    if (!!event) {
      let ActionType = event.event.action_type;
      if (ActionType == ActionReferenceType.EditReviewManagement) {
        this.controlService.getFormData(Formconstant.reviewFormEdit).then(item => {
          const controls = item.response;
          this.editReviewManagement(event, controls);
        });
      }
      if (ActionType == ActionReferenceType.DeleteReviewManagement) {
        this.formValues = event.data.id;
        this.actionReference = ActionReferenceType.DeleteReviewManagement;
        this.isConfrimAlertVisible = true;
        this.confirmationMessage = 'Are you sure you would like to Delete?';
      }
    }
  }

  private displayModalDialog(message?: string): void {
    const modalRef = this._modalService.open(ModalDialogComponent);
    if (message == undefined)
      modalRef.componentInstance.ModalBody = "Unable to process your request. please try again later.";
    else
      modalRef.componentInstance.ModalBody = message;
  }

  public add() {
    this.controlService.getFormData(Formconstant.reviewFormAdd).then(item => {
      const controls = item.response;
      this.addReviewManagement(controls);
    });
  }

  private getTableContent() {
    this._reviewService.getAllReviewConfiguration().subscribe(response => {
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

  public checkBoxChange(value: any) {
    if (!!value) {
      if (value.event.target.checked) {
        var reviewdata = value.data as IReviewConfigurationRequest;
        reviewdata.isActive = true;
        this._reviewService.updateReviewConfiguration(reviewdata).subscribe(response => {
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
        var reviewdata = value.data as IReviewConfigurationRequest;
        reviewdata.isActive = false;
        this._reviewService.updateReviewConfiguration(reviewdata).subscribe(response => {
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

  public confirmationClick(event) {
    if (!!event) {
      if (event.isConfirmed) {
        if (event.referenceType == ActionReferenceType.AddReviewManagemnt) {
          this._reviewService.saveReviewConfiguration(this.formValues).subscribe(response => {
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
        if (event.referenceType == ActionReferenceType.EditReviewManagement) {
          this.formValues.processorId = this.formValues.processorId[0];
          this.formValues.isActive = this.isActive;
          this._reviewService.updateReviewConfiguration(this.formValues).subscribe(response => {
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
        if (event.referenceType == ActionReferenceType.DeleteReviewManagement) {
          this._reviewService.deleteReviewConfigurationById(this.formValues).subscribe(response => {
            if (response.isSuccess) {
              this._toasterService.successMessage(response.message);
              this.isConfrimAlertVisible = false;
              this.formValues = null
              this.getTableContent();
            }
            else {
              this.isConfrimAlertVisible = false;
              this._toasterService.errorMessage(response.message);
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

  private addReviewManagement(controls: any) {
    let ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    this.dynaForm =  this.controlService.toFormGroup(controls);
    this.modalReference = this._modalService.open(GenericModalComponent, ngbModalOptions);
    this.modalReference.componentInstance.header = "Add Review Type";
    this.modalReference.componentInstance.param = Formconstant.reviewFormAdd;
    this.modalReference.componentInstance.controls = controls;
    this.modalReference.componentInstance.dynaForm = this.dynaForm;
    this.modalReference.componentInstance.valueChangeEmitter.subscribe((emittedValue) => {
      if (emittedValue.control.includes("regionId")) {
        this.regionId = emittedValue.res;
      }
      if (emittedValue.control.includes("teamId")) {
        this.teamId = emittedValue.res;
      }
      if (emittedValue.control.includes("lobId")) {
        this.lobId = emittedValue.res;
      }
      if (this.regionId && this.teamId && this.lobId) {
        this.dynaForm.controls['processorId'].reset()
        let attributeItems: AttributesItems[];
        let processorFilter: UserFilterModel = {
          attributes: attributeItems,
          permissions: []
        }
        const processorValue: { name: string, value: string[] }[] = [
          { name: "region", value: [this.regionId.toString()] },
          { name: "team", value: [this.teamId.toString()] },
          { name: "lob", value: [this.lobId.toString()] },
          { name: "role", value: ['Processor'] }
        ]
        processorFilter.attributes = processorValue;
        this._userService.getUsersByFilters(processorFilter).subscribe(res => {
          if (res.isSuccess) {
            this.modalReference.componentInstance.controls.forEach(x => {
              if (x.key == "processorId") {
                const result = res.result;
                x.cols = [];
                result.forEach(i => {
                  x.cols.push({ "label": i.name, "value": i.id })
                });
              }
            })
          }
          else {
            this.modalReference.componentInstance.controls.forEach(x => {
              if (x.key == "processorId") {
                x.cols = [];
              }
            })
          }
        })
      }
      else {
        this.modalReference.componentInstance.controls.forEach(x => {
          if (x.key == "processorId") {
            x.cols = [];
          }
        })
      }
      if (this.regionId && this.teamId && this.lobId) {
        this.dynaForm.controls['reviewerId'].reset()
        let attributeItems: AttributesItems[];
        let reviewerFilter: UserFilterModel = {
          attributes: attributeItems,
          permissions: []
        }
        const reviewerValue: { name: string, value: string[] }[] = [
          { name: "region", value: [this.regionId.toString()] },
          { name: "team", value: [this.teamId.toString()] },
          { name: "lob", value: [this.lobId.toString()] },
          { name: "role", value: ['Reviewer'] }
        ]
        reviewerFilter.attributes = reviewerValue;
        this._userService.getUsersByFilters(reviewerFilter).subscribe(res => {
          if (res.isSuccess) {
            this.modalReference.componentInstance.controls.forEach(x => {
              if (x.key == "reviewerId") {
                x.options = res.result;
              }
            })
          }
          else {
            this.modalReference.componentInstance.controls.forEach(x => {
              if (x.key == "reviewerId") {
                x.options = [];
              }
            })
          }
        })
      }
      else {
        this.modalReference.componentInstance.controls.forEach(x => {
          if (x.key == "reviewerId") {
            x.options = [];
          }
        })
      }
    })
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
        this.actionReference = ActionReferenceType.AddReviewManagemnt;
        this.isConfrimAlertVisible = true;
        this.confirmationMessage = 'Are you sure you would like to Add?';
        return true;
      }
    });
    this.regionId = undefined;
    this.teamId = undefined;
    this.lobId = undefined;
  }

  private editReviewManagement(event: any, controls: any) {
    let ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    this.dynaForm =  this.controlService.toFormGroup(controls);
    this.modalReference = this._modalService.open(GenericModalComponent, ngbModalOptions);
    this.modalReference.componentInstance.header = "Edit Review Type";
    this.modalReference.componentInstance.controls = controls;
    this.modalReference.componentInstance.dynaForm = this.dynaForm;
    if (event.data.regionId && event.data.teamId && event.data.lobId) {
      let attributeItems: AttributesItems[];
      let processorFilter: UserFilterModel = {
        attributes: attributeItems,
        permissions: []
      }
      const processorValue: { name: string, value: string[] }[] = [
        { name: "region", value: [event.data.regionId.toString()] },
        { name: "team", value: [event.data.teamId.toString()] },
        { name: "lob", value: [event.data.lobId.toString()] },
        { name: "role", value: ['Processor'] }
      ]
      processorFilter.attributes = processorValue;
      this._userService.getUsersByFilters(processorFilter).subscribe(res => {
        if (res.isSuccess) {
          this.modalReference.componentInstance.controls.forEach(x => {
            if (x.key == "processorId") {
              const result = res.result;
              x.cols = [];
              result.forEach(i => {
                x.cols.push({ "label": i.name, "value": i.id })
              });
            }
          })
        }
        else {
          this.modalReference.componentInstance.controls.forEach(x => {
            if (x.key == "processorId") {
              x.cols = [];
            }
          })
        }
      })
    }
    else {
      this.modalReference.componentInstance.controls.forEach(x => {
        if (x.key == "processorId") {
          x.cols = [];
        }
      })
    }
    if (event.data.regionId && event.data.teamId && event.data.lobId) {
      let attributeItems: AttributesItems[];
      let reviewerFilter: UserFilterModel = {
        attributes: attributeItems,
        permissions: []
      }
      const reviewerValue: { name: string, value: string[] }[] = [
        { name: "region", value: [event.data.regionId.toString()] },
        { name: "team", value: [event.data.teamId.toString()] },
        { name: "lob", value: [event.data.lobId.toString()] },
        { name: "role", value: ['Reviewer'] }
      ]
      reviewerFilter.attributes = reviewerValue;
      this._userService.getUsersByFilters(reviewerFilter).subscribe(res => {
        if (res.isSuccess) {
          this.modalReference.componentInstance.controls.forEach(x => {
            if (x.key == "reviewerId") {
              x.options = res.result;
            }
          })
        }
        else {
          this.modalReference.componentInstance.controls.forEach(x => {
            if (x.key == "reviewerId") {
              x.options = [];
            }
          })
        }
      })
    }
    else {
      this.modalReference.componentInstance.controls.forEach(x => {
        if (x.key == "reviewerId") {
          x.options = [];
        }
      })
    }
    const processorList = [event.data.processorId];
    event.data.processorId = processorList.flat();
    this.isActive = event.data.isActive;
    this.modalReference.componentInstance.data = event.data;
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
        this.actionReference = ActionReferenceType.EditReviewManagement;
        this.isConfrimAlertVisible = true;
        this.confirmationMessage = 'Are you sure you would like to Update?';
        return true;
      }
    });
  }

}
