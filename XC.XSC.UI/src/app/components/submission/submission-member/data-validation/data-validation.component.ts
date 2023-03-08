import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { DocumentService } from 'src/app/Services/document/document.service';
import * as FileSaver from 'file-saver';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SubmissionService } from 'src/app/Services/submission/submission.service';
import { ActivatedRoute } from '@angular/router';
import { IEmailInfoResponse } from 'src/app/models/email-info/email-info';
import { EmailInfoService } from 'src/app/Services/email-info/email-info.service';
import { DateparserService } from 'src/app/utility/dateparser.service';
import { SubmissionDetailsTableComponent } from './submission-details-table/submission-details-table.component';
import { SubmissionData, SubmissionExtraction } from 'src/app/models/submission/data-validations';
import { ToasterService } from 'src/app/common/toaster/toaster.service';

@Component({
  selector: 'app-data-validation',
  templateUrl: './data-validation.component.html',
  styleUrls: ['./data-validation.component.css']
})

export class DataValidationComponent implements OnInit {
  private emailInfoId: number;
  @Input() set EmailInfoId(value: number) {
    if (!!value) {
      this.emailInfoId = value;
      this.getEmailInfoDetailById();
    }
  }
  public emailDetails: IEmailInfoResponse;
  public submissionData: SubmissionData[];
  public submissionResponseData: SubmissionExtraction;
  public submissionFormData: any;
  public formValid: any;
  @ViewChild(SubmissionDetailsTableComponent) submissionDetailsTableComponent: SubmissionDetailsTableComponent;
  public submissionId: any;

  constructor(private _documentService: DocumentService, private _modalService: NgbModal,
    private _emailInfoService: EmailInfoService, private _routeParams: ActivatedRoute,
    private _dateformatter: DateparserService, private submissionService: SubmissionService,
    private _dateparserService: DateparserService, private _toaster: ToasterService) { }

  ngOnInit(): void {
    //this._routeParams.queryParams.subscribe(params => {
    //  this.submissionId = params['submissionId']
    //});
    this.getSubmissionForm();
    this.getEmailInfoDetailById();
  }

  public downloadFile(documentId: string, fileName: string, fileType: string): void {
      this._documentService.download(documentId)
        .subscribe((res) => {
          if (!!res) {
            let blob;
            if (fileType == '') {
              blob = new Blob([res]);
            }
            else {
              blob = new Blob([res], { type: fileType });
            }
            FileSaver.saveAs(blob, fileName);
          }
          else {
            this._toaster.errorMessage("Error in document download.");
          }
        },
          error => {
            this._toaster.warningMessage(error.message);
          });
  }

  private getEmailInfoDetailById(): void {
    if (this.emailInfoId) {
      this._emailInfoService.getEmailInfoDetailById(this.emailInfoId).subscribe(response => {
        if (response.isSuccess) {
          this.emailDetails = response.result as IEmailInfoResponse;
        }
        else {
          this.emailDetails = {} as IEmailInfoResponse;
          this._toaster.errorMessage(response.message);
        }
      },
        error => {
          this.emailDetails = {} as IEmailInfoResponse;
          this._toaster.warningMessage(error.message);
        });
    }
  }

  public getDate(data: any) {
    return (this._dateformatter.FormatDate(data))
  }


  public save(type: string): void {
    let dateKey = []
    this.submissionDetailsTableComponent.formData.forEach(element => {
      dateKey = element.controls.filter(x => x.control == 'datepicker').map(x => x.key);
      if (dateKey && dateKey.length) {
        dateKey.forEach((item) => {
          Object.keys(element).forEach((x) => {
            if (item == x && !!element[item]) {
              element[item] = this._dateparserService.FormatDate(element[item]);
            }
            if (item == x && element[item] == undefined) {
              element[item] = "";
            }
          })
        });
      }
    });
    let request = [...this.submissionDetailsTableComponent.formData, ...this.submissionDetailsTableComponent.submissionList]
    this.submissionResponseData.submissionFormData.submissionData = request;
    this.submissionService.saveSubmissiondata(this.submissionResponseData).subscribe(res => {
      if (res.isSuccess) {
        this._toaster.successMessage(res.message);
      }
      else {
        this._toaster.errorMessage(res.message);
      }
    }), error => {
      this._toaster.warningMessage(error.message);
    }
  };

  public getSubmissionData(): void {
    this.submissionService.getSubmissionData(this.submissionService._apiUrl.getSubmissionData('27')).subscribe(res => {
      if (res.isSuccess) {
        this.submissionResponseData = res['result'];
        this.submissionData = this.submissionResponseData.submissionFormData.submissionData;
        this.submissionId = this.submissionData['submissionId'];
      }
      else {
        this._toaster.errorMessage(res.message);
      }
    }, error => {
      this._toaster.warningMessage(error.message);
    })
  }

  public childFormValid(event: any): void {
    this.formValid = event;
  }

  public getSubmissionForm(): void {
    this.submissionService.getSubmissionForm(this.submissionService._apiUrl.getSubmissionForm).subscribe(res => {
      if (res.isSuccess) {
        this.submissionFormData = res['result']['submissionEditForm']['submissionForm'];
        this.getSubmissionData();
      }
      else {
        this._toaster.errorMessage(res.message);
      }
    }, error => {
      this._toaster.warningMessage(error.message);
    })
  }

}
