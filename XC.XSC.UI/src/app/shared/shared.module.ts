import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { PanelMenuModule } from 'primeng/panelmenu';
import { MenubarModule} from 'primeng/menubar';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule} from 'primeng/dropdown';
import { MultiSelectModule } from 'primeng/multiselect';
import { TriStateCheckboxModule } from 'primeng/tristatecheckbox';
import { MessagesModule } from 'primeng/messages';
import { InputTextModule } from 'primeng/inputtext';
import { TooltipModule} from 'primeng/tooltip';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { RadioButtonModule} from 'primeng/radiobutton';
import { SliderModule} from 'primeng/slider';
import { AutoCompleteModule} from 'primeng/autocomplete';
import { InputSwitchModule } from 'primeng/inputswitch';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { KeyFilterModule } from 'primeng/keyfilter';
import { ToastModule } from 'primeng/toast';
import { TabViewModule } from 'primeng/tabview';
import { AccordionModule } from 'primeng/accordion';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { InputNumberModule} from 'primeng/inputnumber';
import { ChipsModule} from 'primeng/chips';
import {MessageModule} from 'primeng/message'
import {StepsModule} from 'primeng/steps';
import { TabMenuModule} from 'primeng/tabmenu';
import {CheckboxModule} from 'primeng/checkbox'
import { DynamicFormQuestionComponent } from '../common/dynamic-form/dynamic-form-question.component';
import { QuestionControlService } from '../common/dynamic-form/question-control.service';
import { SharedService } from '../Services/shared.service';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ConfirmationMessageComponent } from '../common/confirmation-message/confirmation-message.component';



@NgModule({
  declarations: [
    DynamicFormQuestionComponent,
    ConfirmationMessageComponent
  ],
  imports: [
    CommonModule,
    StepsModule,
    ReactiveFormsModule,
    FormsModule,
    MenubarModule,
    ButtonModule,
    CalendarModule,
    TableModule,
    DialogModule,
    DropdownModule,
    MultiSelectModule,
    TriStateCheckboxModule,
    MessagesModule,
    MessageModule,
    InputTextModule,
    TooltipModule,
    ConfirmDialogModule,
    ProgressSpinnerModule,
    RadioButtonModule,
    SliderModule,
    AutoCompleteModule,
    InputSwitchModule,
    OverlayPanelModule,
    KeyFilterModule,
    ToastModule,
    TabViewModule,
    PanelMenuModule,
    AccordionModule,
    TabMenuModule,
    StepsModule,
    BreadcrumbModule,
    CheckboxModule,
    InputNumberModule,
    ChipsModule,
    BsDatepickerModule,
  ],
  exports: [
    CommonModule,
    StepsModule,
    ReactiveFormsModule,
    FormsModule,
    MenubarModule,
    ButtonModule,
    CalendarModule,
    TableModule,
    DialogModule,
    DropdownModule,
    MultiSelectModule,
    TriStateCheckboxModule,
    MessagesModule,
    MessageModule,
    InputTextModule,
    TooltipModule,
    ConfirmDialogModule,
    ProgressSpinnerModule,
    RadioButtonModule,
    SliderModule,
    AutoCompleteModule,
    InputSwitchModule,
    OverlayPanelModule,
    KeyFilterModule,
    ToastModule,
    TabViewModule,
    PanelMenuModule, 
    AccordionModule,
    TabMenuModule,
    StepsModule,
    BreadcrumbModule,
    DynamicFormQuestionComponent,
    CheckboxModule,
    InputNumberModule,
    ChipsModule,
    ConfirmationMessageComponent
  ],
  providers: [QuestionControlService, DatePipe, SharedService]
})
export class SharedModule { }
