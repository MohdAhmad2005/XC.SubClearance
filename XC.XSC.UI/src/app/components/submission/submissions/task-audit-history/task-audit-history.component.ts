
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Component, Input, OnInit, OnChanges } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { AlertService } from '../../../../common/alert-message/alert-service.service';
import { ModalDialogComponent } from '../../../../common/modal-dialog/modal-dialog.component';
import { SubmissionService } from '../../../../Services/submission/submission.service';
import { DateparserService } from '../../../../utility/dateparser.service';
import { TaskTatMetricsComponent } from '../task-tat-metrics/task-tat-metrics.component';
import { AuditHistoryResponse } from 'src/app/models/submission/submissionlist';
import { data } from 'jquery';
import { FormArray } from '@angular/forms';
import {AccordionModule} from 'primeng/accordion';

@Component({
  selector: 'app-task-audit-history',
  templateUrl: './task-audit-history.component.html',
  styleUrls: ['./task-audit-history.component.css']
})
export class TaskAuditHistoryComponent implements OnInit {
  tableContent:any;
  tableDuration:any;
  @Input() submissionId: number;
  isControlVisible:boolean=false;
  dataMessageResults: any[]
  jsonList: any;
  
  constructor(private _activeModal: NgbActiveModal,private _submissionService: SubmissionService, private _modalService: NgbModal,  private _alertService: AlertService,) { }

  ngOnInit(): void {
    this.getAuditHistory();
    this.getAuditHistoryDuration();

  }

  closeModal() {

    this._activeModal.close('Modal Closed');
  }

   //#region Get GetAuditHistory list
  private getAuditHistory() {
    this._submissionService.getAuditHistory(this.submissionId).subscribe(res=>{
      if(!!res){
        this.tableContent =res.result
      
      }
     
    },
    (error) => {
      
    });
  }
  //#region Get GetAuditHistory Duration list
  private getAuditHistoryDuration() {
    this._submissionService.getAuditHistoryDuration(this.submissionId).subscribe(res=>{
      this.tableDuration =res.result as any;
    
    },
    (error) => {

    }
    );
  }
//#region Get GetAuditHistory Header
 public getHeaders(data: any) {
    let headers: string[] = [];
     this.jsonList = JSON.parse(data);
    if (this.jsonList) {
      this.jsonList.forEach((value) => {
        Object.keys(value).forEach((key) => {
          if (!headers.find((header) => header == key)) {
            headers.push(key)
          }
        })
      })
    }
    return headers;
  }
  //#endregion

}
