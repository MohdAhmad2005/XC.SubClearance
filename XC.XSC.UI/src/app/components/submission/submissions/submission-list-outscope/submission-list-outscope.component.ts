import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalDialogComponent } from 'src/app/common/modal-dialog/modal-dialog.component';
import { GenericGridSettings, PageInfo } from 'src/app/models/config/genricGridSetting';
import { Submissions } from 'src/app/models/submission/submissionlist';
import { SubmissionService } from 'src/app/Services/submission/submission.service';
import { BindGridSettingService } from 'src/app/utility/common/bind-grid-settings.service';

@Component({
  selector: 'app-submission-list-outscope',
  templateUrl: './submission-list-outscope.component.html',
  styleUrls: ['./submission-list-outscope.component.css']
})
export class SubmissionListOutscopeComponent implements OnInit {

  public columnDefination = [];
  public isSubmissionListVisible: boolean = true;
  public tableContent : Submissions[];
  private submissionFilters: any;
  public pageInfo: PageInfo;
  public genericGridSettings: GenericGridSettings = new GenericGridSettings();

  @Input() set Reset(value: any) {
    if (!!value && value.resetFilters) {
      this.pageInfo = new PageInfo();
      this.tableContent = [];
    }
  }
  @Input() set SubmissionFilters(value: any) {
    if (!!value) {
      this.submissionFilters = value;
      this.getTableContent('');
    }
  }
  constructor(private _submissionService: SubmissionService,
    private _bindGridSettingService: BindGridSettingService,
    private _modalService: NgbModal) { }

  ngOnInit(): void {
    this.pageInfo = new PageInfo();
    this.setSubmissionListColumns();
  }

  private setSubmissionListColumns(): void {

    this.columnDefination = [
      { header: 'Case Number', field: 'caseNumber', sortKey: 'CaseId' },
      { header: 'Received Date', field: 'recieveDate', sortKey: 'EmailInfo.ReceivedDate', type: 'date' },
      { header: 'From', field: 'fromEmail', sortKey: 'EmailInfo.FromEmail' },
      { header: 'Reason for Out-Scope', field: 'comments', sortKey: 'Comments' },
    ]
  }

  public getTableContent(event: any) {
    this.genericGridSettings = this._bindGridSettingService.BindGridSettings(event);

    if (this.isSubmissionListVisible) {
      this._submissionService.getSubmissions(this.submissionFilters, this.genericGridSettings).subscribe(response => {
        if (response.isSuccess) {
          this.tableContent = response.result.submissions;
          const pageInfo: PageInfo = {
            currentPage: response.result.currentPage,
            totalItems: response.result.totalItems,
            totalpages: response.result.totalPages
          }
          this.pageInfo = pageInfo;
        }
        else {
          this.tableContent = [];
          this.pageInfo = new PageInfo;
        }
      },
        error => {
          this.showErrorPopUp();
        });

    }
  }

  private showErrorPopUp(): void {
    const modalRef = this._modalService.open(ModalDialogComponent);
    modalRef.componentInstance.ModalBody = "Unable to process your request. please try again later.";
  }
}
