import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RuleListComponent } from './rule-list/rule-list.component';
import { RuleEditorComponent } from './rule-editor/rule-editor.component';
import { RuleConfigurationRoutingModule } from './rule-configuration-routing.module';
import {
  DxSelectBoxModule, DxValidatorModule,
  DxNumberBoxModule, DxTextBoxModule, DxButtonModule, DxTextAreaModule,
  DxFormModule, DxPopupModule, DxCheckBoxModule,
  DxFilterBuilderModule, DxSwitchModule, DxDataGridModule, DxTagBoxModule
} from 'devextreme-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  declarations: [RuleListComponent, RuleEditorComponent],
  imports: [
    CommonModule,
    RuleConfigurationRoutingModule,
    DxDataGridModule,
    DxValidatorModule,
    DxTextBoxModule,
    DxSelectBoxModule,
    DxButtonModule,
    FormsModule,
    ReactiveFormsModule,
    DxFormModule,
    DxTextAreaModule,
    DxCheckBoxModule,
    DxNumberBoxModule,
    DxFilterBuilderModule,
    DxTextBoxModule,
    DxPopupModule,
    DxSwitchModule,
    DxTagBoxModule,
    SharedModule
  ]
})
export class RuleConfigurationModule { }
