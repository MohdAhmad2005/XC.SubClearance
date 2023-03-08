import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subject, Subscription } from 'rxjs';
import { DoughnutService } from 'src/app/Services/chart/doughnut.service';
import { Router } from '@angular/router';
import { ToasterService } from 'src/app/common/toaster/toaster.service';
import { SubmissionScope } from 'src/app/enum/submission/submission-scope';
import { SubmissionsScopeCountResponse } from 'src/app/models/submission/submissionlist';
import { SubmissionService } from 'src/app/Services/submission/submission.service';
import { DateparserService } from 'src/app/utility/dateparser.service';
import { ChartConfig } from 'src/app/Services/chart/chartconfig';
import { EnvironmentService } from 'src/services/environment-service.service';


@Component({
  selector: 'app-submission-piechart',
  templateUrl: './submission-piechart.component.html',
  styleUrls: ['./submission-piechart.component.css']
})
export class SubmissionPiechartComponent implements OnInit, OnDestroy {

  public scopeList = SubmissionScope;
  public scopeListKeys = [];
  public paramDateRange: Date[] = []
  public currentdate: any = new Date();
  public submissionCount: SubmissionsScopeCountResponse;
  public inScopePercentage: number;
  public outOfScopePercentage: number;
  public selectedDateRange: Date[] = [];
  public differenceInDays: number;
  public data: any;
  public chartOptions: any;
  public subscription: Subscription;
  public config: ChartConfig;
  public bsDateRangeConfig: any = { dateInputFormat: this._envService.dateFormat, rangeInputFormat: this._envService.dateFormat,showWeekNumbers: false };


  @Input() ReloadEmailAutomation: Subject<any>;

  constructor(private _submissionService: SubmissionService,
    private _dateParseService: DateparserService,
    private _toasterService: ToasterService,
    private _router: Router,
    private _chartService: DoughnutService,
    private _envService: EnvironmentService) {
    this.scopeListKeys = Object.keys(this.scopeList).filter(f => isNaN(Number(f)));
  }


  ngOnInit(): void {
    this.ReloadEmailAutomation.subscribe(value => {
      if (value.refreshControl) {
        this.selectedDateRange = (this.selectedDateRange && this.selectedDateRange.length) ? this.selectedDateRange : [new Date(this.currentdate.setDate(this.currentdate.getDate() - 7)), new Date()];
        if (this.selectedDateRange && this.selectedDateRange.length && !value.isLoadedFirstTime) {
          this.getSubmissionScopeCount(this.selectedDateRange);
        }
      }
    });
  }

  public chartData(): void {
    this.data = {
      labels: ['Outscope(' + this.outOfScopePercentage + '%)', 'Inscope(' + this.inScopePercentage + '%)'],
      datasets: [
        {
          data: [this.outOfScopePercentage, this.inScopePercentage],
          backgroundColor: ['rgb(34, 84, 195)', 'rgb(245, 128, 38)'],
        }
      ]
    };
    this.config = this._chartService.config;
  }

  public selectData(event: any): void {
    if (event.element.index == 1) {
      this._router.navigate(
        ['/AutomationSubmissions'],
        { queryParams: { date: this.paramDateRange, scope: this.scopeList.InScope } }
      );
    }
    else {
      this._router.navigate(
        ['/AutomationSubmissions'],
        { queryParams: { date: this.paramDateRange, scope: this.scopeList.OutScope } }
      );
    }
  }

  public getSubmissionScopeCount(event: any): void {
    if (!!event && event.length) {
this.differenceInDays = event[1].getDate() - event[0].getDate() + 1;
      if (event[0] > new Date() || event[1] > new Date()) {
        this._toasterService.errorMessage("Date Range can be selected for past 30days only. Please select Valid Date Range.");
        return;
      }
      else if (this.differenceInDays > 30) {
        this._toasterService.errorMessage("Date Range can be selected for past 30days only. Please select Valid Date Range.");
        return;
      }
        this.paramDateRange = event;
        this._submissionService.getSubmissionScopeCounts(this._dateParseService.FormatToISOString(event[0]), this._dateParseService.FormatToISOString(event[1])).subscribe(response => {
          if (!!response.isSuccess) {
            this.inScopePercentage = parseFloat(((response.result.inScopeCount / response.result.totalCount) * 100).toFixed(2));
            this.outOfScopePercentage = parseFloat(((response.result.outScopeCount / response.result.totalCount) * 100).toFixed(2));
            this.chartData();
          }
          else {
            this.inScopePercentage = 0;
            this.outOfScopePercentage = 0;
            this.chartData();
          }
        },
        error => {
          this._toasterService.warningMessage(error.message);
          });
    }
  }

  public ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
