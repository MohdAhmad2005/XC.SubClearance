

import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToasterService } from 'src/app/common/toaster/toaster.service';
import { CommentType } from 'src/app/enum/submission/commentType';
import { Comments } from 'src/app/models/submission/Comments';

import { SubmissionService } from 'src/app/Services/submission/submission.service';
@Component({
  selector: 'app-under-query',
  templateUrl: './under-query.component.html',
  styleUrls: ['./under-query.component.css'],  
})
export class UnderQueryComponent implements OnInit {
  @Input() submissionId: number;
  @Input() caseNumber:string;
  public comments: Comments;
  public confirmationMessage: string;
  public isConfrim:boolean;

  underQueryformGroup: FormGroup;
 
  constructor(private _modalService: NgbModal
    ,private _activeModal: NgbActiveModal,
    private _submissionService : SubmissionService,
    private _toasterService: ToasterService
    ) { }

  ngOnInit(): void {
    this.underQueryformGroup=new FormGroup({
      underQueryRemark: new FormControl('')}
    )
  }

  getUnderQuryRemark(data: any) {
    this.underQueryformGroup.get('underQueryRemark').setValue(data)
  }
  public closeModal(status:boolean): void {
    this._activeModal.close(status);
  }
 
  submitUnderQuery(){  
    if(!this.underQueryformGroup.controls['underQueryRemark'].value){
      this.underQueryformGroup.controls['underQueryRemark'].setValidators([Validators.required])
      this.underQueryformGroup.controls['underQueryRemark'].updateValueAndValidity()
      return;
    }
    this.isConfrim=true;
    this.confirmationMessage="Do you want to put Case No "+ this.caseNumber +" under Query ?";
   } 
  reset(){    
    this.underQueryformGroup.reset();
     return
  }

  confirmationClick(event){
    if(event.isConfirmed){
      const comments:Comments={
        CommentType:CommentType.Query,
        CommentText: this.underQueryformGroup.get('underQueryRemark').value,     
        SubmissionId:this.submissionId,
      };
      this._submissionService.submissionUnderQuery(comments).subscribe(response => {
        if (response.isSuccess) {
         
          this.closeModal(response.isSuccess);
           
        }
        else{
          this._toasterService.errorMessage("Unable to process your request. please try again later.",this._toasterService.modalToastKey );
         
        }
      },
        error => {
          this._toasterService.errorMessage("Unable to process your request. please try again later." );
          
        });
    }
    this.isConfrim=false;
  }
}
