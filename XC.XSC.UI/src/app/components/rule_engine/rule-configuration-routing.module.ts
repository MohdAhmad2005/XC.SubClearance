import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RuleListComponent } from './rule-list/rule-list.component';
import { RuleEditorComponent } from './rule-editor/rule-editor.component';
const routes: Routes = [
  {
    path: '',
    children: [
      { path: '', component: RuleListComponent, pathMatch: 'full' },
      { path: 'rule-conf/rule-list', component: RuleListComponent, pathMatch: 'full' },
      { path: 'rule-conf/rule-configuration', component: RuleEditorComponent, pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RuleConfigurationRoutingModule { }
