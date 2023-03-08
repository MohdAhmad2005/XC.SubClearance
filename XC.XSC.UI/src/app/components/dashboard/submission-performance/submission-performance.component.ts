import { Component, Input, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { ToasterService } from 'src/app/common/toaster/toaster.service';
import { PerformanceType } from 'src/app/enum/submission/performanceType';
import { SubmissionPerformanceResponse } from 'src/app/models/submission/submissionlist';
import { SubmissionService } from 'src/app/Services/submission/submission.service';
import { DateparserService } from 'src/app/utility/dateparser.service';
import { EnvironmentService } from 'src/services/environment-service.service';


@Component({
  selector: 'app-submission-performance',
  templateUrl: './submission-performance.component.html',
  styleUrls: ['./submission-performance.component.css']
})
export class SubmissionPerformanceComponent implements OnInit {
  public columnDefination = [];
  public performanceType = PerformanceType;
  public performanceData: SubmissionPerformanceResponse[] = [];
  public performanceListKey = [];
  public selectedDateRange: Date[] = [];
  public currentDate: any = new Date();
  public differenceInDays: number;
  public bsDateRangeConfig: any = { dateInputFormat: this._envService.dateFormat, rangeInputFormat: this._envService.dateFormat,showWeekNumbers: false };
  @Input() ReloadEmailAutomation: Subject<any>;

  constructor(private _submissionService: SubmissionService,
    private _dateParserService: DateparserService,
    private _toasterService: ToasterService,
    private _envService: EnvironmentService) { this.performanceListKey = Object.keys(this.performanceType).filter(f => isNaN(Number(f))); }

  ngOnInit(): void {
    this.ReloadEmailAutomation.subscribe(value => {
      if (value.refreshControl) {
        this.selectedDateRange = (this.selectedDateRange && this.selectedDateRange.length) ? this.selectedDateRange : [new Date(this.currentDate.setDate(this.currentDate.getDate() - 7)), new Date()];
        if (this.selectedDateRange && this.selectedDateRange.length && !value.isLoadedFirstTime) {
          this.getSubmissionPerformance(this.selectedDateRange);
        }
      }
    });
    this.columnDefination = [
      //{ header: 'Date', field: 'date', sortKey: 'date', type:'date' },
      { header: 'Processor Name', field: 'processorName', sortKey: 'processorName' },
      { header: 'Assigned', field: 'assignedCount', sortKey: 'assignedCount' },
      { header: 'Completed', field: 'completedCount', sortKey: 'completedCount' },
      { header: 'Accuracy', field: 'accuracy', sortKey: 'accuracy' },
      { header: 'TATBreached', field: 'tatBreachedCount', sortKey: 'tatBreachedCount' }
    ]
  }

  public getSubmissionPerformance(event: Date[]): void {
    if (event.length >= 2) {
      this.differenceInDays = event[1].getDate() - event[0].getDate() + 1;
      if (event[0] > new Date() || event[1] > new Date()) {
        this._toasterService.errorMessage("Date Range can be selected for past 30days only. Please select Valid Date Range.");
        this.performanceData = [];
        return;
      }
      else if (this.differenceInDays > 30) {
        this._toasterService.errorMessage("Date Range can be selected for past 30days only. Please select Valid Date Range.");
        this.performanceData = [];
        return;
      }
      else {
        this._submissionService.getPerformance(this._dateParserService.FormatToISOString(event[0]), this._dateParserService.FormatToISOString(event[1]), this.performanceType.TeamPerformance).subscribe(response => {
          if (!!response.isSuccess) {
            this.performanceData = response.result;
          }
          else {
            this._toasterService.warningMessage(response.message);
            this.performanceData = [];
          }
        },
          error => {
            this._toasterService.errorMessage('An error has occurred.');
          });
      }
    }
  }
}
