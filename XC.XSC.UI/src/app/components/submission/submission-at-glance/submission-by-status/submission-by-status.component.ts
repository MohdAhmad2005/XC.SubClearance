import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SubmissionStatus } from 'src/app/enum/submission/submission-status';
import { SubmissionAtGlance } from 'src/app/models/submission/submission-at-glance';
import { ISubmissionStatusResponse } from 'src/app/models/submission/submissionlist';

@Component({
  selector: 'app-submission-by-status',
  templateUrl: './submission-by-status.component.html',
  styleUrls: ['./submission-by-status.component.css']
})
export class SubmissionByStatusComponent implements OnInit {

  @Input() submissionsByStatus: ISubmissionStatusResponse[];

  constructor(private _router: Router) {

  }

  ngOnInit(): void { }

  public openSubmission(statusId: number): void{
    this._router.navigate(
      ['SubmissionSearch'],
      { queryParams: { submissionStatusId: statusId } }
    );
  }
  
}
