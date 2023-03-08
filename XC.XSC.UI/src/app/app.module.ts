import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker'
import { LoaderInterceptor } from './interceptors/loader.interceptor';
import { TableModule } from 'primeng/table';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { SubmissionModule } from './components/submission/submission.module';
import { AlertModule } from './common/alert-message/alert.module';
import { ConfirmationService, MessageService } from 'primeng/api';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { AppRoutingModule } from './app-routing.module';
import { NgbActiveModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { RuleConfigurationModule } from './components/rule_engine/rule-configuration.module';
import { ToastrModule } from 'ngx-toastr';
import { AppComponent } from './app.component';
import { LoginComponent } from './authentication/login/login.component';
import { LoaderComponent } from './common/loader/loader.component';
import { ForgotPasswordComponent } from './authentication/forgot-password/forgot-password.component';
import { OtpVerificationComponent } from './authentication/otp-verification/otp-verification.component';
import { ResetPasswordComponent } from './authentication/reset-password/reset-password.component';
import { XscHeaderComponent } from './common/xsc-header/xsc-header.component';
import { XscFooterComponent } from './common/xsc-footer/xsc-footer.component';
import { XscLeftMenuComponent } from './common/xsc-left-menu/xsc-left-menu.component';
import { XscLogoutComponent } from './common/xsc-logout/xsc-logout.component';
import { MyAccountComponent } from './components/account/my-account/my-account.component';
import { ModalDialogComponent } from './common/modal-dialog/modal-dialog.component';
import { NotificationComponent } from './common/notification/notification.component';
import { RecoverPasswordComponent } from './authentication/recover-password/recover-password.component';
import { LegalEntityListComponent } from './authentication/legal-entity-list/legal-entity-list.component';
import { ModalDialogInfoComponent } from './common/modal-dialog-info/modal-dialog-info.component';
import { SharedModule } from './shared/shared.module';
import { GenericModalComponent } from './common/generic-modal/generic-modal.component';
import { SchedulerListComponent } from './components/scheduler/scheduler-list/scheduler-list.component';
import { AccountDetailsComponent } from './components/account/my-account/account-details/account-details.component';
import { OrganisationDetailsComponent } from './components/account/my-account/organisation-details/organisation-details.component';
import { NumberOnlyDirective } from './Directive/number-only.directive';
import { initializeKeycloak } from './models/iam/app.init';
import { KeycloakAngularModule, KeycloakService } from 'keycloak-angular';
import { KeycloakGuard } from 'src/guards/keycloak.guard';
import { IAMService } from './Services/auth/iam.service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    LoaderComponent,
    ForgotPasswordComponent,
    OtpVerificationComponent,
    ResetPasswordComponent,
    XscHeaderComponent,
    XscFooterComponent,
    XscLeftMenuComponent,
    XscLogoutComponent,
    MyAccountComponent,
    ModalDialogComponent,
    NotificationComponent,
    RecoverPasswordComponent,
    LegalEntityListComponent,
    ModalDialogInfoComponent,
    GenericModalComponent, 
    SchedulerListComponent,
    AccountDetailsComponent,
    OrganisationDetailsComponent,
    NumberOnlyDirective,
  ],
  imports: [
    BrowserModule, 
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    NgbModule,
    BrowserAnimationsModule,
    BsDatepickerModule,
    ToastrModule.forRoot(),
    TabsModule.forRoot(),
    RuleConfigurationModule,
    TableModule,
    SharedModule,
    AlertModule,
    FormsModule,
    KeycloakAngularModule,
    SubmissionModule
  ],
  exports: [
    SharedModule
  ],

  providers: [DatePipe,
    MessageService, 
    NgbActiveModal, 
    ConfirmationService,
    KeycloakService,
    KeycloakGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoaderInterceptor,
      multi: true,
    }, 
    {
      provide: APP_INITIALIZER,
      useFactory: initializeKeycloak,
      multi: true,
      deps: [KeycloakService, IAMService]
    },  
    { 
      provide: JWT_OPTIONS, 
      useValue: JWT_OPTIONS 
    }, JwtHelperService],
  bootstrap: [AppComponent]
})
export class AppModule { }
