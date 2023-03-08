import { Component, EventEmitter, Input, OnInit, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { ControlContainer, NgForm } from '@angular/forms';
import { submissionColumn } from 'src/app/common/constant/constant';

@Component({
  selector: 'submission-details',
  templateUrl: './submission-details-table.component.html',
  styleUrls: ['./submission-details-table.component.css'],
  encapsulation: ViewEncapsulation.None,
  viewProviders: [{ provide: ControlContainer, useExisting: NgForm }],
})
export class SubmissionDetailsTableComponent implements OnInit {
  @Input() formData: any;
  @Input() rowData: any;
  @Input() submissionId;
  @ViewChild('itemForm') itemForm: NgForm;
  @Output() validityChange = new EventEmitter<boolean>();

  public submissionList: any[] = [];
  public colsData: any[];
  public submitted: false;
  private validStatus: boolean;

  constructor() { }
  ngOnInit() {
    this.getColumn();
    if (!!this.rowData && !!this.formData) {
      this.rowData.forEach(y => {
        this.formData.forEach(x => {
          if (x.fields === y.fields) {
            x['fields'] = y.fields,
              x.suggestions = y.suggestions,
              x.finalEntry = y.finalEntry,
              x.confidance = y.confidance
          }
        })
      });
      this.submissionList = this.rowData.filter(o1 => !this.formData.some(o2 => o1.fields === o2.fields));
    }
  }

  public getColumn(): void {
    this.colsData = submissionColumn.columns;
  };

}
