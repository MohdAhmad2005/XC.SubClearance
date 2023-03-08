import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalDialogComponent } from 'src/app/common/modal-dialog/modal-dialog.component';
import { SubmissionService } from 'src/app/Services/submission/submission.service';
import { TaskTatMetricsResponse } from '../../../../models/submission/submissionlist';
import { DateparserService } from '../../../../utility/dateparser.service';

@Component({
  selector: 'app-task-tat-metrics',
  templateUrl: './task-tat-metrics.component.html',
  styleUrls: ['./task-tat-metrics.component.css']
})
export class TaskTatMetricsComponent implements OnInit {

  @Input() submissionId: number;
  public taskTatData: TaskTatMetricsResponse = {} as TaskTatMetricsResponse;

  constructor(
    private _activeModal: NgbActiveModal,
    private _submissionService : SubmissionService,
    private _modalService: NgbModal,
    private _dateParserService:DateparserService
    )
  {
  }

  ngOnInit(): void {
    this.getTaskTatMetrics();
  }

  public closeModal(): void {
    this._activeModal.close('Modal Closed');
  }

  private getTaskTatMetrics(): void {
      try {
        this._submissionService.getTaskTatMetrics(this.submissionId).subscribe(response => {
          if (!!response) {
            this.taskTatData = response;
          }
          else {
            this.openPopUp();
          }
        },
          error => {
            this.openPopUp();
          });
      } catch (error) {
        this.openPopUp();
      }
  }
  private openPopUp(): void {
      const modalRef = this._modalService.open(ModalDialogComponent);
      modalRef.componentInstance.ModalBody = "Unable to process your request. please try again later.";
      this.closeModal();
    }

  public getDate(date: any) {
      return this._dateParserService.FormatDate(date);
    }

}
