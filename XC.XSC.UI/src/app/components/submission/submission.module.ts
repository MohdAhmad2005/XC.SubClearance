import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from '../../app-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TableModule } from 'primeng/table';
import { AccordionModule } from 'primeng/accordion';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { AlertModule } from '../../common/alert-message/alert.module';

import { SubmissionMemberComponent } from './submission-member/submission-member.component';
import { DataValidationComponent } from './submission-member/data-validation/data-validation.component';
import { ClearanceSanctionComponent } from './submission-member/clearance-sanction/clearance-sanction.component';
import { GeneralInfoComponent } from './submission-member/general-info/general-info.component';
import { SubmissionListOutscopeComponent } from './submissions/submission-list-outscope/submission-list-outscope.component';
import { UnderQueryComponent } from './submissions/under-query/under-query/under-query.component';
import { SubmissionDetailsTableComponent } from './submission-member/data-validation/submission-details-table/submission-details-table.component';
import { SubmissionByStatusComponent } from './submission-at-glance/submission-by-status/submission-by-status.component';
import { SubmissionsComponent } from './submissions/submissions.component';
import { SubmissionFilterComponent } from './submissions/submission-filter/submission-filter.component';
import { TaskAuditHistoryComponent } from './submissions/task-audit-history/task-audit-history.component';
import { TaskTatMetricsComponent } from './submissions/task-tat-metrics/task-tat-metrics.component';
import { SubmissionListComponent } from './submission-list/submission-list.component';
import { SubmissionAtGlanceComponent } from './submission-at-glance/submission-at-glance.component';
import { LandingDashboardComponent } from '../dashboard/landing-dashboard/landing-dashboard.component';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { GenericTableComponent } from '../../common/generic-table/generic-table.component';
import { CalenderComponent } from '../../common/calender/calender.component';
import { GenericSubmissionControlComponent } from './submission-member/data-validation/generic-submission-control/generic-submission-control.component';

import { SlaConfigurationComponent } from '../sla_configuration/sla-configuration/sla-configuration.component';
import { SubmissionPiechartComponent } from '../dashboard/submission-piechart/submission-piechart.component';
import {ChartModule} from 'primeng/chart';
import { ReviewManagemntComponent } from '../review-managemnt/review-managemnt.component';
import { GenericStaticTableComponent } from 'src/app/common/generic-static-table/generic-static-table.component';
import { SharedModule } from '../../shared/shared.module';
import { SubmissionPerformanceComponent } from '../dashboard/submission-performance/submission-performance.component';
import { ReAllocationComponent } from './submissions/re-allocation/re-allocation.component';


@NgModule({
  declarations: [
    SubmissionMemberComponent,
    DataValidationComponent,
    ClearanceSanctionComponent,  
    SubmissionListOutscopeComponent,
    GeneralInfoComponent,
    UnderQueryComponent,
    SubmissionDetailsTableComponent,
    TaskAuditHistoryComponent,
    SubmissionByStatusComponent,
    SubmissionsComponent,
    TaskTatMetricsComponent,
    SubmissionFilterComponent,
    SubmissionListComponent,
    SubmissionAtGlanceComponent,
    LandingDashboardComponent,
    DashboardComponent,
    GenericTableComponent,
    CalenderComponent,
    GenericSubmissionControlComponent,
    SlaConfigurationComponent,
    GenericStaticTableComponent,
    SubmissionPiechartComponent,
    SubmissionPerformanceComponent,
    ReviewManagemntComponent,
    ReAllocationComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    NgbModule,
    BrowserAnimationsModule,
    BsDatepickerModule,
    TabsModule.forRoot(),
    TableModule,
    AccordionModule,
    ConfirmDialogModule,
    ToastModule,
    AlertModule,
    FormsModule,
    ChartModule,
    SharedModule
  ]
  
})
export class SubmissionModule { }
