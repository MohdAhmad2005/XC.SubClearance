import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { XscLogoutComponent } from './common/xsc-logout/xsc-logout.component';
import { MyAccountComponent } from './components/account/my-account/my-account.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { SubmissionMemberComponent } from './components/submission/submission-member/submission-member.component';
import { SubmissionsComponent } from './components/submission/submissions/submissions.component';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized.component';
import { RuleEditorComponent } from './components/rule_engine/rule-editor/rule-editor.component';
import { RuleListComponent } from './components/rule_engine/rule-list/rule-list.component';
import { SlaConfigurationComponent } from './components/sla_configuration/sla-configuration/sla-configuration.component';
import { ReviewManagemntComponent } from './components/review-managemnt/review-managemnt.component';
import { KeycloakGuard } from 'src/guards/keycloak.guard';

const approutes: Routes = [
  { path: '', component: DashboardComponent , canActivate: [KeycloakGuard] }, 
  { path: 'Dashboard', component: DashboardComponent, canActivate: [KeycloakGuard], pathMatch: 'full' },
  { path: 'RuleList', component: RuleListComponent, pathMatch: 'full' },
  { path: 'rule-conf/rule-editor', component: RuleEditorComponent, pathMatch: 'full' },  
  { path: 'Submissions', component: SubmissionsComponent, canActivate: [KeycloakGuard], pathMatch: 'full' },
  { path: 'AutomationSubmissions', component: SubmissionsComponent, canActivate: [KeycloakGuard], pathMatch: 'full' },
  { path: 'SubmissionSearch', component: SubmissionsComponent, canActivate: [KeycloakGuard], pathMatch: 'full' },
  { path: 'SubmissionMember', component: SubmissionMemberComponent, canActivate: [KeycloakGuard], pathMatch: 'full' },
  { path: 'Unauthorized', component: UnauthorizedComponent },
  { path: 'Logout', component: XscLogoutComponent, pathMatch: 'full' },
  { path: 'MyAccount', component: MyAccountComponent, canActivate: [KeycloakGuard], pathMatch: 'full' },  
  { path:'Review', component:ReviewManagemntComponent, canActivate: [KeycloakGuard], pathMatch: 'full' },
  { path: 'Sla', component: SlaConfigurationComponent, canActivate: [KeycloakGuard], pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(approutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
