import { Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ActionReferenceType } from 'src/app/models/submission/submissionlist';
import { QuestionControlService } from '../dynamic-form/question-control.service'; 

 @Component({
 selector: 'app-generic-modal',
 templateUrl: './generic-modal.component.html',
 styleUrls: ['./generic-modal.component.css']
})
export class GenericModalComponent implements OnInit { 

 @Input() param: string;
 @Input() data: any;
 @Input() header: string;
 @Input() controls: any[];
 @Input() dynaForm: FormGroup;
 @Output() emitService = new EventEmitter();
 @Output() valueChangeEmitter = new EventEmitter(); 

 constructor(private controlService: QuestionControlService, private _activeModal: NgbActiveModal) { } 
 
 ngOnInit(): void {
 this.getFormData()
 } 

 hideModel() {
 this.emitService.emit(ActionReferenceType.CloseModal);
 } 

 closeModal() {
 this._activeModal.close();
 } 

 valueChange(): void {
 const valuehangeControl = this.controls.filter(item => item.changeEvent);
 valuehangeControl.forEach(x => {
 this.dynaForm.controls[x.key].valueChanges.subscribe(res => {
 this.valueChangeEmitter.emit({ res, control: [x.key] })
 })
 })
 }

 getFormData() {
 this.valueChange();
 if (this.data) {
 this.dynaForm.patchValue(this.data);
 }
 }

 onSubmit() {
 this.emitService.emit(this.dynaForm);
 } 

 }
